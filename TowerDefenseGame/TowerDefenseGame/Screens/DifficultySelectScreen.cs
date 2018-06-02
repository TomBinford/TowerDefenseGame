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
    public class DifficultySelectScreen : BaseScreen
    {
        Button EasyButton;
        Button NormalButton;
        Button HardButton;

        Sprite Background;
        Sprite Header;
        Sprite Table;
        Sprite Window;

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (EasyButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                GameState.Difficulty = Difficulties.Easy;
                return ScreenTypes.LevelSelect;
            }
            if (NormalButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                GameState.Difficulty = Difficulties.Normal;
                return ScreenTypes.LevelSelect;
            }
            if (HardButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                GameState.Difficulty = Difficulties.Hard;
                return ScreenTypes.LevelSelect;
            }
            return ScreenTypes.None;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Table.Draw(spriteBatch);
            Window.Draw(spriteBatch);
            Header.Draw(spriteBatch);
            EasyButton.Draw(spriteBatch);
            NormalButton.Draw(spriteBatch);
            HardButton.Draw(spriteBatch);
        }

        public override void Load(ContentManager Content)
        {
            Texture2D texture = Content.Load<Texture2D>("Backgrounds/Lava");
            Background = new Sprite(texture, GameState.Screen.GetCenter(), new Color(150, 150, 150), 0, Math.Max(GameState.Screen.Height / (float)texture.Height, GameState.Screen.Width / (float)texture.Width));

            Table = new Sprite(Content.Load<Texture2D>("GUI/Difficulty/Table"), GameState.Screen.GetCenter(), Color.White, 0, Background.Scale);

            texture = Content.Load<Texture2D>("GUI/Difficulty/Header");

            Header = new Sprite(texture, new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f)), Color.White, 0, Background.Scale);

            Window = new Sprite(Content.Load<Texture2D>("GUI/Difficulty/Window"), Table.Position, Color.White, 0, Background.Scale);

            texture = Content.Load<Texture2D>("GUI/Difficulty/Easy");
            Rectangle bounds = texture.Bounds;
            bounds.X = (int)(GameState.Screen.GetCenter().X - (texture.Width / 2f));
            bounds.Y = (int)(Table.Position.Y - (texture.Height * 1.4f));
            EasyButton = new Button(bounds, texture, Color.White, 1f, 1f);

            texture = Content.Load<Texture2D>("GUI/Difficulty/Normal");
            bounds = texture.Bounds;
            bounds.X = EasyButton.Hitbox.X;
            bounds.Y = EasyButton.Hitbox.Y + EasyButton.Texture.Height;
            NormalButton = new Button(bounds, texture, Color.White, 1f, 1f);

            texture = Content.Load<Texture2D>("GUI/Difficulty/Hard");
            bounds.X = EasyButton.Hitbox.X;
            bounds.Y = (int)(NormalButton.Position.Y + (NormalButton.Texture.Height / 2f));
            HardButton = new Button(bounds, texture, Color.White, 1f, 1f);
        }

        public override void UpdatePositions()
        {
            Background.Scale = Math.Max(GameState.Screen.Height / (float)Background.Texture.Height, GameState.Screen.Width / (float)Background.Texture.Width);
            Background.Position = GameState.Screen.GetCenter();
            Table.Position = GameState.Screen.GetCenter();
            Window.Position = GameState.Screen.GetCenter();
            Header.Position = new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f));

            EasyButton.Position = EasyButton.Hitbox.Location.ToVector2();
            EasyButton.Hitbox = new Rectangle(new Point((int)(GameState.Screen.GetCenter().X - (EasyButton.Texture.Width / 2f)), (int)(Table.Position.Y - (EasyButton.Texture.Height * 1.4f))), EasyButton.Hitbox.Size);

            NormalButton.Position = NormalButton.Hitbox.Location.ToVector2();
            NormalButton.Hitbox = new Rectangle(new Point((int)(GameState.Screen.GetCenter().X - (NormalButton.Texture.Width / 2f)), (int)(EasyButton.Position.Y + (NormalButton.Texture.Height / 2f))), NormalButton.Hitbox.Size);

            HardButton.Position = HardButton.Hitbox.Location.ToVector2();
            HardButton.Hitbox = new Rectangle(new Point((int)(GameState.Screen.GetCenter().X - (HardButton.Texture.Width / 2f)), (int)(NormalButton.Position.Y + (HardButton.Texture.Height / 2f))), HardButton.Hitbox.Size);
        }

        public override void GetFocus()
        {

        }
    }
}