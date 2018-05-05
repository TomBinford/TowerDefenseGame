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

        public override void Upgrade(int left, int right)
        {
            // TODO: Handle upgrade
        }

        public override void Update()
        {
            //TODO: Do things based on GameState.Get
        }
    }

    public class StoneTower : Tower
    {
        public override void Upgrade(int left, int right)
        {
            // TODO: Handle upgrade
        }

        public override void Update()
        {
            //TODO: Do things based on GameState.Get
        }
    }

    public abstract class Tower
    {
        #region Properties
        public Vector2 Position
        {
            get
            {
                return Base.Position;
            }
            set
            {
                Base.Position = value;
            }
        }

        private Sprite _Base;

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

        private Sprite _Turret;

        public Sprite Turret
        {
            get
            {
                return _Turret;
            }
            set
            {
                _Turret = value;
                Base.Position = Turret.Position;
            }
        }

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
        
        Dictionary<TowerStates, Animation> Animations;

        public Tuple<int, int> Level;

        public float Angle;
        public float AngleDegrees => MathHelper.ToDegrees(Angle);
        #endregion Properties

        public static T Create<T>(Vector2 position, Dictionary<TowerStates, Animation> animations)
            where T : Tower, new()
        {
            var tower = new T()
            {
                Level = new Tuple<int, int>(0, 0),
                Position = position,
                Angle = 0,
                Animations = animations,
                Tint = Color.White,
                State = TowerStates.Idle
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
                    stoneTower.Range.Radius = 100;
                    break;
            }
            return tower;
        }
        
        public abstract void Upgrade(int l, int r);

        public abstract void Update();

        //List<Animations> Animations, Position, List<IProjectile>, int range, bool oneShot
    }
}