using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Scene
{
    class SceneState
    {
        public Dictionary<string, Layers.ILayer> layers = new Dictionary<string, Layers.ILayer>();
        public IOrderedEnumerable<KeyValuePair<string, Layers.ILayer>> layersDepthSorted
        {
            get
            {
                return layers.OrderBy(kvp => kvp.Value.zPos);
            }
        }
        public Path.Waypoints waypoints = new Path.Waypoints();
        public void Update(float elapsedTime)
        {
            foreach (Layers.ILayer layer in layers.Values)
            {
                layer.Update(elapsedTime);
            }
            
        }
        public void Click(int x, int y)
        {
            foreach (KeyValuePair<string, Layers.ILayer> layer in layersDepthSorted)
            {
                if (layer.Value.collisionDetect(Graphics.Scaler.screenToWorld(x, y).X,
                    Graphics.Scaler.screenToWorld(x, y).Y))
                    break;
            }
        }
        public void Draw(float elapsedTime)
        {
            foreach (Layers.ILayer layer in layers.Values)
            {
                layer.Draw(elapsedTime);
            }
        }
    }
}
