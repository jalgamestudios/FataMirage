using SharpDX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Player.Inventory
{
    /// <summary>
    /// Contains all configuration details of the inventory
    /// that are not likely to change during the execution
    /// </summary>
    class InventoryConfig
    {
        /// <summary>
        /// If true, InventoryTextures.leftSide will be shown on the left side of the content area
        /// </summary>
        public static bool hasLeft;
        /// <summary>
        /// If true, InventoryTextures.rightSide will be shown on the right side of the content area
        /// </summary>
        public static bool hasRight;
        /// <summary>
        /// Where on the screen the inventory is placed
        /// </summary>
        public static InventoryAlign align;

        /// <summary>
        /// Weather or not the inventory can be dismissed
        /// </summary>
        public static bool collapsable;
        /// <summary>
        /// How many seconds it takes to show/hide the inventory
        /// </summary>
        public static float collapseSpeed;
        
        /// <summary>
        /// Specifies how the width of the inventory is determined
        /// </summary>
        public class InventoryWidth
        {
            /// <summary>
            /// The raw width, measured in the unit specified in "unit"
            /// </summary>
            public static float width;

            /// <summary>
            /// The unit width is measured in
            /// </summary>
            public static Units unit;

            /// <summary>
            /// A list of all possible units
            /// Note: Currently, there is only one width unit for the inventory.
            /// More may be added in future
            /// </summary>
            public enum Units
            {
                /// <summary>
                /// The inventory will contain the given number of item spots
                /// </summary>
                Items,
            }

            /// <summary>
            /// Reads the width and it's accompanying unit from a raw string
            /// </summary>
            /// <param name="from">The raw string</param>
            public static void FromString(string from)
            {
                if (from.EndsWith("it"))
                {
                    if (!float.TryParse(from.Substring(0, from.Length - "it".Length), 
                        NumberStyles.Float, CultureInfo.InvariantCulture, out width))
                        Debugging.Logger.Warn("Invalid Inventory Width", (from.Length - "perc".Length) + "could not be parsed", Debugging.Logger.Importance.Error);
                    unit = Units.Items;
                }
                //Note: other width units may be added in the future
            }
        }

        /// <summary>
        /// How much ScaleUnits the Inventory should be high
        /// </summary>
        public static float height;

        /// <summary>
        /// Returns the bounding box of the given item
        /// </summary>
        /// <param name="itemIndex">The index of the item, counting from the left-most spot</param>
        /// <param name="totalItems">The total amount of items</param>
        /// <returns></returns>
        public static RectangleF getBounds(int itemIndex)
        {
            switch (align)
            {
                case InventoryAlign.TopLeft:
                    {
                        float positionX = 0;
                        if (hasLeft)
                            positionX += InventoryTextures.getLeftSideWidth;
                        positionX += itemIndex * InventoryTextures.getItemHighlightWidth;
                        return new RectangleF(positionX, (InventoryManager.inventoryShowed - 1) * height,
                            InventoryTextures.getItemHighlightWidth, height);
                    }
                case InventoryAlign.TopCenter:
                    {
                        float positionX = 0.5f - percentualWidth / 2;
                        if (hasLeft) positionX += InventoryTextures.getLeftSideWidth;
                        positionX += itemIndex * InventoryTextures.getItemHighlightWidth;
                        return new RectangleF(positionX, (InventoryManager.inventoryShowed - 1) * height,
                           InventoryTextures.getItemHighlightWidth, height);
                    }
                case InventoryAlign.TopRight:
                    {
                        float positionX = 1;
                        if (hasRight)
                            positionX -= InventoryTextures.getLeftSideWidth;
                        positionX -= (InventoryWidth.width - itemIndex) * InventoryTextures.getItemHighlightWidth;
                        return new RectangleF(positionX, (InventoryManager.inventoryShowed - 1) * height,
                            InventoryTextures.getItemHighlightWidth, height);
                    }
            }
            //If everything works fine, this line will never be reached
            return new RectangleF(0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the bounds of the left side texture.
        /// <remarks>Please note that this method assumes that hasLeft is true and won't return the correct values otherwise</remarks>
        /// </summary>
        /// <param name="totalItems">The number of total items in the inventory</param>
        /// <returns>A screen-space bounding box</returns>
        public static RectangleF getLeftSideBounds()
        {
            switch (align)
            {
                case InventoryAlign.TopLeft:
                    return new RectangleF(0,
                        (InventoryManager.inventoryShowed - 1) * height,
                        InventoryTextures.getLeftSideWidth,
                        height);
                case InventoryAlign.TopCenter:
                    return new RectangleF(0.5f - percentualWidth / 2,
                        (InventoryManager.inventoryShowed - 1) * height,
                        InventoryTextures.getLeftSideWidth,
                        height);
                case InventoryAlign.TopRight:
                    return new RectangleF(1 - (hasRight ? InventoryTextures.getRightSideWidth : 0) -
                        InventoryWidth.width * InventoryTextures.getItemHighlightWidth -
                        InventoryTextures.getLeftSideWidth,
                        (InventoryManager.inventoryShowed - 1) * height,
                        InventoryTextures.getLeftSideWidth, 
                        height);
            }
            return new RectangleF(0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the bounds of the right side texture.
        /// <remarks>Please note that this method assumes that hasRight is true and won't return the correct values otherwise</remarks>
        /// </summary>
        /// <param name="totalItems">The number of total items in the inventory</param>
        /// <returns>A screen-space bounding box</returns>
        public static RectangleF getRightSideBounds()
        {
            switch (align)
            {
                case InventoryAlign.TopLeft:
                    return new RectangleF((hasLeft ? InventoryTextures.getLeftSideWidth : 0) + InventoryWidth.width * InventoryTextures.getItemHighlightWidth,
                        (InventoryManager.inventoryShowed - 1) * height, 
                        InventoryTextures.getRightSideWidth,
                        height);
                case InventoryAlign.TopCenter:
                    return new RectangleF(0.5f - percentualWidth / 2 + (hasLeft ? InventoryTextures.getLeftSideWidth : 0) +
                        InventoryWidth.width * InventoryTextures.getItemHighlightWidth,
                        (InventoryManager.inventoryShowed - 1) * height, 
                        InventoryTextures.getRightSideWidth,
                        height);

                case InventoryAlign.TopRight:
                    return new RectangleF(1 - InventoryTextures.getRightSideWidth,
                        (InventoryManager.inventoryShowed - 1) * height, 
                        InventoryTextures.getRightSideWidth,
                        height);
            }
            return new RectangleF(0, 0, 0, 0);
        }
        /// <summary>
        /// Gets the width of the total inventory, including the left and right separator.
        /// </summary>
        public static float percentualWidth
        {
            get
            {
                return (hasLeft ? InventoryTextures.getLeftSideWidth : 0) +
                    InventoryWidth.width * InventoryTextures.getItemHighlightWidth +
                    (hasRight ? InventoryTextures.getRightSideWidth : 0);
            }
        }
        public static float inventoryLeft
        {
            get
            {
                switch (align)
                {
                    case InventoryAlign.TopLeft:
                        return 0;
                    case InventoryAlign.TopCenter:
                        return (1 - percentualWidth) / 2f;
                    case InventoryAlign.TopRight:
                        return 1 - percentualWidth;
                }
                //If this line is reached, something went wrong. But it will never be reached...
                return 0;
            }
        }
        public static RectangleF getexpanderBounds()
        {
            return new RectangleF(inventoryLeft + percentualWidth / 2 - InventoryTextures.collapser.getWidthByHeight(height / 2),
                InventoryManager.inventoryShowed * height, InventoryTextures.collapser.getWidthByHeight(height / 2), height / 2);
        }
    }
}
