using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Engine
{
    public class OnScreenJoyStick
    {
        public static SingleSprite BackgroundSprite;
        public static SingleSprite HandleSprite;

        public Vector2 Position { get; set; }
        public Point Size { get; set; }
        private bool flipped;
        private Vector2 HandlePosition;

        public Rectangle Bound =>
            new Rectangle((Position - Size.ToVector2()/2).ToPoint(), Size);

        public float Value => flipped? -getValue() : getValue();

        public OnScreenJoyStick(Vector2 position,Point size,bool flipped = false)
        {
            this.Position = position;
            HandlePosition = position;
            this.Size = size;
            this.flipped = flipped;
        }

        private float getValue()//check touch then mouse the return 0
        {
            float gameScale = 0 ;//= BaseGame.CurrentGame.RenderTextureScale;
            Vector2 gameOffset = Vector2.Zero;// = BaseGame.CurrentGame.RenderTextureOffset;

            var touchPanel = TouchPanel.GetState();

            foreach (TouchLocation touch in touchPanel)
            {
                var inGamePosition = (touch.Position - gameOffset) / gameScale;
                if (Physics.PointOverlap(inGamePosition, Bound))
                {
                    HandlePosition = inGamePosition;
                    return (inGamePosition.Y - Position.Y) / Size.Y;
                }
            }

            {
                var mousePos = Mouse.GetState().Position.ToVector2();
                var inGamePosition = (mousePos - gameOffset) / gameScale;
                if (Physics.PointOverlap(inGamePosition, Bound))
                {
                    HandlePosition = inGamePosition;
                    return (inGamePosition.Y - Position.Y) / Size.Y;
                }
            }


            HandlePosition = Position;
            return 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            HandlePosition.X = Position.X;
            BackgroundSprite.Draw(spriteBatch, scaleRectange(Bound,.8f), 0,false);
            HandleSprite.Draw(spriteBatch, HandlePosition,3f,0,true);
        }
        private Rectangle scaleRectange(Rectangle rect,float scale)
        {
            Point rectangeSize = new Point((int)(rect.Width * scale), (int)(rect.Height * scale));
            Point sizeDelta = rect.Size - rectangeSize;
            return new Rectangle((int)(rect.X + sizeDelta.X/2),(int)(rect.Y + sizeDelta.Y/2), rectangeSize.X, rectangeSize.Y);
        }
    }
}
