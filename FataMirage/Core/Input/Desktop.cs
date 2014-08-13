using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Input
{
    class Desktop
    {
        static MouseState lastMouseState = new MouseState();
        public static void Init()
        {
        }
        public static void LoadContent()
        {

        }
        public static void Update(float elapsedTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Released &&
                lastMouseState.LeftButton == ButtonState.Pressed)
            {
                InputManager.pointerState = InputManager.PointerStates.Click;
                InputManager.pointerX = currentMouseState.X;
                InputManager.pointerY = currentMouseState.Y;
            }
            lastMouseState = currentMouseState;
        }
        public static void Draw(float elapsedTime)
        {
        }
    }
}
