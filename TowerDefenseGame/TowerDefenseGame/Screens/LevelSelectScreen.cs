using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;

namespace TowerDefenseGame
{
    public class LevelSelectScreen : BaseScreen
    {
        Texture2D[] Numbers = new Texture2D[10];
        Texture2D[] Stars = new Texture2D[4];

        Button[] Buttons = new Button[10];

        Button CloseButton;
        Button LeftButton;
        Button RightButton;

        Sprite Background;
        Sprite Table;
        Sprite Header;

        int firstLevel;

        public LevelSelectScreen()
        {
            firstLevel = 1;
        }

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (CloseButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                return ScreenTypes.Main;
            }
            if (LeftButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                if (firstLevel > 1)
                {
                    firstLevel -= 10;
                }
            }
            if (RightButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                firstLevel += 10;
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
            Background = new Sprite(texture, GameState.Get.ScreenViewport.GetCenter(), Color.White, 0f, Math.Max(GameState.Get.ScreenViewport.Height / (float)texture.Height, GameState.Get.ScreenViewport.Width / (float)texture.Width));

            Table = new Sprite(Content.Load<Texture2D>("GUI/LevelSelect/Table"), GameState.Get.ScreenViewport.GetCenter(), Color.White, 0, Background.Scale);

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
        }

        public override void UpdatePositions()
        {
            Background.Position = GameState.Get.ScreenViewport.GetCenter();
            Background.Scale = Math.Max(GameState.Get.ScreenViewport.Height / (float)Background.Texture.Height, GameState.Get.ScreenViewport.Width / (float)Background.Texture.Width);

            Table.Position = Background.Position;
            Header.Position = new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f));
            
            CloseButton.Hitbox = new Rectangle(new Point((int)(Table.Position.X + (Table.Texture.Width / 2.4f)), (int)Table.Position.Y - (int)(Table.Texture.Height / 1.8f)), CloseButton.Hitbox.Size);
            
            LeftButton.Hitbox = new Rectangle(new Point((int)(Table.Position.X - (Table.Texture.Bounds.Width / 2f) - (LeftButton.Texture.Width / 2.3f)), (int)(Table.Position.Y + (Table.Texture.Height / 2f) - (LeftButton.Texture.Height / 1.6f))), LeftButton.Hitbox.Size);

            RightButton.Hitbox = new Rectangle(new Point(), RightButton.Hitbox.Size);
        }

        public override void GetFocus()
        {
            firstLevel = 1;
        }
    }
}