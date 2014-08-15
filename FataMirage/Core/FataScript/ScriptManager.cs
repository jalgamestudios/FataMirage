using System;
using System.Collections.Generic;
using System.Text;

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
                if (line.StartsWith("#") || line.Trim() == "")
                    continue;
                linesPrecompiled.Add(line.Trim());

            }
        }
        public static void AddVar(string name, string value)
        {

        }
    }
}
