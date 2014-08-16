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
            Input.ClickLayerManager.clickLayers.Add(new Input.ClickLayer(1, (x, y) =>
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
                            return true;
                        }
                    }
                    return false;
                }));
        }
        public static void LoadContent()
        {
            Legs.LoadContent();
            Inventory.InventoryManager.LoadContent();
        }
        public static void Update(float elapsedTime)
        {
            Legs.Update(elapsedTime);
            Inventory.InventoryManager.Update(elapsedTime);
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
