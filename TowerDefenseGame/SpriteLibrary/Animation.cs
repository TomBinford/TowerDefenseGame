using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLibrary
{
    public class Animation
    {
        public List<Texture2D> frames;
        public int frame;

        public Animation()
        {
            frames = new List<Texture2D>();
            frame = 0;
        }

        public void AddFrame(Texture2D frame)
        {
            frames.Add(frame);
        }

        public void RemoveFrame(Texture2D frame)
        {
            frames.Remove(frame);
        }

        public void Advance()
        {
            frame = (frame + 1) % frames.Count;
        }
        
        public Texture2D CurrentFrame => frames[frame];
    }
}