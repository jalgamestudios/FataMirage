using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FataMirage.Core.Graphics
{
    class Animation
    {
        public List<Frame> frames;
        public float currentTime;
        public float totalTime
        {
            get
            {
                return frames.Sum(frame => frame.duration);
            }
        }
        public Frame currentFrame
        {
            get { return frames[currentFrameIndex]; }
        }
        public int currentFrameIndex
        {
            get
            {
                float remainingTime = currentTime;
                for (int i = 0; i < frames.Count; i++)
                {
                    if (remainingTime < frames[i].duration)
                        return i;
                    remainingTime -= frames[i].duration;
                }
                return 0;
            }
        }
        public Animation()
        {
            this.frames = new List<Frame>();
        }
        public Animation(List<Frame> frames)
        {
            this.frames = frames;
        }
        public void update(float elapsedTime)
        {
            currentTime += elapsedTime;
            if (currentTime >= totalTime)
                currentTime -= totalTime;
        }
        public Graphics.Texture currentTexture
        {
            get { return currentFrame.texture; }
        }
    }
    class Frame
    {
        public Texture texture;
        public float duration;
        public Frame(string name, float duration)
        {
            this.texture = new Texture(name);
            this.duration = duration;
        }
    }
}
