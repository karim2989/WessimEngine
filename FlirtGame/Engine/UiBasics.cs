using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Engine.UI
{
    public abstract class Element : System.Object
    {
        public Element(Element parent,Vector2 position)
        {
            Children = new List<Element>();
            Parent = parent;
            if(parent != null) Parent.Children.Add(this);
            Position = position;
        }
        /*private*/~Element()
        {
            if(Parent != null)
                Parent.Children.Remove(this);
        }
        public Element Parent { get; set; }
        public List<Element> Children { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 GlobalePosition => Parent == null ? Position : Parent.GlobalePosition + Position;
        public virtual bool Active { get; set; }
        public abstract void Update(GameTime gameTime = null);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
    public class Canvas : Element
    {
        private bool _active;
        public override bool Active
        {
            get => _active;
            set
            {
                _active = value;
                foreach (var element in Children)
                {
                    element.Active = value;
                    element.Update();
                }
            }
        }
        public Rectangle Area { get; set; }
        public Canvas(Rectangle area,string name = default(string)) : base(null, area.Location.ToVector2())
        {
            this.Area = area;
            this.Position = Area.Location.ToVector2();
        }

        public Canvas Clone()
        {
            var output = this.MemberwiseClone() as Canvas;
            output.Children = new List<Element>();
            foreach (var c in this.Children)
                output.Children.Add(c);

            return output;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var element in Children)
                element.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var element in Children)
                element.Draw(spriteBatch);
        }
        public void DestroyChildren() => Children.Clear();
    }
    public class Container : Element
    {
        public Container(Element parent, Vector2 position) : base(parent, position) { }

        public Sprite Bg;
        public Vector2 BgSize;
        public Color TintColor;

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Bg != null) Bg.Draw(spriteBatch, new Rectangle(Position.ToPoint(),BgSize.ToPoint()),0,false,TintColor);

            foreach (var child in Children)
                child.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime = null)
        {
            foreach (var child in Children)
                child.Update(gameTime);
        }
        public void DestroyChilderen() => Children.Clear();
    }
}
