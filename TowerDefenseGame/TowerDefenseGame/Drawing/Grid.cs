using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class Grid
    {
        Texture2D Pixel;

        public Color Tint;

        public Vector2 Spacing;

        public Rectangle Bounds;

        public Grid(Texture2D pixel, Vector2 spacing, Rectangle bounds, Color tint)
        {
            Pixel = pixel;
            Spacing = spacing;
            Bounds = bounds;
            Tint = tint;
        }

        public Grid(Texture2D pixel, Vector2 spacing, Rectangle bounds)
        {
            Pixel = pixel;
            Spacing = spacing;
            Bounds = bounds;
            Tint = Color.Black;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (float x = Bounds.X; x < Bounds.Right; x += Spacing.X)
            {
                spriteBatch.Draw(Pixel, new Vector2(x, Bounds.Y), null, Tint, 0f, Vector2.Zero, new Vector2(1, Bounds.Height), SpriteEffects.None, 0f);
            }
            for (float y = Bounds.Y; y < Bounds.Bottom; y += Spacing.Y)
            {
                spriteBatch.Draw(Pixel, new Vector2(Bounds.X, y), null, Tint, 0f, Vector2.Zero, new Vector2(Bounds.Width, 1), SpriteEffects.None, 0f);
            }
        }
    }
}