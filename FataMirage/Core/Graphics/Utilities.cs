using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Graphics
{
    static class Utilities
    {
        public static void DrawFullScreen(Texture texture, float depth)
        {
            Scaler.Draw(texture, 0, 0, 1, 1 / Settings.renderAspectRatio, depth);
        }
        public static void DrawFullScreen(Texture texture, float depth, float opacity)
        {
            Scaler.Draw(texture, 0, 0, 1, 1 / Settings.renderAspectRatio, new Color(1f, 1f, 1f, opacity), depth);
        }
    }
}
