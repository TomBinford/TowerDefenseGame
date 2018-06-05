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
    public class Button : Label
    {
        public float NormalScale;
        public float ClickedScale;

        private Rectangle hitbox;

        public Rectangle Hitbox
        {
            get
            {
                return hitbox;
            }

            set
            {
                hitbox = value;
                Position = hitbox.Center.ToVector2();
            }
        }
        
        public Button(Rectangle hitbox, Texture2D background, Color tint, float normalScale, float clickedScale, SpriteFont font = null, string text = "")
            : base(background, hitbox.Center.ToVector2(), tint, normalScale, font, text)
        {
            Hitbox = hitbox.Scale(normalScale);
            NormalScale = normalScale;
            ClickedScale = clickedScale;
        }

        public bool IsMousedOver(MouseState State)
        {
            return Hitbox.Contains(State.Position);
        }

        public bool IsPressed(MouseState State)
        {
            if (IsMousedOver(State) && (State.LeftButton == ButtonState.Pressed))
            {
                Scale = new Vector2(ClickedScale);
                return true;
            }
            else
            {
                Scale = new Vector2(NormalScale);
                return false;
            }
        }

        public bool IsClicked(MouseState newState, MouseState oldState)
        {
            return IsPressed(GameState.CurrentMouse) && !IsPressed(GameState.OldMouse) && IsMousedOver(GameState.OldMouse);
        }
    }
}