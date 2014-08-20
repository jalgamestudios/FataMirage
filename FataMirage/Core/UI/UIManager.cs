using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.UI
{
    static class UIManager
    {
        public static List<Fragment> fragments;
        public static void Init()
        {
            fragments = new List<Fragment>();
        }
        public static void Update(float elapsedTime)
        {
            foreach (var fragment in fragments)
                fragment.Update(elapsedTime);
        }
        public static void Draw(float elapsedTime)
        {
            foreach (var fragment in fragments)
                fragment.Draw(elapsedTime);
        }
    }
}
