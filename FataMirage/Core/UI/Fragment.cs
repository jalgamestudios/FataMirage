using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.UI
{
    class Fragment
    {
        public bool visible;
        public float width;
        public float height;
        public HorizontalAligns horizontalAlign;
        public VerticalAligns verticalAlign;
        public Texture2D texture;
        public float x
        {
            get
            {
                return 0;
            }
        }
        public float y
        {
            get
            {
                return 0;
            }
        }
        public enum HorizontalAligns
        {
            Left,
            Center,
            Right,
        }
        public static HorizontalAligns createHorizontalAlign(string value)
        {
            switch (value)
            {
                case "Left": return HorizontalAligns.Left;
                case "Center": return HorizontalAligns.Center;
                case "Right": return HorizontalAligns.Right;
            }
            return HorizontalAligns.Center;
        }
        public enum VerticalAligns
        {
            Top,
            Center,
            Bottom,
        }
        public static VerticalAligns createVerticalAlign(string value)
        {
            switch (value)
            {
                case "Top": return VerticalAligns.Top;
                case "Center": return VerticalAligns.Center;
                case "Bottom": return VerticalAligns.Bottom;
            }
            return VerticalAligns.Center;
        }
        public Fragment(float width, float height,
            HorizontalAligns horizontalAlign, VerticalAligns verticalAlign,
            Texture2D texture)
        {
            this.width = width;
            this.height = height;
            this.horizontalAlign = horizontalAlign;
            this.verticalAlign = verticalAlign;
            this.texture = texture;
        }
        public Fragment(float width, float height,
            HorizontalAligns horizontalAlign, VerticalAligns verticalAlign,
            string textureName)
        {
            this.width = width;
            this.height = height;
            this.horizontalAlign = horizontalAlign;
            this.verticalAlign = verticalAlign;
            this.texture = Stator.contentManager.Load<Texture2D>(textureName);
        }
        public void Update(float elapsedTime)
        {

        }
        public void Draw(float elapsedTime)
        {
            Graphics.Scaler.Draw(texture, x, y, width, height, 0.2f);
        }
    }
}
