using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Scene.Layers
{
    class ImageLayer : ILayer
    {
        Graphics.Texture texture;
        public bool visible;
        public float opacity;
        public ImageLayer(Graphics.Texture texture, float depth)
        {
            this._zPos = depth;
            this.texture = texture;
            this.visible = true;
            this.opacity = 1;
        }
        public ImageLayer(string fileName, float depth)
        {
            this._zPos = depth;
            this.texture = new Graphics.Texture(fileName);
            this.opacity = 1;
        }
        public ImageLayer(Graphics.Texture texture, float depth, float opacity)
        {
            this._zPos = depth;
            this.texture = texture;
            this.visible = true;
            this.opacity = opacity;
        }
        public ImageLayer(string fileName, float depth, float opacity)
        {
            this._zPos = depth;
            this.texture = new Graphics.Texture(fileName);
            this.opacity = opacity;
        }
        void ILayer.Update(float elapsedTime) { _update(elapsedTime); }
        void ILayer.Draw(float elapsedTime) { _draw(elapsedTime); }
        bool ILayer.collisionDetect(float x, float y){return _collisionDetect(x, y);}
        float ILayer.zPos { get { return _zPos; } set { _zPos = value; } }
        void _update(float elapsedTime)
        {
            
        }
        void _draw(float elapsedTime)
        {
            if (visible)
                Graphics.Utilities.DrawFullScreen(texture, _zPos, opacity);
        }
        bool _collisionDetect(float x, float y)
        {
            if (!visible)
                return false;
            x *= texture.texture.Width;
            y *= texture.texture.Width;
            if (x >= 0 && y >= 0 && x < texture.texture.Width && y < texture.texture.Height)
            {
                Color color = texture.GetPixel((int)x, (int)y);
                if (color.A > 0)
                {
                    visible = false;
                    return true;
                }
            }
            return false;
        }

        float _zPos;
    }
}
