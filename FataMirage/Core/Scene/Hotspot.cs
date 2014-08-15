using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.Scene
{
    class Hotspot
    {
        public float y;
        public float x;
        public float radius;
        public string scriptName;
        public Hotspot(float x, float y, float radius, string scriptName)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.scriptName = scriptName;
        }
        public bool clicked(float clickX, float clickY)
        {
            if ((new Vector2(x, y) - new Vector2(clickX, clickY)).Length() <= radius)
                return true;
            return false;
        }
    }
}
