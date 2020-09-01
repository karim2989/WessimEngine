using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public abstract class AnimatedSprite : Sprite
    {   
        public enum FrameTimeForm { GameTime,FrameCount }
        protected SingleSprite[] frames;
        protected bool looping;
        protected int currentFrameIndex;
        SingleSprite currentFrame => frames[currentFrameIndex];

        public AnimatedSprite(SingleSprite[] frames, bool looping = false)
        {
            this.frames = frames;
            this.looping = looping;
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 1, float rotation = 0, bool centered = false, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            currentFrame.Draw(spriteBatch, position, scale, rotation, centered, spriteEffect);
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle dest, float rotation, bool centered, Color tintColor)
        {
            currentFrame.Draw(spriteBatch, dest, rotation, centered, tintColor);
        }

        public override Sprite Clone()
        {
            return (AnimatedSprite)this.MemberwiseClone();
        }

        public AnimatedSprite Reverse()
        {
            var output = (AnimatedSprite)Clone();
            var frames = output.frames;
            for (int i = 0; i < frames.Length; i++)
                output.frames[i] = frames[frames.Length - (i + 1)];
            return output;
        }
        public abstract void Reset();
    }

    public class FrameBasedAnimatedSprite : AnimatedSprite
    {
        private int frameTime;
        private int currentTime = 0;
        public double TotalTime => looping ? double.PositiveInfinity : frameTime * frames.Length;
        public int TotalFrames => frames.Length * frameTime;

        public FrameBasedAnimatedSprite(SingleSprite[] frames, int frameTime, bool looping = false) : base(frames, looping)
        {
            this.frameTime = frameTime;
        }

        public override void Update(GameTime gameTime)
        {
            currentTime += 1;
            if (currentTime >= frameTime)
            {
                if (currentFrameIndex + 1 == frames.Length) { if (looping) currentFrameIndex = 0; }
                else currentFrameIndex++;

                currentTime = 0;
            }
        }
        public override void Reset() { currentFrameIndex = 0; currentTime = 0; }
    }

    public class TimeBasedAnimatedSprite : AnimatedSprite
    {
        private double frameTime;
        private double currentTime = 0;
        public double TotalTime => looping ? double.PositiveInfinity : frameTime * frames.Length;

        public TimeBasedAnimatedSprite(SingleSprite[] frames, double frameTime, bool looping = false) : base(frames,looping)
        {
            this.frameTime = frameTime;
        }

        public override void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= frameTime)
            {
                if (currentFrameIndex + 1 == frames.Length) { if (looping) currentFrameIndex = 0; }
                else currentFrameIndex++;

                currentTime = 0;
            }
        }
        public override void Reset() { currentFrameIndex = 0; currentTime = 0; }
    }
}
