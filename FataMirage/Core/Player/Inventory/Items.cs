using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FataMirage.Core.Player.Inventory
{
    static class Items
    {
        /// <summary>
        /// A list of all items avilable in the game, containting also the items that are currently not in the players inventory
        /// </summary>
        public static Dictionary<string, Item> items = new Dictionary<string, Item>();

        /// <summary>
        /// Stores the item that is currently selected (or "none" if no item is selected)
        /// </summary>
        public static string currentItem = "none";

        /// <summary>
        /// Updates all items
        /// </summary>
        /// <param name="elapsedTime">The time since the last time this methdo was called, measured in seconds</param>
        public static void Update(float elapsedTime)
        {
            for (int i = 0; i < items.Count();i++ )
                items.ElementAt(i).Value.Update(elapsedTime, new Vector2(InventoryConfig.getBounds(i).X , 0));
        }

        /// <summary>
        /// The number of items that exist in total
        /// </summary>
        public static int count
        {
            get
            {
                return items.Count;
            }
        }

        /// <summary>
        /// Returns the position of the item at the given index in the item list (not the inventory)
        /// </summary>
        /// <param name="index">The index in the item list</param>
        /// <returns>If the item is visible, its bounds, otherwise, a new RectangleF(0,0,0,0)</returns>
        public static SharpDX.RectangleF getPosition(int index)
        {
            if (!items.ElementAt(index).Value.OnStage)
                return new SharpDX.RectangleF(0, 0, 0, 0); //That's an amazingly efficient way of hiding something!!
            return new SharpDX.RectangleF(items.ElementAt(index).Value.currentPosition.X,
                (items.ElementAt(index).Value.currentPosition.Y == 0) ? 
                InventoryConfig.getBounds(index).Y : items.ElementAt(index).Value.currentPosition.Y,
                //It's too late in the evening to create a new option for the item width, 
                //so let's just make them all square!
                InventoryConfig.height,
                InventoryConfig.height);
        }

        /// <summary>
        /// Returns the texture of the item at the given index in the item list (not the inventory)
        /// </summary>
        /// <param name="index">The index in the item list</param>
        /// <returns>The texture of the item</returns>
        public static Texture2D getTexture(int index)
        {
            return items.ElementAt(index).Value.texture.texture;
        }

        public static string getItemAtInventoryPosition(int inventoryPosition)
        {
            var itemsInInventory = items.Where(item => item.Value.OnStage).ToList();
            if (itemsInInventory.Count <= inventoryPosition)
                return "none";
            return itemsInInventory.ElementAt(inventoryPosition).Key;
        }
    }
}
