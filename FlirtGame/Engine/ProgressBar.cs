using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.UI
{
    class ProgressBar : Element
    {
        public override bool Active { get; set; }
        public Vector2 Size;
        private Sprite sprite;
        public Rectangle Bound
        {
            get => new Rectangle(Position.ToPoint(), Size.ToPoint());
            set
            {
                Position = value.Location.ToVector2();
                Size = value.Size.ToVector2();
            }
        }

        private float _value;
        public float Value
        {
            get => _value;
            set
            {
                if (value < 0 || value > 1) throw new Exception("value must be between 0 and 1");
                _value = value;
            }
        }

        public Color BackgroundColor { get; set; } = Color.Red;
        public Color ForegroundColor { get; set; } = Color.Green;
        public bool Fliped { get; set; }

        public ProgressBar(Sprite sprite,Vector2 position,Vector2 size) : base(null, position)
        {
            this.sprite = sprite;
            this.Position = position;
            this.Size = size;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Bound, 0, false, BackgroundColor);
            if(!Fliped)
                sprite.Draw(spriteBatch, new Rectangle(Position.ToPoint(),new Vector2((float)(Size.X * Value),Size.Y).ToPoint()), 0, false, ForegroundColor);
            else
                sprite.Draw(spriteBatch, new Rectangle(new Vector2(Position.X + Size.X*(1-Value),Position.Y).ToPoint(),new Vector2(Size.X * Value,Size.Y).ToPoint()), 0, false, ForegroundColor);
        }

        public override void Update(GameTime gameTime) { }
    }
}
