using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FataMirage.Core.Input
{
    static class ClickLayerManager
    {
        public static List<ClickLayer> clickLayers = new List<ClickLayer>();
        public static void Update()
        {
            if (InputManager.pointerState == InputManager.PointerStates.Click)
            {
                foreach (var clickLayer in clickLayers.OrderByDescending(cl => cl.zIndex))
                {
                    if (clickLayer.checkClick(InputManager.pointerX, InputManager.pointerY))
                        break;
                }
                InputManager.pointerState = InputManager.PointerStates.Hover;
            }
        }
    }
    class ClickLayer
    {
        public float zIndex;
        public delegate bool ClickDelegate(float x, float y);
        public ClickDelegate checkClick;
        public ClickLayer(float zIndex, ClickDelegate checkClick)
        {
            this.zIndex = zIndex;
            this.checkClick = checkClick;
        }
    }
}
