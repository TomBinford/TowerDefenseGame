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
    public class GameState
    {
        public MouseState CurrentMouse;
        public MouseState OldMouse;

        public Viewport ScreenViewport;

        public LinkedList<Enemy> Enemies;
        public LinkedList<Soldier> Soldiers;
        public LinkedList<Sprite> ProjectilePositions;

        public Difficulties Difficulty;

        public ulong Money;

        public bool SoundOn;
        public bool MusicOn;
        public bool VibrationOn;
        public bool NotificationOn;
        
        private static GameState state = new GameState();
        private GameState()
        {
            Money = 0;
            Enemies = new LinkedList<Enemy>();
            Soldiers = new LinkedList<Soldier>();
            ProjectilePositions = new LinkedList<Sprite>();
            SoundOn = true;
            MusicOn = true;
            VibrationOn = true;
            NotificationOn = true;
        }

        public static GameState Get
        {
            get
            {   
                return state;
            }
        }
    }
}