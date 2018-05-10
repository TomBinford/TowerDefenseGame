using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class Soldier : Sprite
    {
        public Vector2 GoalPosition;
        public Dictionary<UnitStates, Animation> Animations;
        public UnitStates State;
        public Directions Direction;
        public Enemy Target;
        public float Speed;
        public float Health;
        public float Range;
        public float AttackDamage;

        public Soldier(Dictionary<UnitStates, Animation> animations, Vector2 position, float Range, float AttackDamage, float Health) : base(animations[UnitStates.Walking].CurrentFrame, position, Color.White)
        {
            Animations = animations;
            State = UnitStates.Idle;
            this.Range = Range;
            this.AttackDamage = AttackDamage;
            this.Health = Health;
        }

        public void Update()
        {
            switch (State)
            {
                case UnitStates.Walking:
                    Animations[State].Advance();
                    switch (Direction)
                    {
                        case Directions.Right:
                            Position.X += Speed;
                            break;
                        case Directions.Left:
                            Position.X -= Speed;
                            break;
                        case Directions.Up:
                            Position.Y -= Speed;
                            break;
                        case Directions.Down:
                            Position.Y += Speed;
                            break;
                        case Directions.UpLeft:
                            Position.X -= Speed / (float)Math.Sqrt(2);
                            Position.Y -= Speed / (float)Math.Sqrt(2);
                            break;
                        case Directions.UpRight:
                            Position.X += Speed / (float)Math.Sqrt(2);
                            Position.Y -= Speed / (float)Math.Sqrt(2);
                            break;
                        case Directions.DownLeft:
                            Position.X -= Speed / (float)Math.Sqrt(2);
                            Position.Y += Speed / (float)Math.Sqrt(2);
                            break;
                        case Directions.DownRight:
                            Position.X += Speed / (float)Math.Sqrt(2);
                            Position.Y += Speed / (float)Math.Sqrt(2);
                            break;
                    }
                    break;
            }
        }
    }
}