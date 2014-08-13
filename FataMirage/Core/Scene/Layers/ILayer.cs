using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Scene.Layers
{
    interface ILayer
    {
        float zPos
        {
            get;
            set;
        }
        void Update(float elapsedTime);
        bool collisionDetect(float x, float y);
        void Draw(float elapsedTime);
    }
}
