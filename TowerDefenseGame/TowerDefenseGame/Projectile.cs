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
    public class Projectile : Sprite
    {
        public float Radius;

        public int maxHits;
        private float speed;

        public Projectile(Texture2D texture, Vector2 position, Color tint, float speed, int maxHits, float angle)
            : base(texture, position, tint, angle, 1)
        {
            this.maxHits = maxHits;
            this.speed = speed;
        }

        public void Move()
        {
            position.X += (float)Math.Cos(MathHelper.ToRadians(angle)) * speed;
            position.Y += (float)Math.Sin(MathHelper.ToRadians(angle)) * speed;
        }

        public bool IntersectsWith(Balloon balloon)
        {
            return Vector2.Distance(balloon.position, position) - Radius - balloon.Radius < 0;
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return rect.Intersects(new Rectangle(position.ToPoint(), new Point(0, 0)));
        }
    }
}