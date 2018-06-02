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
    public class MainMenuScreen : BaseScreen
    {
        Button SettingsButton;
        Button SoundButton;
        Button MusicButton;
        Button StartButton;
        Button BuildButton;

        Sprite Background;

        Texture2D SoundOn;
        Texture2D SoundOff;
        Texture2D MusicOn;
        Texture2D MusicOff;

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (SettingsButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                return ScreenTypes.Settings;
            }
            if (SoundButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                GameState.SoundOn = !GameState.SoundOn;
                SoundButton.Texture = GameState.SoundOn ? SoundOn : SoundOff;
            }
            if (MusicButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                GameState.MusicOn = !GameState.MusicOn;
                MusicButton.Texture = GameState.MusicOn ? MusicOn : MusicOff;
            }
            if (StartButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                return ScreenTypes.LevelSelect;
            }
            if (BuildButton.IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                return ScreenTypes.Build;
            }
            return ScreenTypes.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            StartButton.Draw(spriteBatch);
            SettingsButton.Draw(spriteBatch);
            SoundButton.Draw(spriteBatch);
            MusicButton.Draw(spriteBatch);
            BuildButton.Draw(spriteBatch);
        }

        public override void Load(ContentManager Content)
        {
            Texture2D texture = Content.Load<Texture2D>("Backgrounds/Jungle");
            Background = new Sprite(texture, GameState.Screen.GetCenter(), Color.White, 0f, Math.Max(GameState.Screen.Height / (float)texture.Height, GameState.Screen.Width / (float)texture.Width));

            Rectangle bounds;
            texture = Content.Load<Texture2D>("GUI/Main/Settings");

            SoundOn = Content.Load<Texture2D>("GUI/Main/SoundOn");
            SoundOff = Content.Load<Texture2D>("GUI/Main/SoundOff");
            MusicOn = Content.Load<Texture2D>("GUI/Main/MusicOn");
            MusicOff = Content.Load<Texture2D>("GUI/Main/MusicOff");

            bounds = texture.Bounds;
            bounds.X = GameState.Screen.Width - (int)(texture.Width * 1.5f);
            bounds.Y = 40;
            SettingsButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            texture = Content.Load<Texture2D>("GUI/Main/Play");
            bounds = texture.Bounds;
            bounds.X = (int)(GameState.Screen.GetCenter().X - (bounds.Width / 2f));
            bounds.Y = (int)(GameState.Screen.GetCenter().Y - (bounds.Height / 2f));
            StartButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            texture = Content.Load<Texture2D>("GUI/LevelSelect/EmptyButton");
            bounds = texture.Bounds;
            bounds.X = (int)(GameState.Screen.Width - bounds.Width * 1.2f);
            bounds.Y = (int)(GameState.Screen.Height - bounds.Height * 1.2f);
            BuildButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            bounds = SoundOn.Bounds;
            bounds.X = 40;
            bounds.Y = 40;
            SoundButton = new Button(bounds, GameState.SoundOn ? SoundOn : SoundOff, Color.White, 1f, 0.9f);

            bounds.X += (int)(bounds.Width * 1.1f);
            MusicButton = new Button(bounds, GameState.MusicOn ? MusicOn : MusicOff, Color.White, 1f, 0.9f);
        }

        public override void UpdatePositions()
        {
            Background.Scale = Math.Max(GameState.Screen.Height / (float)Background.Texture.Height, GameState.Screen.Width / (float)Background.Texture.Width);
            Background.Position = GameState.Screen.GetCenter();

            BuildButton.Hitbox = new Rectangle(new Point((int)(GameState.Screen.Width - BuildButton.Hitbox.Width * 1.2f), (int)(GameState.Screen.Height - BuildButton.Hitbox.Height * 1.2f)), BuildButton.Hitbox.Size);

            SettingsButton.Hitbox = new Rectangle(new Point((GameState.Screen.Width - (int)(SettingsButton.Texture.Width * 1f)), 20), SettingsButton.Hitbox.Size);
            
            StartButton.Hitbox = new Rectangle(new Point((int)(GameState.Screen.GetCenter().X - (StartButton.Texture.Width / 2f)), (int)(GameState.Screen.GetCenter().Y - (StartButton.Texture.Height / 2f))), StartButton.Hitbox.Size);
        }

        public override void GetFocus()
        {
            SoundButton.Texture = GameState.SoundOn ? SoundOn : SoundOff;
            MusicButton.Texture = GameState.MusicOn ? MusicOn : MusicOff;
        }
    }
}