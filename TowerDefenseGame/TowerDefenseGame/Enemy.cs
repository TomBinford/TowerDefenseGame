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

        public Enemy(Dictionary<UnitStates, Animation> animations, Vector2 position) : base(animations[UnitStates.Walking].CurrentFrame, position, Color.White)
        {
            Animations = animations;
            State = UnitStates.Walking;
            Direction = Directions.None;
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
                    break;
            }
            Texture = Animations[State].CurrentFrame;
        }
    }
}