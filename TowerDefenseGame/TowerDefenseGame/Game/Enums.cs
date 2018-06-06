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

    public enum Difficulties
    {
        Easy,
        Normal,
        Hard
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
        DifficultySelect,
        LevelSelect,
        Battle,
        Build,
        Settings,
        None
    }
    
    public enum Themes
    {
        Cemetery,
        Desert,
        Jungle,
        Medieval,
        Snow,
        Spooky,
        Village
    }

    public static class Functions
    {
        public static Rectangle Scale(this Rectangle rect, float scale)
        {
            return new Rectangle(rect.Location, new Point((int)(rect.Width * scale), (int)(rect.Height * scale)));
        }

        public static Rectangle Scale(this Rectangle rect, Vector2 scale)
        {
            return new Rectangle(rect.Location, new Point((int)(rect.Width * scale.X), (int)(rect.Height * scale.Y)));
        }

        public static Vector2 GetCenter(this Viewport viewport)
        {
            return new Vector2(viewport.X + viewport.Width / 2f, viewport.Y + viewport.Height / 2f);
        }

        public static int DigitAt(this int number, int index)
        {
            string s = number.ToString();
            return int.Parse(s[index].ToString());
        }
        
        public static int Length(this int number)
        {
            for (int i = 0; true; i++)
            {
                if (number % Math.Pow(10, i) == number)
                {
                    return i;
                }
            }
        }
    }
}