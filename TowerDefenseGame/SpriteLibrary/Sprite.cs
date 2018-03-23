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

        private Texture2D Texture;
        public Texture2D texture
        {
            get
            {
                return Texture;
            }
            set
            {
                Texture = value;
                source = Texture.Bounds;
                origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            }
        }

        public Color tint;
        public float angle;
        public float scale;
        public Vector2 origin;
        public Rectangle source;

        public Sprite(Texture2D texture, Vector2 position, Color tint, float angle, float scale)
        {
            this.texture = texture;
            this.position = position;
            this.tint = tint;
            this.angle = angle;
            this.scale = scale;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            source = texture.Bounds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, tint, MathHelper.ToRadians(angle), origin, scale, SpriteEffects.None, 0);
        }
    }
}