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
    public class Label : Sprite
    {
        public SpriteFont Font;
        public string Text;

        public Label(Texture2D background, Vector2 position, Color tint, float scale, SpriteFont font, string text)
            : base(background, position, tint, 0f, scale)
        {
            Font = font;
            Text = text;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, Angle, Origin, Scale, Effect, 0);
            }
            if (Text != "" && Font != null)
            {
                spriteBatch.DrawString(Font, Text, new Vector2(Position.X - (Font.MeasureString(Text).X / 2), Position.Y), Tint, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}