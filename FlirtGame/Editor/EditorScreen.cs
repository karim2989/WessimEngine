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
    class EditorScreen : Engine.Screen
    {
        public Canvas Canvas;

        public EditorScreen(Game g,GraphicsDeviceManager graphics) : base(g)
        {
            Canvas = new Canvas(new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
        }

        public override void Start()
        {
            
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch sb)
        {
            sb.Begin();

            Canvas.Draw(sb);

            sb.End();
        }
    }
}
