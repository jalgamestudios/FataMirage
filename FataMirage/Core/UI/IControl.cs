using SharpDX;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.UI
{
    interface IControl
    {
        RectangleF bounds { get; set; }
        void clicked(float relX, float relY);
        void update(float elapsedTime);
        void draw(float elaspedTime);
    }
}
