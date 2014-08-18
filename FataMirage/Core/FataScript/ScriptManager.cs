using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Globalization;
using System.Threading.Tasks;

namespace FataMirage.Core.FataScript
{
    static class ScriptManager
    {
        public static Dictionary<string, string> scripts = new Dictionary<string, string>();
        public static Dictionary<string, FataVar> vars = new Dictionary<string, FataVar>();
        public static Dictionary<string, IFunction> functions = new Dictionary<string, IFunction>();
        public static void AddScript(string name, string scriptContent)
        {
            scripts.Add(name, scriptContent);
        }
        public static void ExecuteScript(string name)
        {
            //Makes the script run on a new thread so it can run independently of the core game
            Task.Run(new Action(() =>
                {
                    string[] lines = scripts[name].Split(Environment.NewLine.ToCharArray());
                    List<string> linesPrecompiled = new List<string>();
                    foreach (string line in lines)
                    {
                        if (line.Trim().StartsWith("#") || line.Trim() == "")
                            continue;
                        linesPrecompiled.Add(line.Trim());
                    }
                    for (int i = 0; i < linesPrecompiled.Count; i++)
                    {
                        List<string> arguments = linesPrecompiled[i].Split(' ').ToList();
                        string command = arguments[0];
                        arguments.RemoveAt(0);
                        if (command == "if")
                        {
                            if (FataSharpVarProvider.GetVar(arguments[0]) == arguments[1])
                            {
                                while (lines[i] != arguments[2] && i < linesPrecompiled.Count)
                                    i++;
                                continue;
                            }
                        }
                        else if (command == "ifn")
                        {
                            if (FataSharpVarProvider.GetVar(arguments[0]) != arguments[1])
                            {
                                while (lines[i] != arguments[2] && i < linesPrecompiled.Count)
                                    i++;
                                continue;
                            }
                        }
                        else if (command == "fadelayer")
                        {
                            (Scene.SceneManager.currentScene.layers[arguments[0]] as
                                Scene.Layers.ImageLayer).opacity = 
                                float.Parse(arguments[1], CultureInfo.InvariantCulture);
                        }
                        else if (command == "additem")
                        {
                            Player.Inventory.Items.items[arguments[0]].OnStage = true;
                            Player.Inventory.Items.items[arguments[0]].initialPosition = new Vector2(+
                                float.Parse(arguments[1], CultureInfo.InvariantCulture),
                                float.Parse(arguments[2], CultureInfo.InvariantCulture));
                            Player.Inventory.Items.items[arguments[0]].linearProgress = 0;
                        }
                        else if (command == "wait")
                        {
                            //Convert the given time which is measured in seconds to milliseconds
                            System.Threading.Thread.Sleep((int)(float.Parse(arguments[0], CultureInfo.InvariantCulture) * 1000));
                        }
                        else if (command == "moveitem")
                        {
                            Player.Inventory.Items.items[arguments[0]].currentGoal =
                                new Vector2(float.Parse(arguments[1], CultureInfo.InvariantCulture),
                                    float.Parse(arguments[2], CultureInfo.InvariantCulture));
                            Player.Inventory.Items.items[arguments[0]].linearProgress = 0;
                            Player.Inventory.Items.items[arguments[0]].itemState = Player.Inventory.Item.ItemStates.ToScene;
                        }
                        else if (command == "removeitem")
                        {
                            Player.Inventory.Items.items[arguments[0]].OnStage = false;
                        }
                    }
                }));
        }
        public static void AddVar(string name, string value)
        {

        }
        /// <summary>
        /// Pauses the current htread for the given time
        /// </summary>
        /// <param name="ms">The time, measured in milliseconds</param>
        static void Sleep(int ms)
        {
            new System.Threading.ManualResetEvent(false).WaitOne(ms);
        }
    }
}
