using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

    public enum ScreenTypes
    {
        Main,
        LevelSelect,
        Battle,
        Settings,
        None
    }

    public static class Functions
    {
        public static Rectangle Scale(this Rectangle rect, float scale)
        {
            return new Rectangle(rect.Location, new Point((int)(rect.Width * scale), (int)(rect.Height * scale)));
        }

        public static Vector2 GetCenter(this Viewport viewport)
        {
            return new Vector2(viewport.X + viewport.Width / 2f, viewport.Y + viewport.Height / 2f);
        }
    }
}