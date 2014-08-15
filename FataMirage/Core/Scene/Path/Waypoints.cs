using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Scene.Path
{
    class Waypoints
    {
        public Dictionary<string, Waypoint> waypoints;
        public Waypoints()
        {
            waypoints = new Dictionary<string, Waypoint>();
        }
        public Vector2 getPosition(string name)
        {
            if (waypoints.ContainsKey(name))
            {
                return waypoints[name].position;
            }
            return new Vector2(0.5f, 0.5f);
        }
        public Vector2 interpolatePosition(string from, string to, float percentage)
        {
            if (waypoints.ContainsKey(from) && waypoints.ContainsKey(to))
            {
                return waypoints[from].position * (1- percentage) + waypoints[to].position * percentage;
            }
            return new Vector2(0.5f, 0.5f);
        }
        public string getNearestConnection(string from, float x, float y)
        {
            Dictionary<float, string> connections = new Dictionary<float, string>();
            foreach (WaypointConnection connection in waypoints[from].connectedTo)
            {
                //connections.Add((waypoints[connection.goesTo].position - new Vector2(x, y)).Length(), connection.goesTo);
                float connectionAngel = (float)Math.Atan2((waypoints[connection.goesTo].position- waypoints[from].position).Y,
                    (waypoints[connection.goesTo].position - waypoints[from].position).X);
                float clickAngel = (float)Math.Atan2((new Vector2(x, y) - waypoints[from].position).Y,
                    (new Vector2(x, y) - waypoints[from].position).X);
                float differenceAngel = Math.Abs(Math.Max(connectionAngel, clickAngel) - Math.Min(connectionAngel, clickAngel));
                connections.Add((float)Math.Min(differenceAngel, Math.PI * 2 - differenceAngel), connection.goesTo);
            }
            var connectionsOrdered = connections.OrderBy(kvp => kvp.Key).ToList();
            if (connectionsOrdered.Count == 0)
                return "false";
            return connectionsOrdered.First().Value;
        }
        public float getDisctance(string from, string to)
        {
            return (waypoints[from].position - waypoints[to].position).Length();
        }
        public float getAngel(string from, string to)
        {
            return (float)Math.Atan2((waypoints[to].position - waypoints[from].position).Y,
                (waypoints[to].position - waypoints[from].position).X);
        }
    }
}
