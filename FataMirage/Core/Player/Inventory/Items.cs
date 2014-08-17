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
        public static Dictionary<string, Item> items = new Dictionary<string, Item>();
        public static void Update(float elapsedTime)
        {
            for (int i = 0; i < items.Count();i++ )
                items.ElementAt(i).Value.Update(elapsedTime, new Vector2(InventoryConfig.getBounds(i).X , 0));
        }
        public static int count
        {
            get
            {
                return items.Count;
            }
        }
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
        public static Texture2D getTexture(int index)
        {
            return items.ElementAt(index).Value.texture.texture;
        }
    }
}
