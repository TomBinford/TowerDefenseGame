﻿using Microsoft.Xna.Framework;
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
        
        public Button(Rectangle hitbox, Texture2D background, Color tint, float normalScale, float clickedScale, SpriteFont font, string text)
            : base(background, hitbox.Center.ToVector2(), tint, normalScale, font, text)
        {
            Hitbox = hitbox;
            NormalScale = normalScale;
            ClickedScale = clickedScale;
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