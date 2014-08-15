using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.FataScript.BuiltIn
{
    class Cutscene : IFunction
    {
        string IFunction.call(string[] parameters)
        {
            return "true";
        }
    }
}
