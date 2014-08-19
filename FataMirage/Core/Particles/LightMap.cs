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

            texture.Dispose();
        }
        public static Color getColorAtPixel(int x, int y)
        {
            return colors[x + y * lightmapWidth];
        }
        public static Color getColorAtWorldspace(float x, float y)
        {
            return getColorAtPixel((int)(x / lightmapWidth), (int)(y / lightmapHeight));
        }
    }
}
