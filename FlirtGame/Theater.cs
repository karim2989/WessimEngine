using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Engine;
using Coroutine = System.Collections.IEnumerator;

namespace FlirtGame
{
    public class Theater
    {
        public static Vector2 TheaterSize = new Vector2(192, 72);
        RenderTarget2D renderTexture;
        public bool Active { get; set; } = true;

        public TheaterCharacter[] TheaterCharacters = new TheaterCharacter[7];
        public Sprite Env { get; set; }
        public Theater(RenderTarget2D renderTexture)
        {
            int bodyWidth = 38;
            int charactetOffset = 20;

            Env = ContentLibrary.GetSprite(SpriteList.env_school);

            this.renderTexture = renderTexture;
            TheaterCharacters[0] = ContentLibrary.GetTheaterCharacter(TheaterCharactersList.character1);
            TheaterCharacters[0].Position = new Vector2(bodyWidth + charactetOffset * 0, 61);
            TheaterCharacters[0].Fliped = true;

            TheaterCharacters[1] = ContentLibrary.GetTheaterCharacter(TheaterCharactersList.character2);
            TheaterCharacters[1].Position = new Vector2(bodyWidth + charactetOffset * 1, 61);
            TheaterCharacters[1].Fliped = true;

            TheaterCharacters[2] = ContentLibrary.GetTheaterCharacter(TheaterCharactersList.character3);
            TheaterCharacters[2].Position = new Vector2(bodyWidth + charactetOffset * 2, 62);

            TheaterCharacters[3] = ContentLibrary.GetTheaterCharacter(TheaterCharactersList.character4);
            TheaterCharacters[3].Position = new Vector2(bodyWidth + charactetOffset * 3, 61);

            TheaterCharacters[4] = ContentLibrary.GetTheaterCharacter(TheaterCharactersList.character5);
            TheaterCharacters[4].Position = new Vector2(bodyWidth + charactetOffset * 4, 61);

            TheaterCharacters[5] = ContentLibrary.GetTheaterCharacter(TheaterCharactersList.character6);
            TheaterCharacters[5].Position = new Vector2(bodyWidth + charactetOffset * 5, 61);

            TheaterCharacters[6] = ContentLibrary.GetTheaterCharacter(TheaterCharactersList.character7);
            TheaterCharacters[6].Position = new Vector2(bodyWidth + charactetOffset * 6, 61);
        }

        public void Update()
        {
            foreach (var c in TheaterCharacters)
                c.Update();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Active) return;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            Env.Draw(spriteBatch, Vector2.Zero);

            foreach (var c in TheaterCharacters)
                c.Draw(spriteBatch);

            spriteBatch.End();
        }
        public Theater Clone() => this.MemberwiseClone() as Theater;
    }
    public class TheaterCharacter
    {
        public string Name { get; set; }

        public Vector2 Position { get; set; }
        public bool Fliped { get; set; }
        public bool FlipedLocked { get; set; }
        public bool Visible { get; set; } = false;

        public Sprite BodySprite { get; set; }

        public Sprite LowerHeadSpriteN { get; set; }
        public Sprite UpperHeadSpriteN { get; set; }

        public Sprite LowerHeadSpriteA { get; set; }
        public Sprite UpperHeadSpriteA { get; set; }

        public Sprite LowerHeadSpriteS { get; set; }
        public Sprite UpperHeadSpriteS { get; set; }

        public Sprite LowerHeadSpriteH { get; set; }
        public Sprite UpperHeadSpriteH { get; set; }

        private Sprite currentLowerHead;
        private Sprite currentUpperHead;

        public SoundEffectInstance TalkSound;

        private Coroutine currentAction;

        public TheaterCharacter(string name,Sprite bodySprite)
        {
            Name = name;
            BodySprite = bodySprite;
            currentAction = IdleCoroutine();
        }

        public void Update()
        {
            bool actionFinished = false;
            if(currentAction != null) actionFinished = !currentAction.MoveNext();

            if (actionFinished) currentAction = IdleCoroutine();

            if (currentLowerHead == null) currentLowerHead = LowerHeadSpriteN;
            if (currentUpperHead == null) currentUpperHead = UpperHeadSpriteN;
        }

        public void SetEmotion(char e)
        {
            switch (e)
            {
                case 'a':
                    {
                        currentLowerHead = LowerHeadSpriteA;
                        currentUpperHead = UpperHeadSpriteA;
                    }return;
                case 'n':
                    {
                        currentLowerHead = LowerHeadSpriteN;
                        currentUpperHead = UpperHeadSpriteN;
                    }return;
                case 'h':
                    {
                        currentLowerHead = LowerHeadSpriteH;
                        currentUpperHead = UpperHeadSpriteH;
                    }return;
                case 's':
                    {
                        currentLowerHead = LowerHeadSpriteS;
                        currentUpperHead = UpperHeadSpriteS;
                    }return;
                default: throw new Exception($"unrecognized emotion {e}");
            }
        }

        public float JawYOffset { get; set; }
        public float HeadYOffset { get; set; }
        public float JawRotation { get; set; }
        public float HeadOffset { get; set; }
        public float PositionYOffset;

        Vector2 bodyPosition => Position - Vector2.UnitY  * ( PositionYOffset + 16);
        Vector2 lowerPosition => Position - Vector2.UnitY * ( PositionYOffset + (24 + HeadYOffset + HeadOffset));
        Vector2 upperPosition => Position - Vector2.UnitY * ( PositionYOffset + (40 + HeadYOffset + JawYOffset + HeadOffset));

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible) return;
            BodySprite.Draw(spriteBatch, bodyPosition, centered:true, spriteEffect:Fliped?SpriteEffects.FlipHorizontally:SpriteEffects.None);
            currentLowerHead.Draw(spriteBatch, lowerPosition, centered:true, spriteEffect:Fliped?SpriteEffects.FlipHorizontally:SpriteEffects.None);
            currentUpperHead.Draw(spriteBatch, upperPosition, centered:true, spriteEffect:Fliped?SpriteEffects.FlipHorizontally:SpriteEffects.None, rotation:JawRotation);
        }

        public void WalkIn() => currentAction = WalkInCoroutine();
        public Coroutine WalkInCoroutine()
        {
            float time = 15f;
            float dirention = Fliped ? 1:-1;
            for (int j = 0; j < 6; j++)
            {
                ContentLibrary.GetSound(SoundList.jump).Play();
                for (int i = 0; i < (int)time; i++)
                {
                    Position = new Vector2(Position.X + 2 * dirention ,Position.Y);
                    PositionYOffset = (float)Math.Sin((1 / time) * i * Math.PI) * 5;
                    yield return 0;
                }
                for (int i = 0; i < 2; i++) yield return 0;
            }
        }

        public Coroutine IdleCoroutine()
        {
            for (int i = 0; i < GamePlayScreen.random.Next(0, 80); i++) yield return 0;
            while (true)
            {
                HeadYOffset = 0;
                for (int i = 0; i < GamePlayScreen.random.Next(30, 200); i++) yield return 0;
                HeadYOffset = 1;
                for (int i = 0; i < 60; i++) yield return 0;
                //if (GamePlayScreen.random.Next(0, 36) == 6 && !FlipedLocked) Fliped = !Fliped; 
            }
        }

        public void Talk(int speachLength = 3) => currentAction = TalkCoroutine(speachLength);
        public Coroutine TalkCoroutine(int speachLength)
        {
            var rng = GamePlayScreen.random;
            for (int j = 0; j < speachLength; j++)
            {
                JawYOffset = 2;
                JawRotation = 0.05f;
                TalkSound.Pitch = (float)(rng.NextDouble() - 0.5f)/4;
                TalkSound.Stop();
                TalkSound.Play();
                for (int i = 0; i < 5; i++) yield return 0;
                JawYOffset = 0;
                /*TalkSound.Pitch = (float)(rng.NextDouble() - 0.5f)/2;
                TalkSound.Stop();
                TalkSound.Play();*/
                for (int i = 0; i < 5; i++) yield return 0;

                JawYOffset = 2;
                JawRotation = -0.05f;
                TalkSound.Pitch = (float)(rng.NextDouble() - 0.5f)/4;
                TalkSound.Stop();
                TalkSound.Play();
                for (int i = 0; i < 5; i++) yield return 0;
                JawYOffset = 0;
                /*TalkSound.Pitch = (float)(rng.NextDouble() - 0.5f)/2;
                TalkSound.Stop();
                TalkSound.Play();*/
                for (int i = 0; i < 5; i++) yield return 0;
            }
            JawRotation = 0;
            JawYOffset = 0;
        }
    }
}
