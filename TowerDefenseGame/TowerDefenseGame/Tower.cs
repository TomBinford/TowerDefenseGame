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
    public class Tower : Sprite
    {
        public AnimationState state;

        public Dictionary<AnimationState, Animation> animations;

        public float radius;

        public bool isShooting;

        public Tower(Animation idle, Animation shooting, Vector2 position, Color tint, float scale)
            : base(idle.frames[0], position, tint, 0f, scale)
        {
            animations = new Dictionary<AnimationState, Animation>();
            animations.Add(AnimationState.Idle, idle);
            animations.Add(AnimationState.Shoot, shooting);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ChangeTexture(animations[state].CurrentFrame);
            animations[state].Advance();
            base.Draw(spriteBatch);
        }

        public bool InRange(Balloon balloon)
        {
            return Vector2.Distance(balloon.position, position) - (radius + balloon.radius) < 0;
        }

        public void Update()
        {
            animations[state].Advance();
            if (animations[state].frame == animations[state].frames.Count - 1)
            {
                if (state != AnimationState.Idle)
                {
                    animations[state].frame = 0;
                }
                state = AnimationState.Idle;
                isShooting = false;
            }
        }

        public void Shoot(Balloon target)
        {
            isShooting = true;
            Vector2 angleVector = Vector2.Subtract(position, target.position);
            angle = MathHelper.ToDegrees((float)Math.Atan2(-1 * angleVector.X, angleVector.Y));
            state = AnimationState.Shoot;
            target.Pop();
        }
    }
}