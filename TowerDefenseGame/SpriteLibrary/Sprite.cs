using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLibrary
{
    public class Sprite
    {
        public Vector2 position;
        public Texture2D texture;
        public Color tint;
        public float angle;
        public float scale;

        public Sprite(Texture2D texture, Vector2 position, Color tint, float angle, float scale)
        {
            this.texture = texture;
            this.position = position;
            this.tint = tint;
            this.angle = angle;
            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, tint, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}