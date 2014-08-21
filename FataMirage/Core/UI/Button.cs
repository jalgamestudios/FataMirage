using SharpDX;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.UI
{
    class Button : IControl
    {
        RectangleF IControl.bounds { get { return boundInteral; } set { boundInteral = value; } }
        RectangleF boundInteral;
        public string onClickScriptName;
        public Button(string name, RectangleF _bounds, string onClickScript)
        {
            boundInteral = _bounds;
            FataScript.ScriptManager.AddScript("UI->Button->" + name + "->OnClick",
                onClickScript);
            this.onClickScriptName = "UI->Button->" + name + "->OnClick";
        }
        public Button(string name, float x, float y, float width, float height, string onClickScript)
        {
            this.boundInteral = new RectangleF(x, y, width, height);
            FataScript.ScriptManager.AddScript("UI->Button->" + name + "->OnClick",
                onClickScript);
            this.onClickScriptName = "UI->Button->" + name + "->OnClick";
        }

        void IControl.clicked(float relX, float relY)
        {
            FataScript.ScriptManager.ExecuteScript(onClickScriptName);
        }

        void IControl.update(float elapsedTime)
        {
            
        }

        void IControl.draw(float elaspedTime)
        {
        }
    
}
}
