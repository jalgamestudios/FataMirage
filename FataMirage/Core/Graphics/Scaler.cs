using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Graphics
{
    class Scaler
    {
        public static void Draw(Texture2D texture, float x, float y, float width, float height, float depth)
        {
            Draw(texture, x, y, width, height, Color.White, depth);
        }
        public static void Draw(Texture2D texture, float x, float y, float width, float height, Color color, float depth)
        {
            if (Settings.actualScreenWidth / Settings.actualScreenHeight < (Settings.renderWidth / (float)Settings.renderHeight))
            {
                Stator.spriteBatch.Draw(texture, new Rectangle((int)(x * Settings.actualScreenWidth),
                    (int)((Settings.actualScreenHeight - Settings.actualScreenWidth / (Settings.renderWidth / (float)Settings.renderHeight)) / 2 + y * Settings.actualScreenWidth),
                    (int)(width * Settings.actualScreenWidth) + 1,
                    (int)(height * Settings.actualScreenWidth) + 1),
                    null,
                    color,
                    0,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    depth);
            }
            else
            {
                Stator.spriteBatch.Draw(texture, new Rectangle((int)((Settings.actualScreenWidth - Settings.actualScreenHeight * (Settings.renderWidth / (float)Settings.renderHeight)) / 2 + x * Settings.actualScreenHeight),
                    (int)(y * Settings.actualScreenHeight),
                    (int)(width * Settings.actualScreenHeight) + 1,
                    (int)(height * Settings.actualScreenHeight) + 1),
                    null,
                    color,
                    0,
                    new Vector2(0, 0),
                    SpriteEffects.None,
                    depth);
            }
        }
        public static Vector2 screenToWorld(float x, float y)
        {
            return screenToWorld(new Vector2(x, y));
        }
        public static Vector2 screenToWorld(Vector2 pixelPosition)
        {
            if (Settings.actualScreenWidth / Settings.actualScreenHeight < Settings.renderAspectRatio)
            {
                return new Vector2(pixelPosition.X / Settings.actualScreenWidth, (pixelPosition.Y - (Settings.actualScreenHeight - Settings.actualScreenWidth / Settings.renderAspectRatio) / 2) / Settings.actualScreenWidth);
            }
            else
            {
                return new Vector2((pixelPosition.X - (Settings.actualScreenWidth - Settings.actualScreenHeight * Settings.renderAspectRatio) / 2) / Settings.actualScreenHeight, pixelPosition.Y / Settings.actualScreenHeight);
            }
        }
    }
}
