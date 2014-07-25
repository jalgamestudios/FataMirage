using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Debugging
{
    class Logger
    {
        /// <summary>
        /// A list of the different imporances of the warnings
        /// </summary>
        public enum Importance
        {
            /// <summary>
            /// The engine is only outputting information, no need to worry
            /// </summary>
            Information,
            /// <summary>
            /// Something might go wrong
            /// </summary>
            Warning,
            /// <summary>
            /// Something went wrong, but the game can continue to run
            /// </summary>
            Error,
            /// <summary>
            /// Something went so terribly wrong that the game has to stop.
            /// </summary>
            Break
        }
        public static void Warn(string Title, string Details, Importance Importance)
        {
            //TODO: Implement the actual warnings
        }
    }
}
