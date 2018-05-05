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

    public enum EnemyTypes
    {
        Goblin
    }

    public enum UnitStates
    {
        Walking,
        Attacking,
        Dying,
        Hurt
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