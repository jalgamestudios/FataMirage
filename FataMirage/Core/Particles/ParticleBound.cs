using SharpDX;
using System;
using System.Collections.Generic;
using System.Text;

namespace FataMirage.Core.Particles
{
    class ParticleBound
    {
        RectangleF bounds;
        public List<Particle> particles;
        public void Update(float elapsedTime)
        {
            foreach (var particle in particles)
                particle.Update(elapsedTime);
        }
        public void Draw(float elapsedTime)
        {
            foreach (var particle in particles)
                particle.Draw(elapsedTime);
        }
        public ParticleBound(RectangleF bounds)
        {
            this.bounds = bounds;
            this.particles = new List<Particle>();
        }
        public ParticleBound(float x, float y, float width, float height)
        {
            this.bounds = new RectangleF(x, y, width, height);
            this.particles = new List<Particle>();
        }
    }
}
