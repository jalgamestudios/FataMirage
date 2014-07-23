using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Scene
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
            scenes = new Dictionary<string, SceneState>();
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
