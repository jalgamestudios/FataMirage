using Microsoft.Xna.Framework;
//Please note:
//This class currently may look sense-less, but more features like animations will be added later on.
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Graphics
{
    class Texture
    {
        public Texture2D texture;
        public Texture(string fileName)
        {
            texture = Stator.contentManager.Load<Texture2D>(fileName);
        }
        public Texture(Texture2D texture)
        {
            this.texture = texture;
        }
        public Color GetPixel(int x, int y)
        {
            return GetPixels()[x + (y * texture.Width)];
        }
        Color[] GetPixels()
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(colors1D);
            return colors1D;
        }
    }
}
