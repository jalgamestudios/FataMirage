﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FataMirage.Core
{
    static class CoreControl
    {
        public static void Init()
        {
            Scene.SceneManager.Init();
            Player.PlayerManager.Init();
            Particles.ParticleHost.Init();
        }
        public static void LoadContent()
        {
            UI.UIManager.Init();
            Game.LoadMeta.Load();
            Game.LoadUI.Load();
            Scene.SceneLoader.LoadScenes();
            Player.PlayerManager.LoadContent();
        }
        public static void Update(float elapsedTime)
        {
            Input.InputManager.Update(elapsedTime);
            if (Game.State.gameState == Game.State.GameStates.Running)
            {
                Scene.SceneManager.Update(elapsedTime);
                Player.PlayerManager.Update(elapsedTime);
                Particles.ParticleHost.Update(elapsedTime);
                UI.UIManager.Update(elapsedTime);
            }
        }
        public static void Draw(float elapsedTime)
        {
            Graphics.Manager.StartDraw();
            if (Game.State.gameState == Game.State.GameStates.Running)
            {
                Scene.SceneManager.Draw(elapsedTime);
                Player.PlayerManager.Draw(elapsedTime);
                Particles.ParticleHost.Draw(elapsedTime);
                UI.UIManager.Draw(elapsedTime);
            }
            Input.InputManager.Draw(elapsedTime);
            Graphics.Manager.EndDraw();
        }
    }
}
