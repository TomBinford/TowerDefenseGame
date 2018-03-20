using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;

namespace TowerDefenseGame
{
    public class Balloon : Sprite
    {
        Animation pop;

        public bool popped;

        bool popStarted;

        Directions LastDirection;

        public Balloon(Vector2 position, Color tint, float angle, float scale, Animation animation)
            : base(animation.frames[0], position, tint, angle, scale)
        {
            pop = animation;
            popped = false;
            popStarted = false;
            LastDirection = Directions.None;
        }

        public void Update()
        {
            if (popStarted && !popped)
            {
                Pop();
            }
        }

        public void Pop()
        {
            popStarted = true;
            texture = pop.CurrentFrame;
            source = texture.Bounds;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            pop.Advance();
            if (pop.frame == 0)
            {
                popped = true;
            }
        }

        public void Move(Texture2D map)
        {

        }
    }
}