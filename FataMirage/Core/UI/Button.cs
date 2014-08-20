using SharpDX;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.UI
{
    class Button
    {
        public RectangleF bounds;
        public string onClickScriptName;
        public Button(string name, RectangleF bounds, string onClickScript)
        {
            this.bounds = bounds;
            FataScript.ScriptManager.AddScript("UI->Button->" + name + "->OnClick",
                onClickScript);
            this.onClickScriptName = "UI->Button->" + name + "->OnClick";
        }
        public Button(string name, float x, float y, float width, float height, string onClickScript)
        {
            this.bounds = new RectangleF(x, y, width, height);
            FataScript.ScriptManager.AddScript("UI->Button->" + name + "->OnClick",
                onClickScript);
            this.onClickScriptName = "UI->Button->" + name + "->OnClick";
        }
    }
}
