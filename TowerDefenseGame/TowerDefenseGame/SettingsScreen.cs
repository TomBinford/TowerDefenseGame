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
        Button BackButton;
        Sprite Background;

        Texture2D SoundOn;
        Texture2D SoundOff;
        Texture2D MusicOn;
        Texture2D MusicOff;

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

            if (BackButton.IsClicked(GameState.Get.CurrentMouse))
            {
                return PreviousScreen;
            }

            return ScreenTypes.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            BackButton.Draw(spriteBatch);
            MusicButton.Draw(spriteBatch);
            SoundButton.Draw(spriteBatch);
        }

        public override void Load(ContentManager Content)
        {
            Rectangle bounds;
            SoundOn = Content.Load<Texture2D>("GUI/SoundOn");
            SoundOff = Content.Load<Texture2D>("GUI/SoundOff");
            MusicOn = Content.Load<Texture2D>("GUI/MusicOn");
            MusicOff = Content.Load<Texture2D>("GUI/MusicOff");
            bounds = SoundOn.Bounds;
            bounds.X = 500;
            bounds.Y = 20;
            SoundButton = new Button(bounds, SoundOn, Color.White, 1f, 0.9f);

            bounds = MusicOn.Bounds;
            bounds.X = 300;
            bounds.Y = 20;
            MusicButton = new Button(bounds, MusicOn, Color.White, 1f, 0.9f);

            bounds = Content.Load<Texture2D>("GUI/CloseButton").Bounds;
            bounds.X = 300;
            bounds.Y = 20;
            
            BackButton = new Button(bounds, Content.Load<Texture2D>("GUI/CloseButton"), Color.White, 1f, 0.9f);
            Texture2D cemetery = Content.Load<Texture2D>("Backgrounds/Cemetery");
            Background = new Sprite(cemetery, GameState.Get.ScreenViewport.Bounds.Center.ToVector2(), Color.White, 0f, Math.Max(GameState.Get.ScreenViewport.Height / (float)cemetery.Height, GameState.Get.ScreenViewport.Width / (float)cemetery.Width));
        }
    }
}