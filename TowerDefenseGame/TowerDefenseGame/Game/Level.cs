using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame.Screens
{
    public class Level
    {
        public int Stars;

        public Level()
        {
            Stars = 0;
        }

        public Level(int stars)
        {
            Stars = (stars >= 0 && stars < 4) ? stars : throw new ArgumentOutOfRangeException("Star value must be between 0 and 3");
        }
    }
}