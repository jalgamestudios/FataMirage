#if ANDROID
using Android.Content.Res;
#endif
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#if NETFX_CORE || WINDOWS_PHONE
using Windows.Storage;
#endif

namespace FataMirage.Core.Game
{
    class LoadMeta
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
            StorageFile meta = await installedLocation.GetFileAsync("Game\\meta.xml");
            XDocument document = XDocument.Load((await meta.OpenAsync(FileAccessMode.Read)).AsStream());
#elif WINDOWS
            XDocument document = XDocument.Load(File.Open("Game\\meta.xml", FileMode.Open));
#elif ANDROID
            XDocument document = XDocument.Load(FataMirageAndroid.Activity1.assets.Open("Game/meta.xml"));
#endif
            foreach (XElement node in document.Element("Meta").Elements())
            {
                if (node.Name.LocalName == "RenderWidth")
                    Graphics.Settings.renderWidth = int.Parse(node.Value);
                else if (node.Name.LocalName == "RenderHeight")
                    Graphics.Settings.renderHeight = int.Parse(node.Value);
                else if (node.Name.LocalName == "StartingScene")
                    Scene.SceneManager.currentSceneName = node.Value;
                else if (node.Name.LocalName == "PlayerStarts")
                {
                    Player.Legs.currentPositionName = node.Value;
                    Player.Legs.isWalking = false;
                }
                else if (node.Name.LocalName == "PlayerTextures")
                {
                    Player.TextureProvider.walkingLeft = new List<Graphics.Texture>();
                    Player.TextureProvider.walkingRight = new List<Graphics.Texture>();
                    Player.TextureProvider.walkingTop = new List<Graphics.Texture>();
                    Player.TextureProvider.walkingBottom = new List<Graphics.Texture>();
                    foreach (XElement subNode in node.Elements())
                    {
                        if (subNode.Name.LocalName == "StandingFront")
                            Player.TextureProvider.standingFront = new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subNode.Value));
                        else if (subNode.Name.LocalName == "StandingLeft")
                            Player.TextureProvider.standingLeft = new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subNode.Value));
                        else if (subNode.Name.LocalName == "StandingBack")
                            Player.TextureProvider.standingBack = new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subNode.Value));
                        else if (subNode.Name.LocalName == "StandingRight")
                            Player.TextureProvider.standingRight = new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subNode.Value));
                        else if (subNode.Name.LocalName == "WalkingRight")
                            foreach (XElement subSubNode in subNode.Elements())
                                Player.TextureProvider.walkingRight.Add(new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subSubNode.Value)));
                        else if (subNode.Name.LocalName == "WalkingLeft")
                            foreach (XElement subSubNode in subNode.Elements())
                                Player.TextureProvider.walkingLeft.Add(new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subSubNode.Value)));
                        else if (subNode.Name.LocalName == "WalkingUp")
                            foreach (XElement subSubNode in subNode.Elements())
                                Player.TextureProvider.walkingTop.Add(new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subSubNode.Value)));
                        else if (subNode.Name.LocalName == "WalkingDown")
                            foreach (XElement subSubNode in subNode.Elements())
                                Player.TextureProvider.walkingBottom.Add(new Graphics.Texture(Stator.contentManager.Load<Texture2D>(subSubNode.Value)));
                    }
                }
                else
                    Debug.WriteLine("meta.xml: Unknown element (" + node.Name.LocalName);
            }
        }
    }
}
