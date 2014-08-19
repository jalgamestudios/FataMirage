using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.Particles
{
    class ParticleHost
    {
        public static Dictionary<int, ParticleDefinition> particleDefinitions;
        public static Dictionary<string, ParticleBound> particleBounds;
        public static Texture2D particleTexture; //Please note that custom particle textures are planned in the future, but for pixel art, we can go with single pixels
        public static void Init()
        {
            Color[] colors = new Color[1];
            colors[0] = new Color(255, 255, 255, 255);
            particleTexture = new Texture2D(Stator.device, 1, 1, false, SurfaceFormat.Color);
            particleTexture.SetData(colors);
        }
        public static void Update(float elapsedTime)
        {
            foreach (var particleBound in particleBounds)
                particleBound.Value.Update(elapsedTime);
        }
        public static void Draw(float elapsedTime)
        {
            foreach (var particleBound in particleBounds)
                particleBound.Value.Draw(elapsedTime);
        }
    }
}
