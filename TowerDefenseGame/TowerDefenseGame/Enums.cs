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

    public enum Enemies
    {
        Goblin
    }

    public enum TowerStates
    {
        Idle,
        Shoot
    }

    public enum TargetTypes
    {
        First,
        Close,
        Last,
        Strong
    }
}
