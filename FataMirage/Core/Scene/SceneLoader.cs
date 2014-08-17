using System;
using System.Xml.Linq;
#if NETFX_CORE || WINDOWS_PHONE
using Windows.Storage;
#endif
using System.IO;
using System.Globalization;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FataMirage.Core.Scene
{
    class SceneLoader
    {
#if NETFX_CORE || WINDOWS_PHONE
        public async static void LoadScenes()
#elif WINDOWS || ANDROID
        public static void LoadScenes()
#endif
        {
            Game.State.gameState = Game.State.GameStates.LoadingScenes;
            SceneManager.scenes = new Dictionary<string, SceneState>();
#if NETFX_CORE || WINDOWS_PHONE
            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;
            foreach (StorageFolder sceneFolder in await (await installedLocation.GetFolderAsync("Game\\Scenes")).GetFoldersAsync())
            {
                StorageFile sceneXml = await sceneFolder.GetFileAsync("scene.xml");
                XDocument document = XDocument.Load((await sceneXml.OpenAsync(FileAccessMode.Read)).AsStream());
                string sceneName = sceneFolder.Name;
#elif WINDOWS
            var sceneDirs = Directory.GetDirectories("Game\\Scenes", "*", SearchOption.TopDirectoryOnly);
            foreach (var sceneDir in sceneDirs)
            {
                XDocument document = XDocument.Load(File.OpenRead(sceneDir + "\\scene.xml"));
                string sceneName = sceneDir.Substring("Game\\Scenes\\".Length);
#elif ANDROID
            List<string> scenes = new List<string>();
            var reader = new StreamReader(FataMirageAndroid.Activity1.assets.Open("Game/Scenes.txt"));
            while (!reader.EndOfStream)
                scenes.Add(reader.ReadLine());
            foreach (string sceneName in scenes)
            {
                XDocument document = XDocument.Load(FataMirageAndroid.Activity1.assets.Open("Game/Scenes/" + sceneName + "/scene.xml"));
#endif
                SceneState scene = new SceneState();
                foreach (XElement node in document.Element("Scene").Elements())
                {
                    if (node.Name.LocalName == "ImageLayer")
                    {
                        scene.layers.Add(node.Attribute("Name").Value,
                            Layers.LayerFactory.createImageLayer("Scenes\\" + sceneName + "\\Graphics\\" + node.Attribute("Texture").Value,
                            float.Parse(node.Attribute("Depth").Value, CultureInfo.InvariantCulture)));
                    }
                    else if (node.Name.LocalName == "WalkingPoint")
                    {
                        Scene.Path.Waypoint waypoint = new Path.Waypoint(new Vector2(0, 0));
                        waypoint.position.X = float.Parse(node.Attribute("X").Value, CultureInfo.InvariantCulture);
                        waypoint.position.Y = float.Parse(node.Attribute("Y").Value, CultureInfo.InvariantCulture);
                        string walkingPointName = node.Attribute("Name").Value;
                        int hotspotCounter = 0;
                        foreach (XElement subNode in node.Elements())
                        {
                            if (subNode.Name.LocalName == "Connection")
                            {
                                Path.WaypointConnection wpConnection = new Path.WaypointConnection("");
                                wpConnection.goesTo = subNode.Attribute("Name").Value;
                                foreach (var attribute in subNode.Attributes("Speed"))
                                    wpConnection.walkingMultiplier = float.Parse(attribute.Value, CultureInfo.InvariantCulture);
                                if (subNode.Attribute("Enabled") != null)
                                    wpConnection.enabled = Convert.ToBoolean(subNode.Attribute("Enabled").Value);
                                waypoint.connectedTo.Add(wpConnection);
                            }
                            else if (subNode.Name.LocalName == "Hotspot")
                            {
                                string scriptName = "$" + sceneName + "->" + walkingPointName + "->" + hotspotCounter.ToString();
                                FataScript.ScriptManager.AddScript(scriptName, node.Value);
                                Hotspot hotspot = new Hotspot(float.Parse(subNode.Attribute("X").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("Y").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("X").Value, CultureInfo.InvariantCulture),
                                    scriptName);
                                waypoint.hotspots.Add(hotspot);
                                hotspotCounter++;
                            }
                        }
                        scene.waypoints.waypoints.Add(walkingPointName, waypoint);
                    }
                    else if (node.Name.LocalName =="Item")
                    {
                        Player.Inventory.Item item = new Player.Inventory.Item(
                            new Vector2(0, 0),
                            new Graphics.Texture("Scenes\\" + sceneName + "\\Graphics\\" + node.Attribute("Texture").Value));
                        Player.Inventory.Items.items.Add(node.Attribute("Name").Value, item);
                    }
                }
                SceneManager.scenes.Add(sceneName, scene);
            }
            Game.State.gameState = Game.State.GameStates.Running;
        }
    }
}