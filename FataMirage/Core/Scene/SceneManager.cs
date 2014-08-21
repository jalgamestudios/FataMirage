using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Scene
{
    static class SceneManager
    {
        public static Dictionary<string, SceneState> scenes;
        public static string currentSceneName;
        public static SceneState currentScene
        {
            get { return scenes[currentSceneName]; }
        }
        public static void Init()
        {
            Input.ClickLayerManager.clickLayers.Add(new Input.ClickLayer(0.4f, (x, y) =>
                {
                    foreach (var hotspot in currentScene.waypoints.waypoints[Player.Legs.currentPositionName].hotspots)
                    {
                        if (hotspot.clicked(Graphics.Scaler.screenToWorld(x), Graphics.Scaler.screenToWorld(y)))
                        {
                            FataScript.ScriptManager.ExecuteScript(hotspot.scriptName);
                            return true;
                        }
                    }
                        return false;
                }));
        }
        public static void LoadContent()
        {

        }
        public static void Update(float elapsedTime)
        {
            currentScene.Update(elapsedTime);
        }
        public static void Draw(float elapsedTime)
        {
            currentScene.Draw(elapsedTime);
        }
    }
}
