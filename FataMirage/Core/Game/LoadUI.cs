﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#if NETFX_CORE || WINDOWS_PHONE
using Windows.Storage;
#endif
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

using FataMirage.Core.Player.Inventory;
using System.Globalization;

namespace FataMirage.Core.Game
{
    class LoadUI
    {
#if NETFX_CORE || WINDOWS_PHONE
        public async static void Load()
#elif WINDOWS || ANDROID
        public static void Load()
#endif
        {
#if NETFX_CORE || WINDOWS_PHONE
            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;
            StorageFile meta = await installedLocation.GetFileAsync("Game\\UI.xml");
            XDocument document = XDocument.Load((await meta.OpenAsync(FileAccessMode.Read)).AsStream());
#elif WINDOWS
            XDocument document = XDocument.Load(File.Open("Game\\UI.xml", FileMode.Open));
#elif ANDROID
            XDocument document = XDocument.Load(FataMirageAndroid.Activity1.assets.Open("Game/UI.xml"));
#endif
            foreach (XElement node in document.Element("UI").Elements())
            {
                if (node.Name.LocalName == "Inventory")
                {
                    foreach (XElement subNode in node.Elements())
                    {
                        switch (subNode.Name.LocalName)
                        {
                            //Weather or not to display a texture at the left end of the inventory
                            case "HasLeft":
                                InventoryConfig.hasLeft = Convert.ToBoolean(subNode.Value);
                                break;
                            //Weather or not to display a texture at the right end of the inventory
                            case "HasRight":
                                InventoryConfig.hasRight = Convert.ToBoolean(subNode.Value);
                                break;

                            case "LeftEnd":
                                InventoryTextures.leftSide = new Graphics.Texture(subNode.Value);
                                break;
                            case "RightEnd":
                                InventoryTextures.rightSide = new Graphics.Texture(subNode.Value);
                                break;

                            case "InventoryItem":
                                InventoryTextures.itemHighlight = new Graphics.Texture(subNode.Value);
                                break;
                            case "InventoryBlank":
                                InventoryTextures.noItem = new Graphics.Texture(subNode.Value);
                                break;

                            case "Width":
                                InventoryConfig.InventoryWidth.FromString(subNode.Value);
                                break;

                            case "Height":
                                if (!float.TryParse(subNode.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out InventoryConfig.height))
                                {
                                    InventoryConfig.height = 0.05f;
                                    Debugging.Logger.Warn("Invalid inventory height", "The default value of 0.05 was taken instead", Debugging.Logger.Importance.Error);
                                }
                                break;

                            case "Align":
                                InventoryConfig.align = 
                                    InventoryAlignHelper.toInventoryAlign(subNode.Value);
                                break;

                            case "CollapseSpeed":
                                if (!float.TryParse(subNode.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out InventoryConfig.collapseSpeed))
                                {
                                    InventoryConfig.height = 2f;
                                    Debugging.Logger.Warn("Invalid collapse speed", "The default value of 0.0.5 was taken instead", Debugging.Logger.Importance.Error);
                                }
                                break;
                            case "Collapser":
                                InventoryTextures.collapser = new Graphics.Texture(subNode.Value);
                                break;
                            case "Expander":
                                InventoryTextures.expander = new Graphics.Texture(subNode.Value);
                                break;
                            default:
                                Debug.WriteLine("UI.xml: Unknown element (" + subNode.Name.LocalName + ")");
                                break;
            
                        }
                    }
                }
                else if (node.Name.LocalName == "Fragment")
                {
                    UI.Fragment fragment = new UI.Fragment(
                        float.Parse(node.Attribute("Width").Value, CultureInfo.InvariantCulture),
                        float.Parse(node.Attribute("Height").Value, CultureInfo.InvariantCulture),
                        UI.Fragment.createHorizontalAlign(node.Attribute("HorizontalAlign").Value),
                        UI.Fragment.createVerticalAlign(node.Attribute("VerticalAlign").Value),
                        node.Attribute("Texture").Value,
                        UI.Fragment.createSideClick(node.Attribute("SideClick").Value));
                    if (node.Attribute("Visible") != null)
                        fragment.visible = Convert.ToBoolean(node.Attribute("Visible").Value);
                    foreach (XElement buttonElement in node.Elements("Button"))
                    {
                        UI.Button button = new UI.Button(
                            buttonElement.Attribute("Name").Value,
                            float.Parse(buttonElement.Attribute("X").Value, CultureInfo.InvariantCulture),
                            float.Parse(buttonElement.Attribute("Y").Value, CultureInfo.InvariantCulture),
                            float.Parse(buttonElement.Attribute("Width").Value, CultureInfo.InvariantCulture),
                            float.Parse(buttonElement.Attribute("Height").Value, CultureInfo.InvariantCulture),
                            buttonElement.Element("OnClick").Value);
                        fragment.controls.Add(button);
                    }
                    UI.UIManager.fragments.Add(fragment);
                }
                else
                    Debug.WriteLine("UI.xml: Unknown element (" + node.Name.LocalName + ")");
            }
        }
    }
}
