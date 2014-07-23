using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using System.IO;
using System.Diagnostics;

namespace FataMirageWinRT.Core.Game
{
    class LoadUI
    {
        public async static void Load()
        {
            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;
            StorageFile meta = await installedLocation.GetFileAsync("Game\\meta.xml");
            XDocument document = XDocument.Load((await meta.OpenAsync(FileAccessMode.Read)).AsStream());
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
                else
                    Debug.WriteLine("UI.xml: Unknown element (" + node.Name.LocalName);
            }
        }
    }
}
