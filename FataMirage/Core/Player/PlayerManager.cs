using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Player
{
    class PlayerManager
    {
        public static void Init()
        {
            Legs.Init();
            Inventory.InventoryManager.Init();
        }
        public static void LoadContent()
        {
            //TextureProvider.currentTexture = new Graphics.Texture(Stator.contentManager.Load<Texture2D>("Player/Standing_Front"));
            Legs.LoadContent();
            Inventory.InventoryManager.LoadContent();
        }
        public static void Update(float elapsedTime)
        {
            Legs.Update(elapsedTime);
            Inventory.InventoryManager.Update(elapsedTime);
            if (Input.InputManager.pointerState == Input.InputManager.PointerStates.Click)
            {
                if (!Player.Legs.isWalking)
                {
                    string goal = Scene.SceneManager.currentScene.waypoints.getNearestConnection(
                        Player.Legs.currentPositionName,
                        Graphics.Scaler.screenToWorld(Input.InputManager.pointerX, 0).X,
                         Graphics.Scaler.screenToWorld(0, Input.InputManager.pointerY).Y);
                    if (goal != "false")
                    {
                        Player.Legs.currentGoal = goal;
                        Player.Legs.isWalking = true;
                    }
                    Input.InputManager.pointerState = Input.InputManager.PointerStates.Hover;
                }
            }
        }
        public static void Draw(float elapsedTime)
        {
            Legs.Draw(elapsedTime);
            Graphics.Scaler.Draw(TextureProvider.currentTexture.texture,
                Legs.currentPosition.X - TextureProvider.currentTexture.texture.Width / 2f / (float)Graphics.Settings.renderWidth,
                Legs.currentPosition.Y - TextureProvider.currentTexture.texture.Height / (float)Graphics.Settings.renderWidth,
                TextureProvider.currentTexture.texture.Width / (float)Graphics.Settings.renderWidth,
                TextureProvider.currentTexture.texture.Height / (float)Graphics.Settings.renderWidth,
                0.5f);
            Inventory.InventoryManager.Draw(elapsedTime);
        }
    }
}
