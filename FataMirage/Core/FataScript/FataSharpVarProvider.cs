using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FataMirage.Core.FataScript
{
    class FataSharpVarProvider
    {
        public static void SetVar(string name, string value)
        {

        }
        public static string GetVar(string name)
        {
            List<string> splitName = name.Split('.').ToList();
            switch (splitName[0])
            {
                case "ImageLayer":
                    var layer = Scene.SceneManager.currentScene.layers[splitName[1]];
                    switch (splitName[2])
                    {
                        case "Opacity":
                            return (layer as Scene.Layers.ImageLayer).opacity.ToString();
                    }
                    break;
                case "ItemInHand":
                    return Player.Inventory.Items.currentItem;
            }
            return "";
        }
    }
}
