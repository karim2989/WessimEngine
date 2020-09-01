using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public abstract class Sprite
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 position, float scale = 1, float rotation = 0, bool centered = false, SpriteEffects spriteEffect = SpriteEffects.None);
        public abstract void Draw(SpriteBatch spriteBatch, Rectangle dest, float rotation, bool centered, Color tintColor);
        public abstract Sprite Clone();
    }
    public class SingleSprite : Sprite
    {
        private GraphicsDevice graphicsDevice;
        private Texture2D baseTexture;
        private Rectangle sourceRectangle;
        public Point spriteSize => sourceRectangle.Size;

        public SingleSprite(GraphicsDevice graphicsDevice,Texture2D baseTexture, Rectangle sourceRectangle)
        {
            this.graphicsDevice = graphicsDevice;
            this.baseTexture = baseTexture;
            this.sourceRectangle = sourceRectangle;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position,float scale = 1,float rotation = 0,bool centered = false, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            Vector2 spriteOrigin = centered ? spriteSize.ToVector2()/2 :Vector2.Zero;
            spriteBatch.Draw(baseTexture, position , sourceRectangle, Color.White, rotation, spriteOrigin, scale, spriteEffect, 0);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle dest,float rotation,bool centered)
        {
            Vector2 spriteOrigin = centered ? spriteSize.ToVector2()/2 :Vector2.Zero;
            spriteBatch.Draw(baseTexture, dest, sourceRectangle, Color.White, rotation,spriteOrigin,SpriteEffects.None,0);
        }
        public override void Draw(SpriteBatch spriteBatch, Rectangle dest, float rotation, bool centered, Color tintColor = default(Color))
        {
            if (tintColor == default(Color)) tintColor = Color.White;
            Vector2 spriteOrigin = centered ? spriteSize.ToVector2() / 2 : Vector2.Zero;
            spriteBatch.Draw(baseTexture, dest, sourceRectangle, tintColor, rotation, spriteOrigin,SpriteEffects.None, 0);
        }
        public override Sprite Clone()
        {
            return (SingleSprite)this.MemberwiseClone();
        }
    }
}
