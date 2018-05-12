using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpriteLibrary;
using System;
using System.Collections.Generic;

namespace TowerDefenseGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Sprite MouseSprite;
        Vector2 MouseOffset;

        Label TestLabel;
        Button TestButton;
        bool SoundOn;
        Texture2D soundOn;
        Texture2D soundOff;

        List<Tower> towers;
        
        Random random = new Random();
        
        public Color[,] GetColors(Texture2D texture)
        {
            Color[] colors = new Color[texture.Height * texture.Width];
            texture.GetData(colors);
            Color[,] returnColors = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    returnColors[x, y] = colors[x + y * texture.Width];
                }
            }
            return returnColors;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MouseSprite = new Sprite(Content.Load<Texture2D>("GUI/MouseCursor"), Vector2.Zero, Color.White, MathHelper.ToRadians(250), 0.5f);
            MouseOffset.Y = (MouseSprite.Texture.Height / -2) + 6;
            MouseOffset.X = -4;

            soundOff = Content.Load<Texture2D>("GUI/SoundOff");
            soundOn = Content.Load<Texture2D>("GUI/SoundOn");

            Rectangle rect = soundOn.Bounds;
            rect.X = 300;
            rect.Y = 300;

            TestButton = new Button(rect, soundOn, Color.Black, 1f, 0.9f, Content.Load<SpriteFont>("Font"), "This is a long sentence");
            SoundOn = true;

            TestLabel = new Label(soundOn, new Vector2(300, 100), Color.Black, 2f, TestButton.Font, "This is a label");

            Dictionary<UnitStates, Animation> dictionary = new Dictionary<UnitStates, Animation>();
            Animation animation = new Animation();
            //animation.AddFrame(Content.Load<Texture2D>(""));

            dictionary.Add(UnitStates.Idle, animation);

            towers = new List<Tower>();

            // Example code
            //var tower = Tower.Create<ArcherTower>(Vector2.One, null, null);
            //tower.Upgrade();
        }

        protected override void Update(GameTime gameTime)
        {
            GameState.Get.CurrentMouse = Mouse.GetState();
            MouseSprite.Position = GameState.Get.CurrentMouse.Position.ToVector2() - MouseOffset;
            if (GameState.Get.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                MouseSprite.Scale = 0.4f;
            }
            else
            {
                MouseSprite.Scale = 0.5f;
            }

            if (TestButton.IsClicked(GameState.Get.CurrentMouse) && !TestButton.IsClicked(GameState.Get.OldMouse))
            {
                SoundOn = !SoundOn;
                if (SoundOn)
                {
                    TestButton.Texture = soundOn;
                }
                else
                {
                    TestButton.Texture = soundOff;
                }
            }

            GameState.Get.OldMouse = GameState.Get.CurrentMouse;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            TestButton.Draw(spriteBatch);
            TestLabel.Draw(spriteBatch);

            MouseSprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}