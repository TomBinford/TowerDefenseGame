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
            spriteBatch.Draw(texture, position, source, tint, MathHelper.ToRadians(angle), origin, scale, SpriteEffects.None, 0);
        }

        public void ChangeTexture(Texture2D newTexture)
        {
            texture = newTexture;
            source = texture.Bounds;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}