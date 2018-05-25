using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using TowerDefenseGame.Screens;
using WinForms = System.Windows.Forms;

namespace TowerDefenseGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite MouseSprite;
        Vector2 MouseOffset;

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

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            IsMouseVisible = false;
            graphics.IsFullScreen = false;
        }

        WinForms.Form owningForm;
        protected override void Initialize()
        {
            GameState.ScreenViewport = GraphicsDevice.Viewport;
            base.Initialize();

            owningForm = WinForms.Control.FromHandle(Window.Handle) as WinForms.Form;
            owningForm.FormBorderStyle = WinForms.FormBorderStyle.Sizable;
            owningForm.Resize += OwningForm_Resize;
            owningForm.MaximizeBox = true;
        }

        private void OwningForm_Resize(object sender, EventArgs e)
        {
            graphics.PreferredBackBufferWidth = owningForm.WindowState == WinForms.FormWindowState.Maximized ? 1920 : owningForm.ClientRectangle.Width;
            graphics.PreferredBackBufferHeight = owningForm.WindowState == WinForms.FormWindowState.Maximized ? 1080 : owningForm.ClientRectangle.Height;
            graphics.ApplyChanges();

            GameState.ScreenViewport = GraphicsDevice.Viewport;
            ScreenManager.UpdatePositions();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MouseSprite = new Sprite(Content.Load<Texture2D>("GUI/MouseCursor"), Vector2.Zero, Color.White, MathHelper.ToRadians(250), 0.5f);
            MouseOffset.Y = (MouseSprite.Texture.Height / -2) + 6;
            MouseOffset.X = -4;

            ScreenManager.Load(Content);
            NumberDrawer.Load(Content);

            towers = new List<Tower>();

            // Example code
            //var tower = Tower.Create<ArcherTower>(Vector2.One, null, null);
            //tower.Upgrade();
        }
        
        protected override void Update(GameTime gameTime)
        {
            Console.WriteLine(54321.Length());
            GameState.CurrentMouse = Mouse.GetState();
            MouseSprite.Position = GameState.CurrentMouse.Position.ToVector2() - MouseOffset;

            if (GameState.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                MouseSprite.Scale = 0.4f;
            }
            else
            {
                MouseSprite.Scale = 0.5f;
            }

            ScreenManager.Update(gameTime);

            GameState.OldMouse = GameState.CurrentMouse;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            ScreenManager.Draw(spriteBatch);
            MouseSprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}