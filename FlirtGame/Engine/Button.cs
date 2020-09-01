using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Engine;

namespace Engine.UI
{
    public class Button : Element
    {
        public override bool Active { get; set; } = true;
        public Point Size;
        public Rectangle bound => new Rectangle(GlobalPosition.ToPoint(), Size);
        public Sprite sprite;
        public Color TintColor { get; set; }
        public Vector2 GlobalPosition => Parent.GlobalePosition + Position;
        float scaleFactor;

        public Action OnPressed;
        public Action OnJustPressed;
        public Action OnReleased;

        private bool wasPressed;

        public Button(Element parent,Sprite sprite, Point position, Point size,float scaleFactor) : base(parent,position.ToVector2())
        {
            this.Position = position.ToVector2();
            this.Size = size;
            this.sprite = sprite;
            this.Parent = parent;
            this.scaleFactor = scaleFactor;

            OnJustPressed += () => { };
            OnPressed += () => { };
            OnReleased += () => { };
        }

        public override void Update(GameTime gameTime = null)//gametime is only needed for animated sprites
        {
            bool currentlyPressed = this.Pressed;

            if      (wasPressed && !currentlyPressed) OnReleased();
            else if (!wasPressed && currentlyPressed) OnJustPressed();
            else if (currentlyPressed) OnPressed();

            if (sprite is AnimatedSprite animation)
                if(gameTime != null)
                    animation.Update(gameTime);

            wasPressed = currentlyPressed;
        }

        public bool Pressed => Active && ( pressedWithMouse() || pressedWithTouch() );

        private bool pressedWithMouse()
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
                return Physics.PointOverlap(mouseState.Position.ToVector2() / scaleFactor, bound);
            else
                return false;
        }

        private bool pressedWithTouch()
        {
            foreach (var touch in TouchPanel.GetState())
                if (Physics.PointOverlap(touch.Position / scaleFactor, bound)) return true;

            return false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, bound, 0, false,TintColor);
        }
    }
}
