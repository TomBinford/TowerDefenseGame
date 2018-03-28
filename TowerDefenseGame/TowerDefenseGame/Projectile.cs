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
        public float radius;

        int maxHits;
        float speed;
        
        public Projectile(Texture2D texture, Vector2 position, Color tint, float speed, int maxHits)
            : base(texture, position, tint, 0, 1)
        {
            this.maxHits = maxHits;
            this.speed = speed;
        }

        public void Move()
        {
            position.X += (float)Math.Sin(MathHelper.ToRadians(angle));
            position.Y += (float)Math.Cos(MathHelper.ToRadians(angle));
        }

        public bool IntersectsWith(Balloon balloon)
        {
            return Vector2.Distance(balloon.position, position) - radius - balloon.radius < 0;
        }
    }
}
