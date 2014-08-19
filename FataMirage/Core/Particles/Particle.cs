﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.Particles
{
    class Particle
    {
        public int typeID;
        public Vector2 position;
        public Vector2 direction;
        public Dictionary<Color, Color> currentColors = new Dictionary<Color, Color>();
        public Particle(int typeID, Vector2 position)
        {
            this.typeID = typeID;
            this.position = position;
            this.direction = new Vector2(0, 0);
            this.currentColors = ParticleHost.particleDefinitions[typeID].getRandomColors();
        }
        public Particle(int typeID, Vector2 position, Vector2 direction)
        {
            this.typeID = typeID;
            this.position = position;
            this.direction = direction;
            this.currentColors = ParticleHost.particleDefinitions[typeID].getRandomColors();
        }
        public Particle(int typeID, Vector2 position, Dictionary<Color, Color> currentColors)
        {
            this.typeID = typeID;
            this.position = position;
            this.direction = new Vector2(0, 0);
            this.currentColors = currentColors;
        }
        public Particle(int typeID, Vector2 position, Vector2 direction, Dictionary<Color, Color> currentColors)
        {
            this.typeID = typeID;
            this.position = position;
            this.direction = direction;
            this.currentColors = currentColors;
        }
        public void Update(float elapsedTime)
        {

        }
        public void Draw(float elapsedTime)
        {
            Graphics.Scaler.Draw(ParticleHost.particleTexture, position.X, position.Y, 1 / 160f, 1 / 160f, 0.01f);
        }
    }
}