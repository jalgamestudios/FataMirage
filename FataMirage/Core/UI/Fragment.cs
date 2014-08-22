using Microsoft.Xna.Framework.Graphics;
using SharpDX;
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
        public Graphics.Texture texture;
        public float depth;
        public SideClick sideClick;
        public List<IControl> controls;
        public float x
        {
            get
            {
                switch (horizontalAlign)
                {
                    case HorizontalAligns.Left: return 0;
                    case HorizontalAligns.Center: return 0.5f - width / 2; // 50% of the screen width - 50% of the fragment's width
                    case HorizontalAligns.Right: return 1 - width;
                }
                return 0;
            }
        }
        public float y
        {
            get
            {
                switch (verticalAlign)
                {
                    case VerticalAligns.Top: return 0;
                    case VerticalAligns.Center: return (0.5f / Graphics.Settings.renderAspectRatio) - height / 2;
                    case VerticalAligns.Bottom: return 1 - height;
                }
                return 0;
            }
        }
        public RectangleF bounds
        {
            get
            {
                return new RectangleF(x, y, width, height);
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
        public enum SideClick
        {
            LetThrough,
            Close,
            Block
        }
        public static SideClick createSideClick(string value)
        {
            switch (value)
            {
                case "LetThrough": return SideClick.LetThrough;
                case "Close": return SideClick.Close;
                case "Block": return SideClick.Block;
            }
            return SideClick.Block;
        }
        public Fragment(float width, float height,
            HorizontalAligns horizontalAlign, VerticalAligns verticalAlign,
            Graphics.Texture texture, SideClick sideClick)
        {
            this.width = width;
            this.height = height;
            this.horizontalAlign = horizontalAlign;
            this.verticalAlign = verticalAlign;
            this.texture = texture;
            this.sideClick = sideClick;
            this.controls = new List<IControl>();
            createClickHandler();
        }
        public Fragment(float width, float height,
            HorizontalAligns horizontalAlign, VerticalAligns verticalAlign,
            string textureName, SideClick sideClick)
        {
            this.width = width;
            this.height = height;
            this.horizontalAlign = horizontalAlign;
            this.verticalAlign = verticalAlign;
            this.texture = new Graphics.Texture(textureName);
            this.sideClick = sideClick;
            this.controls = new List<IControl>();
            createClickHandler();
        }
        void createClickHandler()
        {
            Input.ClickLayerManager.clickLayers.Add(new Input.ClickLayer(
                depth, (x, y) =>
                {
                    if (visible)
                    {
                        x = Graphics.Scaler.screenToWorld(x);
                        y = Graphics.Scaler.screenToWorld(y);
                        if (bounds.Contains(x, y))
                        {
                            foreach (var control in controls)
                            {
                                if (new RectangleF(control.bounds.X + this.x, control.bounds.Y + this.y, 
                                    control.bounds.Width, control.bounds.Height).Contains(x, y))
                                {
                                    control.clicked(x - this.x, y - this.y);
                                }
                            }
                            return true;
                        }
                        else
                        {
                            switch (sideClick)
                            {
                                case SideClick.Block: return true; //No, you won't get through here!
                                case SideClick.Close: visible = false; return true; //Close the fragment, but block the tap, because it was only intended to close the popup, not to interact with the scene
                                case SideClick.LetThrough: return false; //Just let it through...
                            }
                        }
                    }
                    //The fragment isn't visible, so it can't be clicked
                    return false;
                }));
        }
        public void Update(float elapsedTime)
        {

        }
        public void Draw(float elapsedTime)
        {
            if (visible)
            Graphics.Scaler.Draw(texture, x, y, width, height, 0.2f);
        }
    }
}
