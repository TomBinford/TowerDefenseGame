using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public enum Directions
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    public enum BalloonColors
    {
        Red = 0,
        Blue,
        Green,
        Yellow,
        Pink,
        Black,
        White,
        Zebra,
        Lead,
        Rainbow,
        Ceramic,
        Invincible
    }

    public enum AnimationStates
    {
        Idle,
        Shoot
    }

    public enum TargetTypes
    {
        First,
        Close,
        Last,
        Strong,
        Weak
    }
}
