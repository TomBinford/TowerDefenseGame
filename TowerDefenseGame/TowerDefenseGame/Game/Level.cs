using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;
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
        public List<Sprite> Decorations;
        public List<Vector2> TurretPoints;
        public List<Sprite> Background;
        public Themes Theme;

        //List<RoadPieces>
        //List<Sprites> -> decorations
        //List<TurretPoints> -> Sprites + Tag?/TurretPoint?

        //RoadPiece: Sprite + ConnectionPoints(Vector2's) + PathPoints(Vector2's)

        public Level()
        {
            Stars = 0;
            Road = new List<RoadPiece>();
            Decorations = new List<Sprite>();
            TurretPoints = new List<Vector2>();
            Background = new List<Sprite>();
        }

        public Level(int stars)
        {
            Stars = (stars >= 0 && stars < 4) ? stars : throw new ArgumentOutOfRangeException("Star value must be between 0 and 3");
            Road = new List<RoadPiece>();
            Decorations = new List<Sprite>();
            TurretPoints = new List<Vector2>();
            Background = new List<Sprite>();
        }

        public void Draw(SpriteBatch spriteBatch, Sprite turretPoint)
        {
            foreach (Sprite s in Background)
            {
                s.Draw(spriteBatch);
            }
            foreach (RoadPiece r in Road)
            {
                r.Draw(spriteBatch);
            }
            foreach (Vector2 position in TurretPoints)
            {
                turretPoint.Position = position;
                turretPoint.Draw(spriteBatch);
            }
            foreach (Sprite s in Decorations)
            {
                s.Draw(spriteBatch);
            }
        }
    }
}