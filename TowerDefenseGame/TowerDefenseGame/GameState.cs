using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        
        public LinkedList<Enemy> Enemies;
        public LinkedList<Soldier> Soldiers;

        public ulong Money;
        
        private static GameState state = new GameState();
        private GameState()
        {
            Money = 0;
            Enemies = new LinkedList<Enemy>();
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