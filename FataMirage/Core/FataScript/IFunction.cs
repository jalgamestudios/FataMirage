using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.FataScript
{
    interface IFunction
    {
        string call(string[] parameters);
    }
}
