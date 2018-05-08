using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class CircularHitbox
    {
        public float Radius;
        public Vector2 Position;

        public CircularHitbox(float r, Vector2 position)
        {
            Radius = r;
            Position = position;
        }

        public bool Intersects(Vector2 position)
        {
            return (Vector2.Distance(Position, position) <= Radius);
        }
    }
}