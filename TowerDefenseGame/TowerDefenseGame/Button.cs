using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class Button : Sprite
    {
        public SpriteFont Font;
        public string Text;
        
        public float NormalScale;
        public float ClickedScale;

        public Rectangle Hitbox;
        
        public Button(Rectangle hitbox, Texture2D background, float normalScale, float clickedScale, SpriteFont font, string text)
            : base(background, new Vector2(hitbox.X + hitbox.Width / 2, hitbox.Y + hitbox.Height / 2), Color.White, 0f, normalScale)
        {
            Font = font;
            Text = text;
            Hitbox = hitbox;
            NormalScale = normalScale;
            ClickedScale = clickedScale;
            Scale = NormalScale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Text != "")
            {
                spriteBatch.DrawString(Font, Text, Hitbox.Location.ToVector2(), Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }

        public bool IsMousedOver(MouseState State)
        {
            return Hitbox.Contains(State.Position);
        }

        public bool IsClicked(MouseState State)
        {
            if (IsMousedOver(State) && (State.LeftButton == ButtonState.Pressed))
            {
                Scale = ClickedScale;
                return true;
            }
            else
            {
                Scale = NormalScale;
                return false;
            }
        }
    }
}