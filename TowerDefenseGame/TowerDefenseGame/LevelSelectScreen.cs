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
        
        Sprite Background;
        Sprite Table;
        Sprite Header;
        int firstLevel;

        public LevelSelectScreen()
        {
            firstLevel = 0;
        }

        public override ScreenTypes Update(GameTime gameTime)
        {
            return ScreenTypes.None;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Table.Draw(spriteBatch);
            Header.Draw(spriteBatch);
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
        }

        public override void UpdatePositions()
        {
            Background.Position = GameState.Get.ScreenViewport.GetCenter();
            Table.Position = Background.Position;
            Header.Position = new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f));
        }

        public override void GetFocus()
        {
            firstLevel = 0;
        }
    }
}