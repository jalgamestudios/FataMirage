using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Scene.Layers
{
    /// <summary>
    /// This layer displays a static image with the size of the window at a given opacity
    /// </summary>
    class ImageLayer : ILayer
    {
        /// <summary>
        /// The texture the ImageLayer will display
        /// </summary>
        Graphics.Texture texture;
        /// <summary>
        /// Weather the layer is visible (this will use opacity to get and set values)
        /// </summary>
        public bool visible;
        /// <summary>
        /// How opaque the layer is (0 = invisible, 1 = completely opaque)
        /// </summary>
        public float opacity;
        /// <summary>
        /// Creates a new image layer that is fully visible
        /// </summary>
        /// <param name="texture">The texture the layer should display</param>
        /// <param name="depth">The z-position of the layer</param>
        public ImageLayer(Graphics.Texture texture, float depth)
        {
            this._zPos = depth;
            this.texture = texture;
            this.opacity = 1;
        }
        /// <summary>
        /// Created a new image layer and loads the texture at the given location
        /// </summary>
        /// <param name="fileName">The name of the texture the layer should display</param>
        /// <param name="depth">The z-position of the layer</param>
        public ImageLayer(string fileName, float depth)
        {
            this._zPos = depth;
            this.texture = new Graphics.Texture(fileName);
            this.opacity = 1;
        }
        /// <summary>
        /// Creates a new image layer that is fully visible
        /// </summary>
        /// <param name="texture">The texture the layer should display</param>
        /// <param name="depth">The z-position of the layer</param>
        /// <param name="opacity">The opacity of the layer</param>
        public ImageLayer(Graphics.Texture texture, float depth, float opacity)
        {
            this._zPos = depth;
            this.texture = texture;
            this.opacity = opacity;
        }
        /// <summary>
        /// Created a new image layer and loads the texture at the given location
        /// </summary>
        /// <param name="fileName">The name of the texture the layer should display</param>
        /// <param name="depth">The z-position of the layer</param>
        /// <param name="opacity">The opacity of the layer</param>
        public ImageLayer(string fileName, float depth, float opacity)
        {
            this._zPos = depth;
            this.texture = new Graphics.Texture(fileName);
            this.opacity = opacity;
        }
        /// <summary>
        /// Exposes the update method
        /// </summary>
        /// <param name="elapsedTime">The time in seconds since the last time this method was called (= the last frame)</param>
        void ILayer.Update(float elapsedTime) { _update(elapsedTime); }
        /// <summary>
        /// Exposes the draw method
        /// </summary>
        /// <param name="elapsedTime">The time in seconds since the last time this method was called (= the last frame)</param>
        void ILayer.Draw(float elapsedTime) { _draw(elapsedTime); }
        /// <summary>
        /// Checks if the layer is opaque at the given position
        /// </summary>
        /// <param name="x">The x position, measured in world units</param>
        /// <param name="y">The y position, measured in world units</param>
        /// <returns>True if the layer is opaque, otherwise false</returns>
        bool ILayer.collisionDetect(float x, float y){return _collisionDetect(x, y);}
        /// <summary>
        /// Returns the position on the z axis. The higher it is, the farther it is back
        /// </summary>
        float ILayer.zPos { get { return _zPos; } set { _zPos = value; } }
        /// <summary>
        /// Updates the layer
        /// </summary>
        /// <param name="elapsedTime">The time since the last call of this method, measured in seconds</param>
        void _update(float elapsedTime)
        {
            
        }
        /// <summary>
        /// Draws the layer
        /// </summary>
        /// <param name="elapsedTime">The time since the last call of this method, measured in seconds</param>
        void _draw(float elapsedTime)
        {
            if (visible)
                Graphics.Utilities.DrawFullScreen(texture, _zPos, opacity);
        }
        /// <summary>
        /// Checks if the layer is opaque at the given position
        /// </summary>
        /// <param name="x">The x position, measured in world units</param>
        /// <param name="y">The y position, measured in world units</param>
        /// <returns>True if the layer is opaque, otherwise false</returns>
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
