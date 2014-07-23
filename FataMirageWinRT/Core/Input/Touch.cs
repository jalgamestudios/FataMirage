using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Input
{
    class Touch
    {
        public static void Init()
        {
        }
        public static void LoadContent()
        {

        }
        public static void Update(float elapsedTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                switch (touchCollection[0].State)
                {
                    case TouchLocationState.Pressed:
                    case TouchLocationState.Moved:
                        InputManager.pointerX = (int)touchCollection[0].Position.X;
                        InputManager.pointerY = (int)touchCollection[0].Position.Y;
                        break;
                    case TouchLocationState.Released:
                        InputManager.pointerState = InputManager.PointerStates.Click;
                        InputManager.pointerX = (int)touchCollection[0].Position.X;
                        InputManager.pointerY = (int)touchCollection[0].Position.Y;
                        break;
                }
            }
        }
        public static void Draw(float elapsedTime)
        {
        }
    }
}
