using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FataMirage.Core.Particles
{
    class ParticleDefinition
    {
        public List<ParticleColorMapColor> colors;
        public float gravity;
        public float mass;
        public float randomness;
        public float maxSpeed;
        public ParticleDefinition(float gravity, float mass, float randomness, float maxSpeed)
        {
            this.gravity = gravity;
            this.mass = mass;
            this.randomness = randomness;
            this.maxSpeed = maxSpeed;
            this.colors = new List<ParticleColorMapColor>();
        }
        public ParticleDefinition(float gravity, float mass, float randomness, float maxSpeed, 
            List<ParticleColorMapColor> colors)
        {
            this.gravity = gravity;
            this.mass = mass;
            this.randomness = randomness;
            this.maxSpeed = maxSpeed;
            this.colors = colors;
        }
        public ParticleDefinition(float gravity, float mass, float randomness, float maxSpeed,
            params ParticleColorMapColor[] colors)
        {
            this.gravity = gravity;
            this.mass = mass;
            this.randomness = randomness;
            this.maxSpeed = maxSpeed;
            this.colors = colors.ToList();
        }
        public Dictionary<Color, Color> getRandomColors()
        {
            Dictionary<Color, Color> returnColors = new Dictionary<Color, Color>();
            foreach (var color in colors)
                returnColors.Add(color.sourceColor, color.minColor);
            return returnColors;
        }
    }
    class ParticleColorMapColor
    {
        public Color sourceColor;
        public Color minColor;
        public Color maxColor;
        public float variation;
        public ParticleColorMapColor(Color sourceColor, Color minColor, Color maxColor, float variation)
        {
            this.sourceColor =  sourceColor;
            this.minColor = minColor;
            this.maxColor = maxColor;
            this.variation = variation;
        }
        public ParticleColorMapColor(int sourceR, int sourceG, int sourceB,
            int minR, int minG, int minB, int maxR, int maxG, int maxB,
                float variation)
        {
            this.sourceColor = new Color(sourceR, sourceG, sourceB);
            this.minColor = new Color(minR, minG, minB);
            this.maxColor = new Color(maxR, maxG, maxB);
            this.variation = variation;
        }
    }
}
