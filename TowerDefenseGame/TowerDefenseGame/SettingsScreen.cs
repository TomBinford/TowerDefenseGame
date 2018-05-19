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
        Button VibrationButton;
        Button NotificationButton;

        Button CloseButton;
        Sprite Background;

        Sprite Table;
        Sprite Window;
        Sprite Header;

        Label MusicLabel;
        Label SoundLabel;
        Label VibrationLabel;
        Label NotificationLabel;

        Texture2D On;
        Texture2D Off;
        
        public override ScreenTypes Update(GameTime gameTime)
        {
            if (SoundButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                GameState.Get.SoundOn = !GameState.Get.SoundOn;
                SoundButton.Texture = GameState.Get.SoundOn ? On : Off;
            }
            
            if (MusicButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                GameState.Get.MusicOn = !GameState.Get.MusicOn;
                MusicButton.Texture = GameState.Get.MusicOn ? On : Off;
            }

            if (VibrationButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                GameState.Get.VibrationOn = !GameState.Get.VibrationOn;
                VibrationButton.Texture = GameState.Get.VibrationOn ? On : Off;
            }

            if (NotificationButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                GameState.Get.NotificationOn = !GameState.Get.NotificationOn;
                NotificationButton.Texture = GameState.Get.NotificationOn ? On : Off;
            }
            
            if (CloseButton.IsClicked(GameState.Get.CurrentMouse, GameState.Get.OldMouse))
            {
                return PreviousScreen;
            }

            return ScreenTypes.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Table.Draw(spriteBatch);
            Window.Draw(spriteBatch);
            Header.Draw(spriteBatch);
            CloseButton.Draw(spriteBatch);
            MusicLabel.Draw(spriteBatch);
            MusicButton.Draw(spriteBatch);
            SoundLabel.Draw(spriteBatch);
            SoundButton.Draw(spriteBatch);
            VibrationLabel.Draw(spriteBatch);
            VibrationButton.Draw(spriteBatch);
            NotificationLabel.Draw(spriteBatch);
            NotificationButton.Draw(spriteBatch);
        }

        public override void Load(ContentManager Content)
        {
            On = Content.Load<Texture2D>("GUI/Settings/ButtonOn");
            Off = Content.Load<Texture2D>("GUI/Settings/ButtonOff");

            Rectangle bounds;
            bounds = On.Bounds;
            bounds.X = 500;
            bounds.Y = 20;
            SoundButton = new Button(bounds, On, Color.White, 1f, 0.9f);

            Texture2D cemetery = Content.Load<Texture2D>("Backgrounds/Cemetery");
            Background = new Sprite(cemetery, GameState.Get.ScreenViewport.GetCenter(), new Color(150, 150, 150), 0f, Math.Max(GameState.Get.ScreenViewport.Height / (float)cemetery.Height, GameState.Get.ScreenViewport.Width / (float)cemetery.Width));

            Table = new Sprite(Content.Load<Texture2D>("GUI/Settings/Table"), GameState.Get.ScreenViewport.GetCenter(), Color.White, 0, Background.Scale);

            Header = new Sprite(Content.Load<Texture2D>("GUI/Settings/Header"), new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f)), Color.White, 0, Background.Scale);

            Window = new Sprite(Content.Load<Texture2D>("GUI/Settings/Table1"), new Vector2(Table.Position.X - (Table.Texture.Width / 4.5f), Table.Position.Y + Table.Texture.Height / 25f), Color.White, 0, Background.Scale);

            bounds = Content.Load<Texture2D>("GUI/Settings/CloseButton").Bounds;
            bounds.X = (int)Table.Position.X + (int)(Table.Texture.Width / 2.4f);
            bounds.Y = (int)Table.Position.Y - (int)(Table.Texture.Height / 1.8f);
            CloseButton = new Button(bounds, Content.Load<Texture2D>("GUI/Settings/CloseButton"), Color.White, 1f, 0.9f);

            MusicLabel = new Label(null, new Vector2(Table.Position.X - (Table.Texture.Width / 3f), Table.Position.Y - (Table.Texture.Height / 3.8f)), new Color(219, 200, 153), 1f, Content.Load<SpriteFont>("Font"), "Music");
            SoundLabel = new Label(null, new Vector2(MusicLabel.Position.X, MusicLabel.Position.Y + (On.Height * 1.3f)), MusicLabel.Tint, 1f, MusicLabel.Font, "Sound");
            VibrationLabel = new Label(null, new Vector2(SoundLabel.Position.X, SoundLabel.Position.Y + (On.Height * 1.3f)), SoundLabel.Tint, 1f, SoundLabel.Font, "Vibration");
            NotificationLabel = new Label(null, new Vector2(VibrationLabel.Position.X, VibrationLabel.Position.Y + (On.Height * 1.3f)), VibrationLabel.Tint, 1f, VibrationLabel.Font, "Notification");

            bounds = On.Bounds;
            bounds.X = (int)(MusicLabel.Position.X + (On.Width));
            bounds.Y = (int)(MusicLabel.Position.Y - (On.Height / 6f));
            MusicButton = new Button(bounds, On, Color.White, 1f, 1f);

            bounds = On.Bounds;
            bounds.X = (int)(SoundLabel.Position.X + (On.Width));
            bounds.Y = (int)(SoundLabel.Position.Y - (On.Height / 6f));
            SoundButton = new Button(bounds, On, Color.White, 1f, 1f);

            bounds = On.Bounds;
            bounds.X = (int)(VibrationLabel.Position.X + (On.Width));
            bounds.Y = (int)(VibrationLabel.Position.Y - (On.Height / 6f));
            VibrationButton = new Button(bounds, On, Color.White, 1f, 1f);

            bounds = On.Bounds;
            bounds.X = (int)(NotificationLabel.Position.X + (On.Width));
            bounds.Y = (int)(NotificationLabel.Position.Y - (On.Height / 6f));
            NotificationButton = new Button(bounds, On, Color.White, 1f, 1f);
        }

        public override void UpdatePositions()
        {
            Table.Position = GameState.Get.ScreenViewport.GetCenter();
            Header.Position = new Vector2(Table.Position.X, Table.Position.Y - (Table.Texture.Height / 2.5f));
            Window.Position = new Vector2(Table.Position.X - (Table.Texture.Width / 4.5f), Table.Position.Y + (Table.Texture.Height / 25f));
            MusicLabel.Position = new Vector2(Table.Position.X - (Table.Texture.Width / 3f), Table.Position.Y - (Table.Texture.Height / 5f));
            SoundLabel.Position = new Vector2(MusicLabel.Position.X, MusicLabel.Position.Y + (On.Height * 1f));
            VibrationLabel.Position = new Vector2(SoundLabel.Position.X, SoundLabel.Position.Y + (On.Height * 1f));
            NotificationLabel.Position = new Vector2(VibrationLabel.Position.X, VibrationLabel.Position.Y + (On.Height * 1f));

            CloseButton.Position = CloseButton.Hitbox.Location.ToVector2();
            CloseButton.Hitbox = new Rectangle(new Point((int)Table.Position.X + (int)(Table.Texture.Width / 2.4f), (int)Table.Position.Y - (int)(Table.Texture.Height / 1.8f)), CloseButton.Hitbox.Size);

            MusicButton.Position = MusicButton.Hitbox.Location.ToVector2();
            MusicButton.Hitbox = new Rectangle(new Point((int)(MusicLabel.Position.X + (On.Width)), (int)(MusicLabel.Position.Y - (On.Height / 5f))), MusicButton.Hitbox.Size);

            SoundButton.Position = SoundButton.Hitbox.Location.ToVector2();
            SoundButton.Hitbox = new Rectangle(new Point((int)(SoundLabel.Position.X + (On.Width)), (int)(SoundLabel.Position.Y - (On.Height / 5f))), SoundButton.Hitbox.Size);

            VibrationButton.Position = VibrationButton.Hitbox.Location.ToVector2();
            VibrationButton.Hitbox = new Rectangle(new Point((int)(VibrationLabel.Position.X + (On.Width)), (int)(VibrationLabel.Position.Y - (On.Height / 5f))), VibrationButton.Hitbox.Size);

            NotificationButton.Position = NotificationButton.Hitbox.Location.ToVector2();
            NotificationButton.Hitbox = new Rectangle(new Point((int)(NotificationLabel.Position.X + (On.Width)), (int)(NotificationLabel.Position.Y - (On.Height / 5f))), NotificationButton.Hitbox.Size);
        }

        public override void GetFocus()
        {
            SoundButton.Texture = GameState.Get.SoundOn ? On : Off;
            MusicButton.Texture = GameState.Get.MusicOn ? On : Off;
            VibrationButton.Texture = GameState.Get.VibrationOn ? On : Off;
            NotificationButton.Texture = GameState.Get.NotificationOn ? On : Off;
        }
    }
}