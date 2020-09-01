using System;
using System.IO;
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
    class GamePlayScreen : Engine.Screen
    {
        public GamePlayScreen(Game game,GraphicsDeviceManager graphics) : base(game)
        {
            mainRenderTexture = new RenderTarget2D(game.GraphicsDevice, 1080, 1920);

            Vector2 renderSize = new Vector2(1080,1920);
            Vector2 screenSize = new Vector2(graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight);
            Vector2 sizeDelta = screenSize / renderSize;
            renderTextureScale = Math.Min(sizeDelta.X, sizeDelta.Y);
            renderTextureOffset = (screenSize - renderSize * renderTextureScale) / 2;
        }

        private RenderTarget2D mainRenderTexture;
        public static float renderTextureScale;
        private Vector2 renderTextureOffset;

        public static Canvas Canvas;
        private Scenario scenario;

        public override void Start()
        {
            Canvas = new Canvas(new Rectangle(0, 607, 1080, 1920 - 607));

            scenario = Scenario.FromString(File.ReadAllText("c:\\dev\\FlirtGame\\FlirtGame\\game.txt"));
        }
        public override void Update(GameTime gameTime)
        {
            scenario.Update();
            Canvas.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            graphicsDevice.SetRenderTarget(mainRenderTexture);
            graphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
                Canvas.Draw(spriteBatch);
            spriteBatch.End();
            
            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Black);
            Rectangle destRect = new Rectangle(renderTextureOffset.ToPoint(),(new Vector2(1080, 1920) * renderTextureScale).ToPoint());
            spriteBatch.Begin();
                spriteBatch.Draw(mainRenderTexture,destRect,Color.White);
            spriteBatch.End();
        }
    }
}
