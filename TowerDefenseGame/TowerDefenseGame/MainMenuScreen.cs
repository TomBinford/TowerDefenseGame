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
        Button settingsButton;
        private SettingsScreen settings;
        public SettingsScreen Settings
        {
            get
            {
                return settings;
            }
            set
            {
                settings = value;
                settings.PreviousScreen = this;
            }
        }

        Button startButton;

        Button SoundButton;
        Button MusicButton;

        Texture2D SoundOn;
        Texture2D SoundOff;

        Texture2D MusicOn;
        Texture2D MusicOff;

        Sprite background;

        public override void Update(GameTime gameTime)
        {
            if (settingsButton.IsClicked(GameState.Get.CurrentMouse))
            {
                ScreenManager.ActiveScreen = Settings;
            }

            if (SoundButton.IsClicked(GameState.Get.CurrentMouse) && !SoundButton.IsClicked(GameState.Get.OldMouse))
            {
                GameState.Get.SoundOn = !GameState.Get.SoundOn;
                if (GameState.Get.SoundOn)
                {
                    SoundButton.Texture = SoundOn;
                }
                else
                {
                    SoundButton.Texture = SoundOff;
                }
            }

            if (MusicButton.IsClicked(GameState.Get.CurrentMouse) && !MusicButton.IsClicked(GameState.Get.OldMouse))
            {
                GameState.Get.MusicOn = !GameState.Get.MusicOn;
                if (GameState.Get.MusicOn)
                {
                    MusicButton.Texture = MusicOn;
                }
                else
                {
                    MusicButton.Texture = MusicOff;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            SoundButton.Draw(spriteBatch);
            MusicButton.Draw(spriteBatch);
            Settings.Draw(spriteBatch);
        }

        public override void Load(ContentManager Content)
        {
            background = new Sprite(Content.Load<Texture2D>("Backgrounds/Cemetery"), Vector2.Zero, Color.White);
            SoundOn = Content.Load<Texture2D>("GUI/SoundOn");
            SoundOff = Content.Load<Texture2D>("GUI/SoundOff");
            MusicOn = Content.Load<Texture2D>("GUI/MusicOn");
            MusicOff = Content.Load<Texture2D>("GUI/MusicOff");

            Rectangle bounds = SoundOn.Bounds;
            bounds.X = 100;
            bounds.Y = 20;
            SoundButton = new Button(bounds, SoundOn, Color.White, 1f, 0.9f);

            bounds = MusicOn.Bounds;
            bounds.X = 200;
            bounds.Y = 20;
            MusicButton = new Button(bounds, MusicOn, Color.White, 1f, 0.9f);

            Texture2D settings = Content.Load<Texture2D>("GUI/Settings");
            bounds = settings.Bounds;
            bounds.X = 500;
            bounds.Y = 20;
            settingsButton = new Button(bounds, settings, Color.White, 1f, 1f);
        }
    }
}