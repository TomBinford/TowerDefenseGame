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
    public class CircularHitboxSprite : Sprite
    {
        public CircularHitbox Hitbox;
        
        public CircularHitboxSprite(Texture2D texture, Vector2 position, float radius, float angle = 0, float scale = 1)
            : base(texture, position, Color.White, angle, scale)
        {
            Hitbox = new CircularHitbox(radius);
        }

        public bool Intersects(Vector2 point)
        {
            return Vector2.Distance(point, Position) <= Hitbox.Radius;
        }
    }
}