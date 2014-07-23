using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Game
{
    class State
    {
        public static GameStates gameState = GameStates.LoadingScenes;
        public enum GameStates
        {
            LoadingScenes,
            Running
        }
    }
}
