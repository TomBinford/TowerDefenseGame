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
    public class LaserTower : Tower
    {
        // TODO: Define stuff specific to Laser Tower

        public override void Upgrade()
        {
            // TODO: Handle upgrade
        }
    }

    public class StoneTower : Tower
    {
        public override void Upgrade()
        {
            // TODO: Handle upgrade
        }
    }


    public abstract class Tower : Sprite
    {
        protected List<Projectile> Projectiles;

        private Texture2D nonMovingSprite;
        private bool spriteAssigned = false;

        protected int TowerLevel;

        public AnimationStates State;

        public TargetTypes TargetType;

        public Dictionary<AnimationStates, Animation> Animations;

        public float Radius;

        public float Range;

        public bool OneShot;

        public Texture2D Projectile;

        private int ticksSinceShoot;

        public int ShootDelay;

        public static T Create<T>(Vector2 position, Dictionary<AnimationStates, Animation> animations, List<Projectile> projectiles)
            where T : Tower, new()
        {
            var tower = new T()
            {
                Projectiles = projectiles,
                Animations = animations,
                TowerLevel = 0
            };

            switch (tower)
            {
                case LaserTower laserTower:
                    // TODO: Handle specifics of Laser Tower that are not default,
                    //       such as settings based on game state, etc
                    laserTower.Angle = 45;
                    break;

                case StoneTower stoneTower:
                    // TODO: Handle specifics of Stone Tower that are not default,
                    //       such as settings based on game state, etc
                    stoneTower.Range = 100;
                    break;
            }

            return tower;
        }

        /// <summary>
        /// Upgrades the tower to the next level
        /// </summary>
        public abstract void Upgrade();

        // TODO: Remove this
        protected Tower()
            : base(null, Vector2.Zero, Color.White, 0f, 1f)
        {

        }


        //List<Animations> Animations, Position, List<IProjectile>, int range, bool oneShot
        public Tower(Animation idle, Animation shooting, Vector2 position, Color tint, float scale, Texture2D projectile, int radius, int range, bool oneShot, TargetTypes type, Texture2D nonMoving)
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
            this.Projectile = projectile;
            Projectiles = new List<Projectile>();
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
            Projectiles = new List<Projectile>();
            ShootDelay = 60;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture = Animations[State].CurrentFrame;
            Animations[State].Advance();
            base.Draw(spriteBatch);
            if (spriteAssigned)
            {
                spriteBatch.Draw(nonMovingSprite, Position, Tint);
            }
        }

        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            if (Projectiles.Count == 0)
            {
                return;
            }
            foreach (Projectile projectile in Projectiles)
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
            return (Vector2.Distance(balloon.Position, Position) - (Range + balloon.Radius) < 0) && Animations[State].Frame == 0;
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
            Vector2 angleVector = Vector2.Subtract(Position, target.Position);
            Angle = MathHelper.ToDegrees((float)Math.Atan2(-1 * angleVector.X, angleVector.Y));
            State = AnimationStates.Shoot;
            Projectiles.Add(new Projectile(Projectile, Position, Color.White, 1, 1, Angle - 90));
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
                            if (Vector2.Distance(Position, balloon.Position) < Vector2.Distance(Position, bestCandidate.Position))
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
            if (Projectiles.Count == 0)
            {
                return;
            }
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Move();
            }
        }
    }
}