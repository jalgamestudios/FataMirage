using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirageWinRT.Core.Player
{
    class Legs
    {
        public const float speed = 0.2f;
        static float directionAngel = 0;
        public enum Directions
        {
            Top,
            Bottom,
            Left,
            Right
        }
        public static Directions direction
        {
            get
            {
                if (directionAngel > Math.PI / -4 && directionAngel <= Math.PI / 4)
                    return Directions.Right;
                if (directionAngel <= Math.PI /-4 * 3 || directionAngel > Math.PI / 4 * 3)
                    return Directions.Left;
                if (directionAngel > Math.PI / 4 && directionAngel <= Math.PI / 4 * 3)
                    return Directions.Bottom;
                return Directions.Top;
            }
        }
        public static Vector2 currentPosition
        {
            get
            {
                if (isWalking)
                    return Scene.SceneManager.currentScene.waypoints.interpolatePosition(currentPositionName, currentGoal, progress);
                else
                    return Scene.SceneManager.currentScene.waypoints.getPosition(currentPositionName);
            }
        }
        public static string currentPositionName;
        public static string currentGoal;
        public static float timeWalking;
        public static float progress
        {
            get
            {
                return (speed * timeWalking) /Scene.SceneManager.currentScene.waypoints.getDisctance(currentPositionName, currentGoal);
            }
        }
        //public static float walkingLength;
        public static bool isWalking
        {
            get { return _isWalking; }
            set{_isWalking = value; if (!value) timeWalking = 0;}
        }
        static bool _isWalking = false;

        public static void Init()
        {
            currentPositionName = "left";
            currentGoal = "center";
        }
        public static void LoadContent()
        {
        }
        public static void Update(float elapsedTime)
        {
            if (isWalking)
            {
                timeWalking += elapsedTime;
                directionAngel = Scene.SceneManager.currentScene.waypoints.getAngel(currentPositionName, currentGoal);
                if (progress >= 1)
                {
                    isWalking = false;
                    currentPositionName = currentGoal;
                    timeWalking = 0;
                }
            }
        }
        public static void Draw(float elapsedTime)
        {
        }
    }
}
