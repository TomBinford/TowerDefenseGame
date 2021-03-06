﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpriteLibrary;
using System;
using System.Collections.Generic;
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
            GameState.Screen = GraphicsDevice.Viewport;
            base.Initialize();

            owningForm = WinForms.Control.FromHandle(Window.Handle) as WinForms.Form;
            owningForm.FormBorderStyle = WinForms.FormBorderStyle.Sizable;
            owningForm.Resize += OwningForm_Resize;
            owningForm.MaximizeBox = true;
        }

        private void OwningForm_Resize(object sender, EventArgs e)
        {
            graphics.PreferredBackBufferWidth = owningForm.ClientRectangle.Width;
            graphics.PreferredBackBufferHeight = owningForm.ClientRectangle.Height;
            graphics.ApplyChanges();
            
            GameState.Screen = GraphicsDevice.Viewport;
            ScreenManager.UpdatePositions();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            MouseSprite = new Sprite(Content.Load<Texture2D>("GUI/MouseCursor"), Vector2.Zero, Color.White, MathHelper.ToRadians(250), 0.5f);
            MouseOffset.Y = (MouseSprite.Texture.Height / -2) + 6;
            MouseOffset.X = -4;

            GameState.Load();
            LevelTextures.Load(Content);
            ScreenManager.Load(Content);
            NumberDrawer.Load(Content);

            towers = new List<Tower>();

            // Example code
            //var tower = Tower.Create<ArcherTower>(Vector2.One, null, null);
            //tower.Upgrade();
        }
        
        protected override void Update(GameTime gameTime)
        {
            GameState.CurrentMouse = Mouse.GetState();
            GameState.CurrentKeyboard = Keyboard.GetState();
            MouseSprite.Position = GameState.CurrentMouse.Position.ToVector2() - MouseOffset;

            if (GameState.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                MouseSprite.Scale = new Vector2(0.4f);
            }
            else
            {
                MouseSprite.Scale = new Vector2(0.5f);
            }

            ScreenManager.Update(gameTime);

            GameState.OldMouse = GameState.CurrentMouse;
            GameState.OldKeyboard = GameState.CurrentKeyboard;
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