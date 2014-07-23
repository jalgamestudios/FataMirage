using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Graphics
{
    static class Utilities
    {
        public static void DrawFullScreen(Texture texture, float depth)
        {
            Scaler.Draw(texture.texture, 0, 0, 1, 1 / Settings.renderAspectRatio, depth);
        }
    }
}
