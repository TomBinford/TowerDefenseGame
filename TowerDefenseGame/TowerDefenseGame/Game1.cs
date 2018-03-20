using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteLibrary;
using System;
using System.Collections.Generic;

namespace TowerDefenseGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        List<Balloon> balloons;

        Animation pop;
        
        MouseState currentMouse;
        MouseState lastMouse;

        Random random = new Random();

        float balloonScale = 1f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pop = new Animation();
            pop.AddFrame(Content.Load<Texture2D>("WhiteBalloon"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            balloons = new List<Balloon>();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentMouse = Mouse.GetState();

            if (currentMouse.LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed)
            {
                Balloon balloon = new Balloon(new Vector2(random.Next(0, GraphicsDevice.Viewport.Width), random.Next(0, GraphicsDevice.Viewport.Height)), Color.White, 0f, balloonScale, pop);
                balloons.Add(balloon);
            }

            if (currentMouse.RightButton == ButtonState.Released && lastMouse.RightButton == ButtonState.Pressed && balloons.Count != 0)
            {
                balloons[balloons.Count - 1].Pop();
            }

            if (balloons.Count != 0)
            {
                for(int i = 0; i < balloons.Count; i++)
                {
                    Balloon balloon = balloons[i];
                    if (balloon.popped)
                    {
                        balloons.Remove(balloon);
                    }
                    balloon.Update();
                }
            }

            lastMouse = currentMouse;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            
            if (balloons.Count != 0)
            {
                foreach (Balloon balloon in balloons)
                {
                    balloon.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}