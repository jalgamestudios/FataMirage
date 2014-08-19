using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Player.Inventory
{
    static class InventoryManager
    {
        /// <summary>
        /// A number indicating how much of the inventory is currently vertically revealed.
        /// <example>0.25 means that one fourth of the inventory is visible, while the rest is too high</example>
        /// </summary>
        public static float inventoryShowed = 0;
        /// <summary>
        /// How much of the inventory will be showed after the end fo the animation
        /// </summary>
        public static float inventoryShowedGoal = 1;
        public static void Init()
        {
            inventoryShowedGoal = 1;
            Input.ClickLayerManager.clickLayers.Add(new Input.ClickLayer(2, (x, y) =>
            {
                var relativePosition = Graphics.Scaler.screenToWorld(x, y);
                if (InventoryConfig.getexpanderBounds().Contains(relativePosition.X, relativePosition.Y))
                {
                    switch ((int)inventoryShowedGoal)
                    {
                        case 0: inventoryShowedGoal = 1; break;
                        case 1: inventoryShowedGoal = 0; break;
                    }
                    return true;
                }
                for (int i = 0; i < InventoryConfig.InventoryWidth.width; i++)
                {
                    if (InventoryConfig.getBounds(i).Contains(relativePosition.X, relativePosition.Y))
                    {
                        Items.currentItem = Items.getItemAtInventoryPosition(i);
                        return true;
                    }
                }
                return false;
                }));
        }
        public static void LoadContent()
        {
        }
        public static void Update(float elapsedTime)
        {
            if (inventoryShowedGoal < inventoryShowed)
            {
                inventoryShowed -= InventoryConfig.collapseSpeed * elapsedTime;
                if (inventoryShowedGoal >=inventoryShowed)
                    inventoryShowed = inventoryShowedGoal;
            }
            else if (inventoryShowedGoal > inventoryShowed)
            {
                inventoryShowed += InventoryConfig.collapseSpeed * elapsedTime;
                if (inventoryShowedGoal <= inventoryShowed)
                    inventoryShowed = inventoryShowedGoal;
            }
            Items.Update(elapsedTime);
        }
        public static void Draw(float elapsedTime)
        {
            if (InventoryConfig.hasLeft)
            {
                var leftBounds = InventoryConfig.getLeftSideBounds();
                Graphics.Scaler.Draw(InventoryTextures.leftSide.texture,
                    leftBounds.X, leftBounds.Y, leftBounds.Width, leftBounds.Height, 0.05f);
            }
            for (int i = 0; i < InventoryConfig.InventoryWidth.width; i++)
            {
                var bounds = InventoryConfig.getBounds(i);
                Graphics.Scaler.Draw(InventoryTextures.itemHighlight.texture,
                    bounds.X, bounds.Y, bounds.Width, bounds.Height, 0.05f);
            }
            if (InventoryConfig.hasRight)
            {
                var rightBounds = InventoryConfig.getRightSideBounds();
                Graphics.Scaler.Draw(InventoryTextures.rightSide.texture,
                    rightBounds.X, rightBounds.Y, rightBounds.Width, rightBounds.Height, 0.05f);
            }
            for (int i = 0; i < Items.count; i++)
            {
                var itembounds = Items.getPosition(i);
                Color color = Color.White;
                if (Items.currentItem != "none")
                {
                    color = Items.items.ElementAt(i).Key == Items.currentItem ? Color.White : Color.LightGray;
                }
                Graphics.Scaler.Draw(Items.getTexture(i),
                    itembounds.X, itembounds.Y, itembounds.Width, itembounds.Height,
                    color, 0);
            }
            var expanderBounds = InventoryConfig.getexpanderBounds();
            Graphics.Scaler.Draw(InventoryTextures.collapser.texture,
                expanderBounds.X, expanderBounds.Y,
                expanderBounds.Width, expanderBounds.Height, new Color(1f, 1f, 1f, 1), 0.05f);
            Graphics.Scaler.Draw(InventoryTextures.expander.texture,
                expanderBounds.X, expanderBounds.Y,
                expanderBounds.Width, expanderBounds.Height, new Color(1f, 1f, 1f, inventoryShowed), 0.05f);
        }
    }
}
