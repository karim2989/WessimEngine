using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public abstract class Screen
    {
        public Screen(Game game) => this.game = game;
        public Game game { get; protected set; }
        public bool Started = false;

        public abstract void Start();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime,GraphicsDevice graphicsDevice,SpriteBatch sprite);

    }
}
