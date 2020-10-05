using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine;

namespace FlirtGame
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;

        //GamePlayScreen gamePlayScreen;
        Engine.Screen CurrentScreen;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            Graphics.PreferredBackBufferWidth = 1080 / 3;
            Graphics.PreferredBackBufferHeight = 1920 / 3;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            CurrentScreen = new EditorScreen(this,Graphics);

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ContentLibrary.LoadedTexture(Content,GraphicsDevice);
            ContentLibrary.LoadSprites(Content,GraphicsDevice);
            ContentLibrary.LoadFont(Content);
            ContentLibrary.LoadSounds(Content);
            ContentLibrary.loadXml(Content);

            CurrentScreen.Start();

            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                CurrentScreen = new GamePlayScreen(this, Graphics);
                CurrentScreen.Start();
            }

            CurrentScreen.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            CurrentScreen.Draw(gameTime,GraphicsDevice,spriteBatch);

            base.Draw(gameTime);
        }
    }
}
