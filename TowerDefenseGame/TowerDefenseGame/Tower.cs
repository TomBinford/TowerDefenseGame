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
        public List<Projectile> projectiles;
        private Texture2D nonMovingSprite;
        private bool spriteAssigned = false;

        public AnimationStates State;

        public TargetTypes TargetType;

        public Dictionary<AnimationStates, Animation> Animations;

        public float Radius;

        public float Range;

        public bool OneShot;

        public Texture2D Projectile;

        private int ticksSinceShoot;

        public int ShootDelay;

        public Tower(Animation idle, Animation shooting, Vector2 position, Color tint, float scale, Texture2D Projectile, int radius, int range, bool oneShot, TargetTypes type, Texture2D nonMoving)
            : base(idle.Frames[0], position, tint, 0f, scale)
        {
            TargetType = type;
            Radius = radius;
            Range = range;
            OneShot = oneShot;
            nonMovingSprite = nonMoving;
            Animations = new Dictionary<AnimationStates, Animation>();
            Animations.Add(AnimationStates.Idle, idle);
            Animations.Add(AnimationStates.Shoot, shooting);
            TargetType = TargetTypes.First;
            this.Projectile = Projectile;
            projectiles = new List<Projectile>();
            spriteAssigned = true;
        }

        public Tower(Animation idle, Animation shooting, Vector2 position, Color tint, float scale, Texture2D Projectile, int radius, int range, bool oneShot, TargetTypes type)
            : base(idle.Frames[0], position, tint, 0f, scale)
        {
            TargetType = type;
            Radius = radius;
            Range = range;
            OneShot = oneShot;
            Animations = new Dictionary<AnimationStates, Animation>();
            Animations.Add(AnimationStates.Idle, idle);
            Animations.Add(AnimationStates.Shoot, shooting);
            TargetType = TargetTypes.First;
            this.Projectile = Projectile;
            projectiles = new List<Projectile>();
            ShootDelay = 60;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture = Animations[State].CurrentFrame;
            Animations[State].Advance();
            base.Draw(spriteBatch);
            if (spriteAssigned)
            {
                spriteBatch.Draw(nonMovingSprite, position, tint);
            }
        }

        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            if (projectiles.Count == 0)
            {
                return;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        public bool InRange(Balloon balloon)
        {
            if (Range == 0)
            {
                return true;
            }
            return (Vector2.Distance(balloon.position, position) - (Range + balloon.Radius) < 0) && Animations[State].Frame == 0;
        }

        public void Update()
        {
            Animations[State].Advance();
            if (Animations[State].Frame == Animations[State].Frames.Count - 1)
            {
                if (State != AnimationStates.Idle)
                {
                    Animations[State].Frame = 0;
                }
                State = AnimationStates.Idle;
            }
            ticksSinceShoot++;
        }

        public void Shoot(Balloon target)
        {
            if (ticksSinceShoot < ShootDelay)
            {
                return;
            }
            ticksSinceShoot = 0;
            Vector2 angleVector = Vector2.Subtract(position, target.position);
            angle = MathHelper.ToDegrees((float)Math.Atan2(-1 * angleVector.X, angleVector.Y));
            State = AnimationStates.Shoot;
            projectiles.Add(new Projectile(Projectile, position, Color.White, 5, 1, angle - 90));
        }

        public Balloon BestShot(List<Balloon> balloons)
        {
            if (balloons.Count == 0)
            {
                return null;
            }
            Balloon bestCandidate = null;
            foreach (Balloon balloon in balloons)
            {
                switch (TargetType)
                {
                    case TargetTypes.First:
                        if (bestCandidate != null)
                        {
                            if (balloon.MovesMade > bestCandidate.MovesMade)
                            {
                                bestCandidate.PopStarted = false;
                                bestCandidate.Texture = bestCandidate.PopAnimation.Frames[0];
                                bestCandidate = balloon;
                            }
                        }
                        else
                        {
                            bestCandidate = balloon;
                        }
                        break;
                    case TargetTypes.Last:
                        if (bestCandidate != null)
                        {
                            if (balloon.MovesMade < bestCandidate.MovesMade)
                            {
                                bestCandidate.PopStarted = false;
                                bestCandidate.Texture = bestCandidate.PopAnimation.Frames[0];
                                bestCandidate = balloon;
                            }
                        }
                        else
                        {
                            bestCandidate = balloon;
                        }
                        break;
                    case TargetTypes.Close:
                        if (bestCandidate != null)
                        {
                            if (Vector2.Distance(position, balloon.position) < Vector2.Distance(position, bestCandidate.position))
                            {
                                bestCandidate.PopStarted = false;
                                bestCandidate.Texture = bestCandidate.PopAnimation.Frames[0];
                                bestCandidate = balloon;
                            }
                        }
                        else
                        {
                            bestCandidate = balloon;
                        }
                        break;
                    case TargetTypes.Strong:
                        if (bestCandidate != null)
                        {
                            if ((int)balloon.BalloonColor > (int)bestCandidate.BalloonColor)
                            {
                                bestCandidate.PopStarted = false;
                                bestCandidate.Texture = bestCandidate.PopAnimation.Frames[0];
                                bestCandidate = balloon;
                            }
                        }
                        else
                        {
                            bestCandidate = balloon;
                        }
                        break;
                }
                if (balloon != bestCandidate)
                {
                    balloon.Texture = balloon.PopAnimation.Frames[0];
                    balloon.PopStarted = false;
                }
            }
            return bestCandidate;
        }

        public void UpdateProjectiles()
        {
            if (projectiles.Count == 0)
            {
                return;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Move();
            }
        }
    }
}