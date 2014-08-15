using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FataMirage.Core;
using System.Windows;

namespace FataMirageWindowsPhone8
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Game";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
            CoreControl.Init();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            FataMirage.Stator.contentManager = Content;
            FataMirage.Stator.spriteBatch = spriteBatch;
            CoreControl.LoadContent();
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            //Do not use graphics.PreferedBackBufferWidth because it is always 0 on Windows Phone
            FataMirage.Core.Graphics.Settings.actualScreenWidth = 800;//(int)Application.Current.Host.Content.ActualWidth;
            FataMirage.Core.Graphics.Settings.actualScreenHeight = 480; //(int)Application.Current.Host.Content.ActualHeight;
            CoreControl.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            CoreControl.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Draw(gameTime);
        }
    }
}
