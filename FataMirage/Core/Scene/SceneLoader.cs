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
                        var layer = Layers.LayerFactory.createImageLayer("Scenes\\" + sceneName + "\\Graphics\\" + node.Attribute("Texture").Value,
                            float.Parse(node.Attribute("Depth").Value, CultureInfo.InvariantCulture));
                        if (node.Attribute("Opacity") != null)
                            (layer as Scene.Layers.ImageLayer).opacity = 
                                float.Parse(node.Attribute("Opacity").Value, CultureInfo.InvariantCulture);
                        if (node.Attribute("Visible") != null)
                            (layer as Scene.Layers.ImageLayer).visible = 
                                Convert.ToBoolean(node.Attribute("Visible").Value, CultureInfo.InvariantCulture);
                        scene.layers.Add(node.Attribute("Name").Value,
                            layer);
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
                                    float.Parse(subNode.Attribute("Radius").Value, CultureInfo.InvariantCulture),
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
                    else if (node.Name.LocalName=="ParticleSystem")
                    {
                        Particles.ParticleHost.particleDefinitions = new Dictionary<int, Particles.ParticleDefinition>();
                        Particles.ParticleHost.particleBounds = new Dictionary<string, Particles.ParticleBound>();
                        if (node.Attribute("Lightmap") != null)
                            Particles.LightMap.Load("Scenes\\" + sceneName + "\\Graphics\\" + node.Attribute("Lightmap").Value);
                        foreach (XElement subNode in node.Elements())
                        {
                            if (subNode.Name.LocalName == "ParticleType")
                            {
                                Particles.ParticleDefinition definition = new Particles.ParticleDefinition(
                                    float.Parse(subNode.Attribute("Gravity").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("Mass").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("Randomness").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("MaxSpeed").Value, CultureInfo.InvariantCulture));
                                XElement colorElement = subNode.Element("Colors");
                                foreach (XElement colorSubNode in colorElement.Elements())
                                {
                                    var color = new Particles.ParticleColorMapColor(
                                            int.Parse(colorSubNode.Attribute("ColorMapR").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("ColorMapG").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("ColorMapB").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("RMin").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("GMin").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("BMin").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("RMax").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("GMax").Value, CultureInfo.InvariantCulture),
                                            int.Parse(colorSubNode.Attribute("BMax").Value, CultureInfo.InvariantCulture),
                                            float.Parse(colorSubNode.Attribute("Variation").Value, CultureInfo.InvariantCulture));
                                    if (colorSubNode.Attribute("AMin") != null)
                                        color.minColor.A = (byte)int.Parse(colorSubNode.Attribute("AMin").Value, CultureInfo.InvariantCulture);
                                    if (colorSubNode.Attribute("AMax") != null)
                                        color.minColor.A = (byte)int.Parse(colorSubNode.Attribute("AMax").Value, CultureInfo.InvariantCulture);
                                    definition.colors.Add(color);
                                }
                                Particles.ParticleHost.particleDefinitions.Add(int.Parse(subNode.Attribute("ID").Value, CultureInfo.InvariantCulture), definition);
                            }
                            else if (subNode.Name.LocalName == "ParticleBound")
                            {
                                Particles.ParticleBound bound = new Particles.ParticleBound(
                                    float.Parse(subNode.Attribute("X").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("Y").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("Width").Value, CultureInfo.InvariantCulture),
                                    float.Parse(subNode.Attribute("Height").Value, CultureInfo.InvariantCulture));
                                if (subNode.Element("InitialFill") != null)
                                {
                                    XElement initialFill = subNode.Element("InitialFill");
                                    foreach (XElement iFillSubNode in initialFill.Elements())
                                    {
                                        if (iFillSubNode.Name.LocalName == "RandomGrid")
                                        {
                                            for (float i = float.Parse(iFillSubNode.Attribute("X").Value, CultureInfo.InvariantCulture);
                                                i <= float.Parse(iFillSubNode.Attribute("Width").Value, CultureInfo.InvariantCulture);
                                                i += float.Parse(iFillSubNode.Attribute("XSize").Value, CultureInfo.InvariantCulture))
                                            {
                                                for (float j = float.Parse(iFillSubNode.Attribute("Y").Value, CultureInfo.InvariantCulture);
                                                j <= float.Parse(iFillSubNode.Attribute("Height").Value, CultureInfo.InvariantCulture);
                                                j += float.Parse(iFillSubNode.Attribute("YSize").Value, CultureInfo.InvariantCulture))
                                                {
                                                    Particles.Particle particle = new Particles.Particle(
                                                        int.Parse(iFillSubNode.Attribute("ParticleID").Value, CultureInfo.InvariantCulture),
                                                        new Vector2(i, j));
                                                    bound.particles.Add(particle);
                                                }
                                            }
                                        }
                                    }
                                    Particles.ParticleHost.particleBounds.Add(subNode.Attribute("Name").Value, bound);
                                }
                            }
                        }
                    }
                }
                SceneManager.scenes.Add(sceneName, scene);
            }
            Game.State.gameState = Game.State.GameStates.Running;
        }
    }
}