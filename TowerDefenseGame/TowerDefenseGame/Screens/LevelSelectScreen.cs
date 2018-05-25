using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;
using TowerDefenseGame.Screens;

namespace TowerDefenseGame
{
    public class LevelSelectScreen : BaseScreen
    {
        Texture2D[] Numbers;
        Texture2D[] Stars;

        Button[] Buttons;
        Sprite[] ButtonStars;
        int[] ButtonNumbers;

        Button CloseButton;
        Button LeftButton;
        Button RightButton;

        Sprite Background;
        Sprite Table;
        Sprite Header;

        private int firstLevel;
        private int FirstLevel
        {
            get
            {
                return firstLevel;
            }
            set
            {
                firstLevel = value;
                for (int i = 0; i < ButtonNumbers.Length; i++)
                {
                    ButtonNumbers[i] = firstLevel - 1 + i;
                }
            }
        }

        public LevelSelectScreen()
        {
            Buttons = new Button[8];
            ButtonStars = new Sprite[8];
            ButtonNumbers = new int[8];
            Numbers = new Texture2D[8];
            Stars = new Texture2D[4];
            FirstLevel = 1;
        }

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (CloseButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                return ScreenTypes.Main;
            }
            if (LeftButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                if (FirstLevel > 1)
                {
                    FirstLevel -= 10;
                    UpdateSprites();
                }
            }
            if (RightButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                FirstLevel += 10;
                UpdateSprites();
            }
            return ScreenTypes.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Table.Draw(spriteBatch);
            Header.Draw(spriteBatch);
            CloseButton.Draw(spriteBatch);
            LeftButton.Draw(spriteBatch);
            RightButton.Draw(spriteBatch);
            for (int i = 0; i < 8; i++)
            {
                Buttons[i].Draw(spriteBatch);
            }
            NumberDrawer.Draw(spriteBatch, ButtonNumbers[0], Buttons[0].Position);
        }

        public override void Load(ContentManager Content)
        {
            for (int i = 0; i < Numbers.Length; i++)
            {
                Numbers[i] = Content.Load<Texture2D>($"GUI/LevelSelect/Numbers/{i}");
            }
            Stars[0] = Content.Load<Texture2D>("GUI/LevelSelect/Stars/NoStars");
            Stars[1] = Content.Load<Texture2D>("GUI/LevelSelect/Stars/OneStar");
            Stars[2] = Content.Load<Texture2D>("GUI/LevelSelect/Stars/TwoStars");
            Stars[3] = Content.Load<Texture2D>("GUI/LevelSelect/Stars/ThreeStars");

            Texture2D texture = Content.Load<Texture2D>("Backgrounds/Desert");
            Background = new Sprite(texture, GameState.ScreenViewport.GetCenter(), Color.White, 0f, Math.Max(GameState.ScreenViewport.Height / (float)texture.Height, GameState.ScreenViewport.Width / (float)texture.Width));

            Table = new Sprite(Content.Load<Texture2D>("GUI/LevelSelect/Table"), GameState.ScreenViewport.GetCenter(), Color.White, 0, Background.Scale);

            texture = Content.Load<Texture2D>("GUI/LevelSelect/Header");
            Header = new Sprite(texture, new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f)), Color.White, 0, Background.Scale);

            texture = Content.Load<Texture2D>("GUI/LevelSelect/CloseButton");
            Rectangle bounds = texture.Bounds;
            bounds.X = (int)Table.Position.X + (int)(Table.Texture.Width / 2.4f);
            bounds.Y = (int)Table.Position.Y - (int)(Table.Texture.Height / 1.8f);
            CloseButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            texture = Content.Load<Texture2D>("GUI/LevelSelect/LeftButton");
            bounds = texture.Bounds;
            bounds.X = (int)(Table.Position.X - (Table.Texture.Width / 2f) - (bounds.Width / 2.3f));
            bounds.Y = (int)(Table.Position.Y + (Table.Texture.Height / 2f) - (bounds.Height / 1.6f));
            LeftButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            texture = Content.Load<Texture2D>("GUI/LevelSelect/RightButton");
            bounds = texture.Bounds;
            bounds.X = LeftButton.Hitbox.X + (int)(Table.Texture.Width * 0.97f);
            bounds.Y = LeftButton.Hitbox.Y;
            RightButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            texture = Content.Load<Texture2D>("GUI/LevelSelect/EmptyButton");
            bounds = texture.Bounds;
            bounds.X = (int)(Table.Position.X - (Table.Texture.Width / 2.34f));
            bounds.Y = (int)(Header.Position.Y + (texture.Height * 0.5f));
            
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Buttons[y * 4 + x] = new Button(bounds, texture, Color.White, 1f, 0.9f);
                    bounds.X += (int)(texture.Width * 1.22f);
                }
                bounds.X = (int)(Table.Position.X - (Table.Texture.Width / 2.34));
                bounds.Y += (int)(texture.Height * 1.27f);
            }

            UpdateSprites();
        }

        private void UpdateSprites()
        {
            
        }

        public override void UpdatePositions()
        {
            Background.Position = GameState.ScreenViewport.GetCenter();
            Background.Scale = Math.Max(GameState.ScreenViewport.Height / (float)Background.Texture.Height, GameState.ScreenViewport.Width / (float)Background.Texture.Width);

            Table.Position = Background.Position;
            Header.Position = new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f));
            
            CloseButton.Hitbox = new Rectangle(new Point((int)(Table.Position.X + (Table.Texture.Width / 2.4f)), (int)Table.Position.Y - (int)(Table.Texture.Height / 1.8f)), CloseButton.Hitbox.Size);
            
            LeftButton.Hitbox = new Rectangle(new Point((int)(Table.Position.X - (Table.Texture.Bounds.Width / 2f) - (LeftButton.Texture.Width / 2.3f)), (int)(Table.Position.Y + (Table.Texture.Height / 2f) - (LeftButton.Texture.Height / 1.6f))), LeftButton.Hitbox.Size);

            RightButton.Hitbox = new Rectangle(new Point(LeftButton.Hitbox.X + (int)(Table.Texture.Width * 0.97f), LeftButton.Hitbox.Y), RightButton.Hitbox.Size);

            Rectangle bounds = Buttons[0].Hitbox;
            bounds.X = (int)(Table.Position.X - (Table.Texture.Width / 2.34f));
            bounds.Y = (int)(Header.Position.Y + (Buttons[0].Texture.Height * 0.5f));
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Buttons[y * 4 + x].Hitbox = bounds;
                    bounds.X += (int)(Buttons[0].Texture.Width * 1.22f);
                }
                bounds.X = (int)(Table.Position.X - (Table.Texture.Width / 2.34));
                bounds.Y += (int)(Buttons[0].Texture.Height * 1.27f);
            }
        }

        public override void GetFocus()
        {
            FirstLevel = 1;
            UpdateSprites();
        }
    }
}