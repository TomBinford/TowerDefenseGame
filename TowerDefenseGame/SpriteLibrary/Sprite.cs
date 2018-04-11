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

        Texture2D _texture;
        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
               _texture = value;
                source =_texture.Bounds;
                origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }

        public Color tint;
        public float angle;
        public float scale;
        public Vector2 origin;
        public Rectangle source;

        public Sprite(Texture2D texture, Vector2 position, Color tint, float angle, float scale)
        {
            this.Texture = texture;
            this.position = position;
            this.tint = tint;
            this.angle = angle;
            this.scale = scale;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, null, tint, MathHelper.ToRadians(angle), origin, scale, SpriteEffects.None, 0);
        }
    }
}