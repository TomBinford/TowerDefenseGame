using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    [Flags]
    public enum Directions
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8,
        UpLeft = Up | Left,
        UpRight = Up | Right,
        DownLeft = Down | Left,
        DownRight = Down | Right,
        InvalidUpDown = Up | Down,
        InvalidLeftRight = Left | Right
    }

    public enum EnemyTypes
    {
        Goblin
    }

    public enum UnitStates
    {
        Walking,
        Attacking,
        Dying,
        Hurt,
        Idle
    }

    public enum TowerStates
    {
        Idle,
        Shoot
    }

    public enum TowerTypes
    {
        Support,
        Archer,
        Magic,
        Stone
    }
}