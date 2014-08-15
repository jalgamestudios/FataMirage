using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Input
{
    static class InputManager
    {
        public static PointerStates pointerState;
        public enum PointerStates
        {
            DontDraw,
            Hover,
            Click
        }
        public  static int pointerX;
        public static int pointerY;
        public static void Init()
        {
        }
        public static void LoadContent()
        {

        }
        public static void Update(float elapsedTime)
        {
            //Let the click only last for one frame.
            if (pointerState == PointerStates.Click)
                pointerState = PointerStates.Hover;
            Touch.Update(elapsedTime);
            Desktop.Update(elapsedTime);
            Gamepad.Update(elapsedTime);
            ClickLayerManager.Update();
        }
        public static void Draw(float elapsedTime)
        {
        }
    }
}
