using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FataMirageWinRT
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Game";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
            Core.CoreControl.Init();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Stator.contentManager = Content;
            Stator.spriteBatch = spriteBatch;
            Core.CoreControl.LoadContent();
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            Core.Graphics.Settings.actualScreenWidth = graphics.PreferredBackBufferWidth;
            Core.Graphics.Settings.actualScreenHeight = graphics.PreferredBackBufferHeight;
            Core.CoreControl.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Core.CoreControl.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Draw(gameTime);
        }
    }
}
