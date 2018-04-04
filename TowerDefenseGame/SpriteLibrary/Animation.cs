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
        public List<Texture2D> Frames;
        public int Frame;

        public Animation()
        {
            Frames = new List<Texture2D>();
            Frame = 0;
        }

        public void AddFrame(Texture2D Frame)
        {
            Frames.Add(Frame);
        }

        public void RemoveFrame(Texture2D Frame)
        {
            Frames.Remove(Frame);
        }

        public void Advance()
        {
            Frame = (Frame + 1) % Frames.Count;
        }
        
        public Texture2D CurrentFrame => Frames[Frame];
    }
}