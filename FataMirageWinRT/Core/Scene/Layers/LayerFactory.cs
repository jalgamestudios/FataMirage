using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Scene.Layers
{
    class LayerFactory
    {
        public static ILayer createImageLayer(string imageUri, float depth)
        {
            return new ImageLayer(new Graphics.Texture(imageUri), depth);
        }
    }
}
