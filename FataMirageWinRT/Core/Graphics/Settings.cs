using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Graphics
{
    static class Settings
    {
        public static int actualScreenWidth = 640;
        public static int actualScreenHeight = 480;
        public static float actualAspectRatio
        {
            get { return (float)actualScreenWidth / actualScreenHeight; }
        }
        public static int renderWidth = 640;
        public static int renderHeight = 480;
        public static float renderAspectRatio
        {
            get { return (float)renderWidth / renderHeight;}
        }
        public static bool useDynaScale = true;
        public static bool interpolateSmooth = true;
    }
}
