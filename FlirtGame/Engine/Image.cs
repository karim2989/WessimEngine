using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.UI
{
    class Image : Element
    {
        public Image(Element parent,Sprite sprite,Vector2 pos,float scale = 1,SpriteEffects effect = SpriteEffects.None) : base(parent, pos)
        {
            this.Parent = parent;
            this.Position = pos;
            this.sprite = sprite;
            this.scale = scale;
            this.effect = effect;
        }

        private Sprite sprite;
        private float scale;
        private SpriteEffects effect;

        public override bool Active { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, GlobalePosition, scale,spriteEffect:effect);
        }

        public override void Update(GameTime gameTime = null)
        {
            sprite.Update(gameTime);
        }
    }
}
