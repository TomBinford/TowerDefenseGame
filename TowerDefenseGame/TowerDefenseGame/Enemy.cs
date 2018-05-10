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
    public class Enemy : Sprite
    {
        public Dictionary<UnitStates, Animation> Animations;
        public UnitStates State;
        public Directions Direction;
        public Soldier Target;
        public float Speed;
        public float Health;
        public float Range;
        public float AttackDamage;

        public Enemy(Dictionary<UnitStates, Animation> animations, Vector2 position, float Range, float AttackDamage, float Health) : base(animations[UnitStates.Walking].CurrentFrame, position, Color.White)
        {
            Animations = animations;
            State = UnitStates.Idle;
            Direction = Directions.None;
            this.Range = Range;
            this.AttackDamage = AttackDamage;
            this.Health = Health;
            Target = null;
        }

        public void Update()
        {
            switch(State)
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
                case UnitStates.Dying:
                    Animations[State].Advance();
                    if (Animations[State].Frame == 0)
                    {
                        GameState.Get.Enemies.Remove(this);
                        return;
                    }
                    break;
                case UnitStates.Attacking:
                    Animations[State].Advance();
                    if (Animations[State].Frame == 0)
                    {
                        if (Target != null)
                        {
                            Target.Health -= AttackDamage;
                            Target.State = UnitStates.Hurt;
                            if (Target.Health < 0)
                            {
                                Target.State = UnitStates.Dying;
                                State = UnitStates.Idle;
                                Target = null;
                            }
                        }
                    }
                    break;
                case UnitStates.Idle:
                    float lowestRange = -1;
                    var n = GameState.Get.Soldiers.First;
                    while(n != null)
                    {
                        float r = Vector2.Distance(n.Value.Position, Position);
                        if (r < Range)
                        {
                            if (r < lowestRange)
                            {
                                Target = n.Value;
                                lowestRange = r;
                            }
                        }
                        n = n.Next;
                    }
                    if (Target != null)
                    {
                        State = UnitStates.Walking;
                    }
                    break;
            }
            Texture = Animations[State].CurrentFrame;
        }
    }
}