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
    public class ArcherTower : Tower
    {
        public Sprite Archer2 = null;
        bool leftShooting;
        bool rightShooting;
        Dictionary<TowerStates, Animation> Archer2Animations;
        public Sprite Arrow;
        public Sprite Arrow2;
        public float ArrowSpeed;

        public override void Upgrade()
        {
            // TODO: Handle upgrade
            Level++;
            State = TowerStates.Idle;
            TurretAnimations[TowerStates.Shoot].Frame = 0;
            TurretAnimations[TowerStates.Idle].Frame = 0;
            if (Level == 2)
            {
                Archer2Animations = TurretAnimations;
                Archer2 = Turret;
                Arrow2 = Arrow;
                Turret.Position.X += 20;
                Archer2.Position.X -= 20;
                //Create another archer
            }
        }

        public override void Update()
        {
            if (Archer2 != null)
            {
                switch (State)
                {
                    case TowerStates.Idle://Has to always be idle because tower has to find targets and shoot at the same time
                        Target = Vector2.Zero;
                        float lowestRange = -1;

                        var n = GameState.Enemies.First;
                        while(n != null)
                        {
                            if (Range.Intersects(n.Value.Position))
                            {
                                float newRange = Vector2.Distance(Range.Position, n.Value.Position);
                                if (newRange < lowestRange)
                                {
                                    Target = n.Value.Position;
                                    lowestRange = newRange;
                                }
                            }
                            n = n.Next;
                        }
                        if (lowestRange != -1)//If a target was found, determine which archer to use and what direction to face it
                        {
                            if (Target.X <= TruePosition.X)//shoot left
                            {
                                if (!leftShooting)
                                {
                                    leftShooting = true;
                                    Archer2.Effect = SpriteEffects.FlipHorizontally;
                                }
                                else if(!rightShooting)
                                {
                                    rightShooting = true;
                                    Turret.Effect = SpriteEffects.FlipHorizontally;
                                }
                            }
                            else//shoot right
                            {
                                if (!rightShooting)
                                {
                                    rightShooting = true;
                                    Turret.Effect = SpriteEffects.None;
                                }
                                else if (!leftShooting)
                                {
                                    leftShooting = true;
                                    Archer2.Effect = SpriteEffects.None;
                                }
                            }
                        }
                        if (leftShooting)
                        {
                            Archer2Animations[TowerStates.Shoot].Advance();
                            if (Archer2Animations[TowerStates.Shoot].Frame == 0)
                            {
                                leftShooting = false;
                            }
                            else if (Archer2Animations[TowerStates.Shoot].Frame == ShootFrame)
                            {
                                //TODO: Add projectile to GameState.Get pointing toward Target
                            }
                            Archer2.Texture = Archer2Animations[TowerStates.Shoot].CurrentFrame;
                        }
                        if (rightShooting)
                        {
                            TurretAnimations[TowerStates.Shoot].Advance();
                            if (TurretAnimations[TowerStates.Shoot].Frame == 0)
                            {
                                leftShooting = false;
                            }
                            else if (TurretAnimations[TowerStates.Shoot].Frame == ShootFrame)
                            {
                                //TODO: Add projectile to GameState.Get pointing toward Target
                            }
                            Turret.Texture = TurretAnimations[TowerStates.Shoot].CurrentFrame;
                        }
                        break;
                }
            }
            else//only one archer
            {
                switch (State)
                {
                    case TowerStates.Idle:
                        Target = Vector2.Zero;
                        float lowestRange = -1;
                        
                        var n = GameState.Enemies.First;
                        for (int i = 0; i < GameState.Enemies.Count; i++)
                        {
                            if (Range.Intersects(n.Value.Position))
                            {
                                float newRange = Vector2.Distance(Range.Position, n.Value.Position);
                                if (newRange < lowestRange)
                                {
                                    Target = n.Value.Position;
                                    lowestRange = newRange;
                                }
                            }
                            n = n.Next;
                        }

                        if (lowestRange != -1)//If a target was found, determine what direction to face the archer
                        {
                            if (Target.X <= TruePosition.X)
                            {
                                //Face left
                                Turret.Effect = SpriteEffects.FlipHorizontally;
                            }
                            else
                            {
                                //Face right
                                Turret.Effect = SpriteEffects.None;
                            }
                            State = TowerStates.Shoot;
                        }
                        break;
                    case TowerStates.Shoot:
                        TurretAnimations[TowerStates.Shoot].Advance();
                        if (TurretAnimations[TowerStates.Shoot].Frame == 0)
                        {
                            State = TowerStates.Idle;
                        }
                        else if (TurretAnimations[TowerStates.Shoot].Frame == ShootFrame)
                        {
                            //TODO: Add Projectile to GameState.Get
                        }
                        Turret.Texture = TurretAnimations[TowerStates.Shoot].CurrentFrame;
                        break;
                }
            }
        }

        public override void DrawTower(SpriteBatch spriteBatch)
        {
            Base.Draw(spriteBatch);
            if (Archer2 == null)
            { }
            else
            {
                Archer2.Draw(spriteBatch);
            }
            Turret.Draw(spriteBatch);
        }
    }

    public class BallistaTower : Tower
    {
        public override void Upgrade()
        {
            // TODO: Handle upgrade
            Level++;
        }

        public override void Update()
        {
            //TODO: Do things based on GameState.Get
        }

        public override void DrawTower(SpriteBatch spriteBatch)
        {
            Base.Draw(spriteBatch);
        }
    }

    public class StoneTower : Tower
    {
        public override void Upgrade()
        {
            // TODO: Handle upgrade
        }

        public override void Update()
        {
            //TODO: Do things based on GameState.Get
        }

        public override void DrawTower(SpriteBatch spriteBatch)
        {
            Base.Draw(spriteBatch);
        }
    }

    public abstract class Tower
    {
        #region Properties
        public Vector2 TruePosition;

        protected Sprite _Base;

        public Sprite Base
        {
            get
            {
                return _Base;
            }
            set
            {
                _Base = value;
                Turret.Position = Base.Position;
            }
        }

        public Sprite Turret;

        public Color Tint
        {
            get
            {
                return Base.Tint;
            }
            set
            {
                Base.Tint = value;
                Turret.Tint = value;
            }
        }
        public CircularHitbox Range;
        public Rectangle Hitbox;

        public TowerStates State;

        public Dictionary<TowerStates, Animation> TurretAnimations;

        public int ShootFrame;
        public Vector2 Target;

        public int Level;
        #endregion Properties

        public static T Create<T>(Vector2 position, Texture2D baseTexture, Vector2 BaseOffset, Vector2 TurretOffset, int shootFrame = 0, Texture2D arrow = null, float arrowSpeed = 0, Dictionary<TowerStates, Animation> turretAnimations = null)
            where T : Tower, new()
        {
            var tower = new T()
            {
                Level = 0,
                TruePosition = position,
                Base = new Sprite(baseTexture, position + BaseOffset, Color.White),
                Range = new CircularHitbox(10, position),
                TurretAnimations = turretAnimations,
                ShootFrame = shootFrame,
                Tint = Color.White,
                State = TowerStates.Idle
            };

            if (turretAnimations != null)
            {
                tower.Turret = new Sprite(turretAnimations[TowerStates.Idle].CurrentFrame, position + TurretOffset, Color.White);
            }
            else
            {
                tower.Turret = new Sprite(null, position, Color.White);
            }

            switch (tower)
            {
                case ArcherTower archerTower:
                    archerTower.Arrow = new Sprite(arrow, Vector2.Zero, Color.White);
                    archerTower.ArrowSpeed = arrowSpeed;
                    // TODO: Handle specifics of Laser Tower that are not default,
                    //       such as settings based on game state, etc
                    break;

                case StoneTower stoneTower:
                    // TODO: Handle specifics of Stone Tower that are not default,
                    //       such as settings based on game state, etc
                    break;
            }
            return tower;
        }

        public abstract void Upgrade();

        public abstract void Update();

        public abstract void DrawTower(SpriteBatch spriteBatch);

        //List<Animations> Animations, Position, List<IProjectile>, int range, bool oneShot
    }
}