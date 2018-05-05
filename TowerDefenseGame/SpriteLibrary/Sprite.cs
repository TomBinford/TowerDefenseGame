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
        public Vector2 Position;

        private Texture2D texture;

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;

                if (texture != null)
                {
                    Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                }
            }
        }

        public Color Tint;
        public float Angle;
        public float Scale;
        public Vector2 Origin;

        public Sprite(Texture2D texture, Vector2 position, Color tint, float angle = 0, float scale = 1)
        {
            Texture = texture;
            Position = position;
            Tint = tint;
            Angle = angle;
            Scale = scale;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture == null)
            {
                return;
            }
            spriteBatch.Draw(Texture, Position, null, Tint, MathHelper.ToRadians(Angle), Origin, Scale, SpriteEffects.None, 0);
        }
    }
}