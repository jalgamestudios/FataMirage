using System;
using System.Xml.Linq;
using Windows.Storage;
using System.IO;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace FataMirage.Core.Scene
{
    class SceneLoader
    {
        public async static void LoadScenes()
        {
            Game.State.gameState = Game.State.GameStates.LoadingScenes;
            Windows.ApplicationModel.Package package = Windows.ApplicationModel.Package.Current;
            Windows.Storage.StorageFolder installedLocation = package.InstalledLocation;
            foreach (StorageFolder sceneFolder in await (await installedLocation.GetFolderAsync("Game\\Scenes")).GetFoldersAsync())
            {
                StorageFile sceneXml = await sceneFolder.GetFileAsync("scene.xml");
                XDocument document = XDocument.Load((await sceneXml.OpenAsync(FileAccessMode.Read)).AsStream());
                SceneState scene = new SceneState();
                foreach (XElement node in document.Element("Scene").Elements())
                {
                    if (node.Name.LocalName == "ImageLayer")
                    {
                        scene.layers.Add(node.Attribute("Name").Value,
                            Layers.LayerFactory.createImageLayer("Scenes\\" + sceneFolder.Name + "\\Graphics\\" + node.Attribute("Texture").Value,
                            float.Parse(node.Attribute("Depth").Value, CultureInfo.InvariantCulture)));
                    }
                    else if (node.Name.LocalName == "WalkingPoint")
                    {
                        Scene.Path.Waypoint waypoint = new Path.Waypoint(new Vector2(0, 0));
                        waypoint.position.X = float.Parse(node.Attribute("X").Value, CultureInfo.InvariantCulture);
                        waypoint.position.Y = float.Parse(node.Attribute("Y").Value, CultureInfo.InvariantCulture);
                        foreach (XElement subNode in node.Elements())
                        {
                            if (subNode.Name.LocalName == "Connection")
                            {
                                Path.WaypointConnection wpConnection = new Path.WaypointConnection("");
                                wpConnection.goesTo = subNode.Attribute("Name").Value;
                                foreach (var attribute in subNode.Attributes("Speed"))
                                    wpConnection.walkingMultiplier = float.Parse(attribute.Value, CultureInfo.InvariantCulture);
                                waypoint.connectedTo.Add(wpConnection);
                            }
                        }
                        string name = node.Attribute("Name").Value;
                        scene.waypoints.waypoints.Add(name, waypoint);
                    }
                }
                SceneManager.scenes.Add(sceneFolder.Name, scene);
            }
            Game.State.gameState = Game.State.GameStates.Running;
        }
    }
}