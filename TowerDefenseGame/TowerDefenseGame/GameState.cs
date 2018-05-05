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
        public MouseState currentMouse;
        public MouseState oldMouse;

        private static GameState state = new GameState();
        private GameState()
        {}

        public static GameState Get
        {
            get
            {
                return state;
            }
        }
    }
}