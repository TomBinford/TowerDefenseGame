using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class SettingsScreen : BaseScreen
    {
        Button SoundButton;
        Button MusicButton;
        Button CloseButton;
        Sprite Background;

        Texture2D SoundOn;
        Texture2D SoundOff;
        Texture2D MusicOn;
        Texture2D MusicOff;

        Sprite Table;
        Sprite Window1;
        Sprite Header;

        Label MusicLabel;
        Label SoundLabel;

        public SettingsScreen(ScreenTypes previous)
        {
            PreviousScreen = previous;
        }

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (SoundButton.IsClicked(GameState.Get.CurrentMouse) && !SoundButton.IsClicked(GameState.Get.OldMouse))
            {
                GameState.Get.SoundOn = !GameState.Get.SoundOn;
            }

            if (MusicButton.IsClicked(GameState.Get.CurrentMouse) && !MusicButton.IsClicked(GameState.Get.OldMouse))
            {
                GameState.Get.MusicOn = !GameState.Get.MusicOn;
            }

            if (CloseButton.IsClicked(GameState.Get.CurrentMouse))
            {
                //return PreviousScreen;
            }

            return ScreenTypes.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Table.Draw(spriteBatch);
            Window1.Draw(spriteBatch);
            Header.Draw(spriteBatch);
            CloseButton.Draw(spriteBatch);
            MusicLabel.Draw(spriteBatch);
            //MusicButton.Draw(spriteBatch);
            //SoundButton.Draw(spriteBatch);
        }

        public override void Load(ContentManager Content)
        {
            Rectangle bounds;
            SoundOn = Content.Load<Texture2D>("GUI/Settings/SoundOn");
            SoundOff = Content.Load<Texture2D>("GUI/Settings/SoundOff");
            MusicOn = Content.Load<Texture2D>("GUI/Settings/MusicOn");
            MusicOff = Content.Load<Texture2D>("GUI/Settings/MusicOff");
            bounds = SoundOn.Bounds;
            bounds.X = 500;
            bounds.Y = 20;
            SoundButton = new Button(bounds, SoundOn, Color.White, 1f, 0.9f);

            bounds = MusicOn.Bounds;
            bounds.X = 300;
            bounds.Y = 20;
            MusicButton = new Button(bounds, MusicOn, Color.White, 1f, 0.9f);
            
            Texture2D cemetery = Content.Load<Texture2D>("Backgrounds/Cemetery");
            Background = new Sprite(cemetery, GameState.Get.ScreenViewport.GetCenter(), new Color(150, 150, 150), 0f, Math.Max(GameState.Get.ScreenViewport.Height / (float)cemetery.Height, GameState.Get.ScreenViewport.Width / (float)cemetery.Width));
            
            Table = new Sprite(Content.Load<Texture2D>("GUI/Settings/Table"), GameState.Get.ScreenViewport.GetCenter(), Color.White, 0, Background.Scale);

            Header = new Sprite(Content.Load<Texture2D>("GUI/Settings/Header"), new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f)), Color.White, 0, Background.Scale);

            Window1 = new Sprite(Content.Load<Texture2D>("GUI/Settings/Table1"), new Vector2(Table.Position.X - (Table.Texture.Width / 4.5f), Table.Position.Y + Table.Texture.Height / 25f), Color.White, 0, Background.Scale);

            bounds = Content.Load<Texture2D>("GUI/Settings/CloseButton").Bounds;
            bounds.X = (int)Table.Position.X + (int)(Table.Texture.Width / 2.4f);
            bounds.Y = (int)Table.Position.Y - (int)(Table.Texture.Height / 1.8f);
            CloseButton = new Button(bounds, Content.Load<Texture2D>("GUI/Settings/CloseButton"), Color.White, 1f, 0.9f);
            
            Vector2 Position = Table.Position;
            Position.X -= Table.Texture.Width / 3f;
            Position.Y -= Table.Texture.Height / 5f;
            MusicLabel = new Label(null, Position, Color.White, 1f, Content.Load<SpriteFont>("Font"), "Music");
        }

        public override void UpdatePositions()
        {
            Table.Position = GameState.Get.ScreenViewport.GetCenter();
            Header.Position = new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f));
            CloseButton.Position = CloseButton.Hitbox.Location.ToVector2();
            CloseButton.Hitbox = new Rectangle(new Point((int)Table.Position.X + (int)(Table.Texture.Width / 2.4f), (int)Table.Position.Y - (int)(Table.Texture.Height / 1.8f)), CloseButton.Hitbox.Size);
        }
    }
}