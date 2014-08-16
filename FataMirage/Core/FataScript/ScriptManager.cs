using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
                arguments.RemoveAt(0);
                if (linesPrecompiled[i].StartsWith("if"))
                {
                    if (FataSharpVarProvider.GetVar(arguments[0]) == arguments[1])
                    {
                        while (arguments[i] != arguments[2] && i < linesPrecompiled.Count)
                            i++;
                        continue;
                    }
                }
                else if (linesPrecompiled[i].StartsWith("ifn"))
                {
                    if (FataSharpVarProvider.GetVar(arguments[0]) != arguments[1])
                    {
                        while (arguments[i] != arguments[2] && i < linesPrecompiled.Count)
                            i++;
                        continue;
                    }
                }
                else if (linesPrecompiled[i].StartsWith("fadelayer"))
                {
                    (Scene.SceneManager.currentScene.layers[arguments[0]] as
                        Scene.Layers.ImageLayer).opacity = 0;
                }
            }
        }
        public static void AddVar(string name, string value)
        {

        }
    }
}
