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
        public Enemy Target;

        public Soldier(Dictionary<UnitStates, Animation> animations, Vector2 position) : base(animations[UnitStates.Walking].CurrentFrame, position, Color.White)
        {
            Animations = animations;
        }

        public void Update()
        {
            switch (State)
            {
                case UnitStates.Walking:
                    break;
            }
        }
    }
}