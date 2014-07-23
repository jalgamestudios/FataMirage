using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Player
{
    static class TextureProvider
    {
        public static Graphics.Texture currentTexture
        {
            get
            {
                if (Legs.isWalking)
                {
                    
                    switch (Legs.direction)
                    {
                        case Legs.Directions.Top: return walkingTop[
                            Math.Min((int)(Legs.timeWalking % walkingCycleLength / walkingCycleLength * walkingTop.Count), walkingTop.Count)];

                        case Legs.Directions.Left: return walkingLeft[
                            Math.Min((int)(Legs.timeWalking % walkingCycleLength / walkingCycleLength * walkingLeft.Count), walkingLeft.Count)];

                        case Legs.Directions.Bottom: return walkingBottom[
                            Math.Min((int)(Legs.timeWalking % walkingCycleLength / walkingCycleLength * walkingBottom.Count), walkingBottom.Count)];

                        case Legs.Directions.Right: return walkingRight[
                            Math.Min((int)(Legs.timeWalking % walkingCycleLength / walkingCycleLength * walkingRight.Count), walkingRight.Count)];
                    }
                    //See comment below:)
                    return standingFront;
                }
                else
                {
                    switch (Legs.direction)
                    {
                        case Legs.Directions.Top: return standingBack;
                        case Legs.Directions.Left: return standingLeft;
                        case Legs.Directions.Bottom: return standingFront;
                        case Legs.Directions.Right: return standingRight;
                    }
                    //Will never be reached, but the C# compiler apparently isn't that intelligent...
                    return standingFront;
                }
            }
        }
        public static Graphics.Texture standingFront, standingLeft, standingBack, standingRight;
        public static float walkingCycleLength = 0.8f;
        public static List<Graphics.Texture> walkingLeft, walkingRight, walkingTop, walkingBottom;
    }
}
