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
        public Sprite Turret2 = null;
        
        public override void Upgrade()
        {
            // TODO: Handle upgrade
            Level++;
            if (Level == 2)
            {
                Turret2 = Turret;
                Turret.Position.X += 20;
                Turret2.Position.X -= 20;
                //Create another archer
            }
        }

        public override void Update()
        {
            if (Turret2 != null)
            {

            }
            //TODO: Do things based on GameState.Get
        }

        public override void DrawTower(SpriteBatch spriteBatch)
        {
            Base.Draw(spriteBatch);
            if (Turret2 == null)
            { }
            else
            {
                Turret2.Draw(spriteBatch);
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
        
        Dictionary<TowerStates, Animation> TurretAnimations;

        public int Level;
        #endregion Properties

        public static T Create<T>(Vector2 position, Texture2D baseTexture, Dictionary<TowerStates, Animation> turretAnimations = null)
            where T : Tower, new()
        {
            var tower = new T()
            {
                Level = 0,
                Base = new Sprite(baseTexture, position, Color.White),
                TurretAnimations = turretAnimations,
                Tint = Color.White,
                State = TowerStates.Idle
            };

            if (turretAnimations != null)
            {
                tower.Turret = new Sprite(turretAnimations[TowerStates.Idle].CurrentFrame, position, Color.White);
            }
            else
            {
                tower.Turret = new Sprite(null, position, Color.White);
            }

            switch (tower)
            {
                case ArcherTower archerTower:
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