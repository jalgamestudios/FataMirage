using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Graphics
{
    static class Manager
    {
        public static void StartDraw()
        {
            Stator.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
        }
        public static void EndDraw()
        {
            Stator.spriteBatch.End();
        }
    }
}
