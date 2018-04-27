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
    public class Projectile : CircularHitboxSprite
    {
        public int MaxHits;
        private float speed;

        public Projectile(Texture2D texture, Vector2 position, Color tint, float speed, int maxHits, float angle)
            : base(texture, position, angle, 1)
        {
            MaxHits = maxHits;
            this.speed = speed;
        }

        public void Move()
        {
            Position.X += (float)Math.Sin(MathHelper.ToRadians(Angle));
            Position.Y += (float)Math.Cos(MathHelper.ToRadians(Angle));
        }

        public bool IntersectsWith(Balloon balloon)
        {
            return Intersects(balloon.Position);
        }
    }
}