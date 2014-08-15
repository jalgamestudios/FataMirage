using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core.Scene.Path
{
    class Waypoint
    {
        public Vector2 position;
        public List<WaypointConnection> connectedTo;
        public Waypoint(Vector2 position)
        {
            this.position = position;
            this.connectedTo = new List<WaypointConnection>();
        }
        public Waypoint(float x, float y)
        {
            this.position = new Vector2(x, y);
            this.connectedTo = new List<WaypointConnection>();
        }
        public void addConnection(string goesTo)
        {
            connectedTo.Add(new WaypointConnection(goesTo));
        }
        public void addConnection(string goesTo, float speedMultiplier)
        {
            connectedTo.Add(new WaypointConnection(goesTo, speedMultiplier));
        }
    }
    class WaypointConnection
    {
        public string goesTo;
        public float walkingMultiplier;
        public bool enabled;
        public WaypointConnection(string goesTo)
        {
            this.goesTo = goesTo;
            this.walkingMultiplier = 1;
            this.enabled = true;
        }
        public WaypointConnection(string goesTo, float walkingMultiplier)
        {
            this.goesTo = goesTo;
            this.walkingMultiplier = walkingMultiplier;
            this.enabled = true;
        }
        public WaypointConnection(string goesTo, bool enabled)
        {
            this.goesTo = goesTo;
            this.walkingMultiplier = 1;
            this.enabled = enabled;
        }
        public WaypointConnection(string goesTo, float walkingMultiplier, bool enabled)
        {
            this.goesTo = goesTo;
            this.walkingMultiplier = walkingMultiplier;
            this.enabled = enabled;
        }
    }
}
