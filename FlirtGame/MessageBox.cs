using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlirtGame
{
    class MessageBox : Button
    {
        public MessageBox(Element parent, Sprite sprite, Point position, Point size,string[] bodyMessage,string bottomMessage) : base(parent, sprite, position, size,GamePlayScreen.renderTextureScale)
        {
            smallfont = ContentLibrary.GetFont(FontList.Sans_serif);
            largefont = ContentLibrary.GetFont(FontList.Sans_serif_large);
            this.bodyMessage = bodyMessage;
            this.bottomMessage = bottomMessage;
        }

        private SpriteFont smallfont;
        private SpriteFont largefont;
        private string[] bodyMessage;
        private string bottomMessage;
        public bool ShowBottomText { get; set; } = true;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < bodyMessage.Length; i++)
                if (bodyMessage[i] != default(string))
                    spriteBatch.DrawString(largefont, bodyMessage[i], GlobalPosition + new Vector2(30,25+ 45*i), new Color(192,192,192,255));

            Vector2 bottomMessagePosition = (bound.Location + bound.Size).ToVector2() - (smallfont.MeasureString(bottomMessage) + new Vector2(40,15));
            if(ShowBottomText) spriteBatch.DrawString(smallfont, bottomMessage, bottomMessagePosition, new Color(192,192,192,255));
        }
    }
}
