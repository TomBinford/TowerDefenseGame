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

        Button StartButton;
        
        Sprite Background;

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (SettingsButton.IsClicked(GameState.Get.CurrentMouse))
            {
                return ScreenTypes.Settings;
            }
            if (StartButton.IsClicked(GameState.Get.CurrentMouse))
            {
                return ScreenTypes.LevelSelect;
            }
            return ScreenTypes.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            SettingsButton.Draw(spriteBatch);
        }

        public override void Load(ContentManager Content)
        {
            Background = new Sprite(Content.Load<Texture2D>("Backgrounds/Cemetery"), Vector2.Zero, Color.White, 0f, 1f);

            Rectangle bounds;
            Texture2D settings = Content.Load<Texture2D>("GUI/Settings");
            bounds = settings.Bounds;
            bounds.X = 500;
            bounds.Y = 20;
            SettingsButton = new Button(bounds, settings, Color.White, 1f, 1f);
        }
    }
}