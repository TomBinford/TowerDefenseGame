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
        List<Sprite> frames;
        int frame;

        public Animation()
        {
            frames = new List<Sprite>();
            frame = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frames[frame].Draw(spriteBatch);
        }

        public void AddFrame(Sprite sprite)
        {
            frames.Add(sprite);
        }

        public void RemoveFrame(Sprite sprite)
        {
            frames.Remove(sprite);
        }

        public void Advance()
        {
            frame = (frame + 1) % frames.Count;
        }
    }
}