using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.Particles
{
    static class LightMap
    {
        static Color[] colors;
        static int lightmapWidth, lightmapHeight;
        public static void Load(string contentFileName)
        {
            Texture2D texture = Stator.contentManager.Load<Texture2D>(contentFileName);
            colors = new Color[texture.Width * texture.Height];
            texture.GetData(colors);
            lightmapWidth = texture.Width;
            lightmapHeight = texture.Height;
            texture.Dispose();
        }
        public static Color getColorAtPixel(int x, int y)
        {
            int actualX = Math.Max(0, Math.Min(x, lightmapWidth));
            int actualY = Math.Max(0, Math.Min(y, lightmapHeight));
            return colors[Math.Max(0, Math.Min(x, lightmapWidth - 1)) + Math.Max(0, Math.Min(y, lightmapHeight - 1)) * lightmapWidth];
        }
        public static Color getColorAtWorldspace(float x, float y)
        {
            return getColorAtPixel((int)(x * lightmapWidth), (int)(y * lightmapWidth));
        }
    }
}
