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
            Animations[State].Advance();
            switch (State)
            {
                case UnitStates.Walking:
                    double angle = (float)Math.Atan2(Target.Position.Y - Position.Y, Target.Position.X - Position.X);
                    Position.X += (float)(Math.Cos(angle) * Speed);
                    Position.Y += (float)(Math.Sin(angle) * Speed);
                    break;
                case UnitStates.Dying:
                    if (Animations[State].Frame == 0)
                    {
                        GameState.Soldiers.Remove(this);
                        return;
                    }
                    break;
                case UnitStates.Attacking:
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
                case UnitStates.Hurt:
                    if (Animations[State].Frame == 0)
                    {
                        State = UnitStates.Idle;
                    }
                    break;
                case UnitStates.Idle:
                    float lowestRange = -1;
                    var n = GameState.Enemies.First;
                    while (n != null)
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