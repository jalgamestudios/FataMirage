using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Player.Inventory
{
    /// <summary>
    /// Stores all the texture's the inventory needs to render (except the item textures)
    /// </summary>
    static class InventoryTextures
    {
        /// <summary>
        /// The texture that is displayed left of the inventory's content area
        /// </summary>
        public static Graphics.Texture leftSide;
        /// <summary>
        /// Gets the width of the leftSide texture when displayed at the height specified in "InventoryConfig.height".
        /// Sets InventoryConfig.height to make the texture be displayed at the given width
        /// </summary>
        public static float getLeftSideWidth
        {
            get
            {
                return leftSide.getWidthByHeight(InventoryConfig.height);
            }
            set
            {
                InventoryConfig.height = leftSide.getHeightByWidth(value);
            }
        }

        /// <summary>
        /// The texture that is displayed right of the inventory's content area
        /// </summary>
        public static Graphics.Texture rightSide;
        /// <summary>
        /// Gets the width of the leftSide texture when displayed at the height specified in "InventoryConfig.height".
        /// Sets InventoryConfig.height to make the texture be displayed at the given width
        /// </summary>
        public static float getRightSideWidth
        {
            get
            {
                return rightSide.getWidthByHeight(InventoryConfig.height);
            }
            set
            {
                InventoryConfig.height = rightSide.getHeightByWidth(value);
            }
        }

        /// <summary>
        /// The texture that is displayed below the texture of an item in the inventory
        /// </summary>
        public static Graphics.Texture itemHighlight;
        /// <summary>
        /// Gets the width of the leftSide texture when displayed at the height specified in "InventoryConfig.height".
        /// Sets InventoryConfig.height to make the texture be displayed at the given width
        /// </summary>
        public static float getItemHighlightWidth
        {
            get
            {
                return itemHighlight.getWidthByHeight(InventoryConfig.height);
            }
            set
            {
                InventoryConfig.height = itemHighlight.getHeightByWidth(value);
            }
        }

        /// <summary>
        /// The texture that is repeated in the content area in spots where there is no item
        /// </summary>
        public static Graphics.Texture noItem;
        /// <summary>
        /// Gets the width of the leftSide texture when displayed at the height specified in "InventoryConfig.height".
        /// Sets InventoryConfig.height to make the texture be displayed at the given width
        /// </summary>
        public static float getNoItemWidth
        {
            get
            {
                return noItem.getWidthByHeight(InventoryConfig.height);
            }
            set
            {
                InventoryConfig.height = noItem.getHeightByWidth(value);
            }
        }

        /// <summary>
        /// The texture of the button that will expand the inventory when the inventory is hidden
        /// </summary>
        public static Graphics.Texture expander;

        /// <summary>
        /// The texture of the button that will collapse the inventory when the inventory is shown
        /// </summary>
        public static Graphics.Texture collapser;


    }
}
