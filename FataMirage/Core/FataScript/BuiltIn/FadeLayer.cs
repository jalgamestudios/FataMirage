using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.FataScript.BuiltIn
{
    class FadeLayer : IFunction
    {
        string IFunction.call(string[] parameters)
        {
            return "true";
        }
    }
}
