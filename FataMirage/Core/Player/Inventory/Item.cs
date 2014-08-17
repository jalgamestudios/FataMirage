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
        /// <summary>
        /// Creates a new item with the given position and texture
        /// </summary>
        /// <param name="initialPosition">The position. This may be overwritten when the item is actually spawned and therefore can easily be (0,0)</param>
        /// <param name="texture">The texture of the item</param>
        public Item(Vector2 initialPosition, Graphics.Texture texture)
        {
            this.initialPosition = initialPosition;
            this.texture = texture;
        }

        /// <summary>
        /// The position where the item was spawned in the scene
        /// </summary>
        /// 
        public Vector2 initialPosition;
        /// <summary>
        /// The position towards the item is currently moving
        /// </summary>
        public Vector2 currentGoal;

        /// <summary>
        /// Calculates the current position of the item, based on initialPosition, currentGoal and progress
        /// </summary>
        public Vector2 currentPosition
        {
            get
            {
                return initialPosition * (1 - progress) +
                    currentGoal * progress;
            }
        }

        /// <summary>
        /// The progress of the item towards it's goal. This is the liearProgress, but processed by the smooth step algorithm
        /// </summary>
        public float progress
        {
            get
            {
                //Implements the smooth-step algorythm
                return (float)(3 * Math.Pow(linearProgress, 2) -
                    2 * Math.Pow(linearProgress, 3));
            }
        }

        /// <summary>
        /// The linear progress of the item towards its goal
        /// </summary>
        public float linearProgress;

        /// <summary>
        /// The texture of the item
        /// </summary>
        public Graphics.Texture texture;

        //TODO: Find a better name!
        /// <summary>
        /// Weather the item is visible in the scene
        /// </summary>
        public bool OnStage;

        /// <summary>
        /// Updates the item and moves it towards its goal
        /// </summary>
        /// <param name="elapsedTime">The time since this method was called the last time, emasured in seconds</param>
        /// <param name="inInventoryPosition">The position of the item in the inventory. 
        /// This will be set as the currentGoal of the item. 
        /// Changing this while the item is still moving might result in weird results</param>
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
