using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public static class GameState
    {
        public static MouseState CurrentMouse;
        public static MouseState OldMouse;

        public static Viewport ScreenViewport;

        public static LinkedList<Enemy> Enemies = new LinkedList<Enemy>();
        public static LinkedList<Soldier> Soldiers = new LinkedList<Soldier>();
        public static LinkedList<Sprite> ProjectilePositions = new LinkedList<Sprite>();

        public static Difficulties Difficulty;

        public static ulong Money = 0;

        public static bool SoundOn = true;
        public static bool MusicOn = true;
        public static bool VibrationOn = true;
        public static bool NotificationOn = true;

        public static int[] LevelStars = {
            0, 1, 2, 3, 1, 2, 3, 2,
            2
        };
    }
}