using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Player.Inventory
{
    class InventoryManager
    {
        public static Graphics.Texture leftSide, rightSide, itemHighlight, noItem;
        public static float inventoryShowed = 0;
        public static float inventoryShowedGoal = 1;
        public static float inventoryShowSpeed = 4;
        public static int inventoryHeight = 8 / 90;
        public static void Init()
        {
        }
        public static void LoadContent()
        {
            //leftSide = new Graphics.Texture("");
        }
        public static void Update(float elapsedTime)
        {
            if (inventoryShowedGoal < inventoryShowed)
            {
                inventoryShowed -= inventoryShowSpeed * elapsedTime;
                if (inventoryShowed >= inventoryShowedGoal)
                    inventoryShowed = inventoryShowedGoal;
            }
            else if (inventoryShowedGoal > inventoryShowed)
            {
                inventoryShowed += inventoryShowSpeed * elapsedTime;
                if (inventoryShowed <= inventoryShowedGoal)
                    inventoryShowed = inventoryShowedGoal;
            }
        }
        public static void Draw(float elapsedTime)
        {
            //Graphics.Scaler.Draw()
        }
    }
}
