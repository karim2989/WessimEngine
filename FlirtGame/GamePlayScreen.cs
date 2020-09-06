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

            theaterRenderTexture = new RenderTarget2D(game.GraphicsDevice,(int)Theater.TheaterSize.X,(int)Theater.TheaterSize.Y);
            theaterScale = Math.Min(1080 / Theater.TheaterSize.X , 1920 / Theater.TheaterSize.Y);
        }

        private RenderTarget2D mainRenderTexture;
        public static float renderTextureScale;
        private Vector2 renderTextureOffset;
        public static Color MainTextureTintColor { get; set; } = Color.White;

        private RenderTarget2D theaterRenderTexture;
        public static float theaterScale;
        public static Theater Theater { get; set; }

        public static Canvas Canvas;
        private Scenario scenario;
        public static Random random = new Random();

        public override void Start()
        {
            Canvas = new Canvas(new Rectangle(0, 462, 1080, 1920 - 462));

            //scenario = Scenario.FromString(File.ReadAllText("c:\\dev\\FlirtGame\\FlirtGame\\game.txt"));
            scenario = Scenario.FromString(ContentLibrary.GetXml(XmlFile.game1));

            ContentLibrary.LoadTheaterCharacter(scenario,game.GraphicsDevice);
            Theater = new Theater(theaterRenderTexture);
        }
        public override void Update(GameTime gameTime)
        {
            scenario.Update();
            Canvas.Update(gameTime);
            Theater.Update();
        }

        public override void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            graphicsDevice.SetRenderTarget(theaterRenderTexture);
            Theater.Draw(spriteBatch);

            graphicsDevice.SetRenderTarget(mainRenderTexture);
            graphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

                Canvas.Draw(spriteBatch);

            Rectangle theaterDestRect = new Rectangle(Point.Zero,(new Vector2(1080, 462)).ToPoint());
                if(Theater.Active)
                    spriteBatch.Draw(theaterRenderTexture, theaterDestRect, Color.White);

            spriteBatch.End();

            
            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Black);
            Rectangle mainDestRect = new Rectangle(renderTextureOffset.ToPoint(),(new Vector2(1080, 1920) * renderTextureScale).ToPoint());
            spriteBatch.Begin();
                spriteBatch.Draw(mainRenderTexture, mainDestRect,MainTextureTintColor);
            spriteBatch.DrawString(ContentLibrary.GetFont(FontList.Sans_serif), "karim2989.itch.io & wassim tounsi", Vector2.Zero, new Color(100,100,100,20),0,Vector2.Zero,.3f,SpriteEffects.None,0);
            spriteBatch.End();
        }
    }
}
