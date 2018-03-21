using Microsoft.Xna.Framework;
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
        Song song;
        Song popSound;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        List<Balloon> balloons;

        Animation pop;
        
        MouseState currentMouse;
        MouseState lastMouse;

        Texture2D m;

        Random random = new Random();

        float balloonScale = 1f;

        Color[,] map;

        Color[] balloonColors;

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
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            song = Content.Load<Song>("Background");
            popSound = Content.Load<Song>("Pop");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            pop = new Animation();
            pop.AddFrame(Content.Load<Texture2D>("WhiteBalloon"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            pop.AddFrame(Content.Load<Texture2D>("Pop1"));
            balloons = new List<Balloon>();
            m = Content.Load<Texture2D>("Map2");
            map = new Color[m.Width, m.Height];
            map = GetColors(m);
            balloonColors = new Color[11];
            balloonColors[(int)BalloonColors.Red] = new Color(255, 0, 0);
            balloonColors[(int)BalloonColors.Blue] = new Color(0, 0, 255);
            balloonColors[(int)BalloonColors.Green] = new Color(0, 255, 0);
            balloonColors[(int)BalloonColors.Yellow] = new Color(255, 255, 0);
            balloonColors[(int)BalloonColors.Pink] = new Color(100, 0, 0);
            balloonColors[(int)BalloonColors.Black] = new Color(0, 0, 0);
            balloonColors[(int)BalloonColors.White] = new Color(255, 255, 255);
            balloonColors[(int)BalloonColors.Zebra] = new Color(100, 100, 100);
            balloonColors[(int)BalloonColors.Lead] = new Color(50, 50, 50);
            balloonColors[(int)BalloonColors.Rainbow] = new Color(255, 255, 255);
            balloonColors[(int)BalloonColors.Ceramic] = new Color(120, 70, 50);
        }
        protected override void Update(GameTime gameTime)
        {
            currentMouse = Mouse.GetState();

            if (currentMouse.LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed)
            {
                Balloon balloon = new Balloon(map, Color.White, 0f, balloonScale, pop, balloonColors, BalloonColors.Ceramic);
                balloons.Add(balloon);
                balloons[balloons.Count - 1].Place(map);
            }

            if (currentMouse.RightButton == ButtonState.Released && lastMouse.RightButton == ButtonState.Pressed && balloons.Count != 0)
            {
                balloons[0].Pop(popSound);
            }

            if (balloons.Count != 0)
            {
                for(int i = 0; i < balloons.Count; i++)
                {
                    Balloon balloon = balloons[i];
                    if (balloon.popped || balloon.hasFinished)
                    {
                        balloons.Remove(balloon);
                    }
                    balloon.Move(map);
                    balloon.Update();
                }
            }

            lastMouse = currentMouse;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(m, Vector2.Zero, Color.White);
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