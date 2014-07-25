using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace FataMirageWinRT.Core.Player.Inventory
{
    /// <summary>
    /// Defines the align of the inventory
    /// </summary>
    public enum InventoryAlign
    {
        TopLeft,
        TopCenter,
        TopRight
    }
    /// <summary>
    /// Provides helper functions for the InventoryAlign type
    /// </summary>
    public static class InventoryAlignHelper
    {
        /// <summary>
        /// Converts a string to InventoryAlign
        /// </summary>
        /// <param name="from">The string that should be converted</param>
        /// <returns>The matching InventoryAlign or TopCenter in case no matching align was found.</returns>
        public static InventoryAlign toInventoryAlign(string from)
        {
            //Convert everything to lower letters so it is not required to correctly upper case the word
            from = from.ToLower();
            switch (from)
            {
                case "topcenter":
                case "top":
                case "top-center":
                case "center-top":
                case "top center":
                case "center top":
                case "centertop": return InventoryAlign.TopCenter;

                
                case "topleft":
                case "lefttop":
                case "top-left":
                case "left-top":
                case "top left":
                case "left top":
                case "left": return InventoryAlign.TopLeft;

                case "topright":
                case "righttop":
                case "top-right":
                case "right-top":
                case "top right":
                case "right top":
                case "right": return InventoryAlign.TopRight;
                    
            }
            Debugging.Logger.Warn("Invalid inventory align",
                "The value \"" + from + "\" is not valid for the property \"<Align>\". File: UI.xml. The default value \"CenterTop\" was used instead.",
                Debugging.Logger.Importance.Error);
            return InventoryAlign.TopCenter;
        }
    }
}
