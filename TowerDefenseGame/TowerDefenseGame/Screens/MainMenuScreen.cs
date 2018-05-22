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

        Sprite Background;

        Texture2D SoundOn;
        Texture2D SoundOff;
        Texture2D MusicOn;
        Texture2D MusicOff;

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (SettingsButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                return ScreenTypes.Settings;
            }
            if (SoundButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                GameState.Get.SoundOn = !GameState.Get.SoundOn;
                SoundButton.Texture = GameState.Get.SoundOn ? SoundOn : SoundOff;
            }
            if (MusicButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                GameState.Get.MusicOn = !GameState.Get.MusicOn;
                MusicButton.Texture = GameState.Get.MusicOn ? MusicOn : MusicOff;
            }
            if (StartButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                return ScreenTypes.LevelSelect;
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
        }

        public override void Load(ContentManager Content)
        {
            Texture2D texture = Content.Load<Texture2D>("Backgrounds/Jungle");
            Background = new Sprite(texture, GameState.Get.ScreenViewport.GetCenter(), Color.White, 0f, Math.Max(GameState.Get.ScreenViewport.Height / (float)texture.Height, GameState.Get.ScreenViewport.Width / (float)texture.Width));

            Rectangle bounds;
            texture = Content.Load<Texture2D>("GUI/Main/Settings");

            SoundOn = Content.Load<Texture2D>("GUI/Main/SoundOn");
            SoundOff = Content.Load<Texture2D>("GUI/Main/SoundOff");
            MusicOn = Content.Load<Texture2D>("GUI/Main/MusicOn");
            MusicOff = Content.Load<Texture2D>("GUI/Main/MusicOff");

            bounds = texture.Bounds;
            bounds.X = GameState.Get.ScreenViewport.Width - (int)(texture.Width * 1.5f);
            bounds.Y = 40;
            SettingsButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            texture = Content.Load<Texture2D>("GUI/Main/Play");
            bounds = texture.Bounds;
            bounds.X = (int)(GameState.Get.ScreenViewport.GetCenter().X - (bounds.Width / 2f));
            bounds.Y = (int)(GameState.Get.ScreenViewport.GetCenter().Y - (bounds.Height / 2f));
            StartButton = new Button(bounds, texture, Color.White, 1f, 0.9f);

            bounds = SoundOn.Bounds;
            bounds.X = 40;
            bounds.Y = 40;
            SoundButton = new Button(bounds, GameState.Get.SoundOn ? SoundOn : SoundOff, Color.White, 1f, 0.9f);

            bounds.X += (int)(bounds.Width * 1.1f);
            MusicButton = new Button(bounds, GameState.Get.MusicOn ? MusicOn : MusicOff, Color.White, 1f, 0.9f);
        }

        public override void UpdatePositions()
        {
            Background.Scale = Math.Max(GameState.Get.ScreenViewport.Height / (float)Background.Texture.Height, GameState.Get.ScreenViewport.Width / (float)Background.Texture.Width);
            Background.Position = GameState.Get.ScreenViewport.GetCenter();
            
            SettingsButton.Hitbox = new Rectangle(new Point((GameState.Get.ScreenViewport.Width - (int)(SettingsButton.Texture.Width * 1f)), 20), SettingsButton.Hitbox.Size);
            
            StartButton.Hitbox = new Rectangle(new Point((int)(GameState.Get.ScreenViewport.GetCenter().X - (StartButton.Texture.Width / 2f)), (int)(GameState.Get.ScreenViewport.GetCenter().Y - (StartButton.Texture.Height / 2f))), StartButton.Hitbox.Size);
        }

        public override void GetFocus()
        {
            SoundButton.Texture = GameState.Get.SoundOn ? SoundOn : SoundOff;
            MusicButton.Texture = GameState.Get.MusicOn ? MusicOn : MusicOff;
        }
    }
}