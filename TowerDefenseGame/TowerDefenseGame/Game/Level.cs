using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class Level
    {
        public int Stars;
        public List<RoadPiece> Road;

        //List<RoadPieces>
        //List<Sprites> -> decorations
        //List<TurretPoints> -> Sprites + Tag?/TurretPoint?

        //RoadPiece: Sprite + ConnectionPoints(Vector2's) + PathPoints(Vector2's)

        public Level()
        {
            Stars = 0;
            Road = new List<RoadPiece>();
        }

        public Level(int stars)
        {
            Stars = (stars >= 0 && stars < 4) ? stars : throw new ArgumentOutOfRangeException("Star value must be between 0 and 3");
        }
    }
}