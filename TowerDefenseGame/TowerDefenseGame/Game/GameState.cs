using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefenseGame.Screens;

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

        public static List<Level> Levels = new List<Level>();
        public static Level CurrentLevel;
        
        public static void Load()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                Levels.Add(new Level(random.Next(4)));
            }
        }
    }
}