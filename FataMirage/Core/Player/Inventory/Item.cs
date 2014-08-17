using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Player.Inventory
{
    /// <summary>
    /// Represents an item in the inventory
    /// </summary>
    class Item
    {
        public Item(Vector2 initialPosition, Graphics.Texture texture)
        {
            this.initialPosition = initialPosition;
            this.texture = texture;
        }
        public Vector2 initialPosition;
        public Vector2 currentGoal;
        public Vector2 currentPosition
        {
            get
            {
                return initialPosition * (1 - progress) +
                    currentGoal * progress;
            }
        }
        public float progress
        {
            get
            {
                //Implements the smooth-step algorythm
                return (float)(3 * Math.Pow(linearProgress, 2) -
                    2 * Math.Pow(linearProgress, 3));
            }
        }
        public float linearProgress;
        public Graphics.Texture texture;
        //TODO: Find a better name!
        public bool OnStage;

        public void Update(float elapsedTime, Vector2 inInventoryPosition)
        {
            if (OnStage)
            {
                if (linearProgress < 1)
                {
                    linearProgress += elapsedTime;
                    if (linearProgress > 1)
                        linearProgress = 1;
                }
                currentGoal = inInventoryPosition;
            }
        }
    }
}
