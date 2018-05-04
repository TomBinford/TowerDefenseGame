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
        Song song;
        SoundEffect popSound;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Balloon> balloons;

        Animation pop;

        Animation towerIdle;
        Animation towerShoot;

        MouseState currentMouse;
        MouseState lastMouse;

        Texture2D m;

        List<Tower> towers;
        
        Random random = new Random();

        float balloonScale = 1f;

        Color[,] map;

        Dictionary<BalloonColors, Color> balloonColors;

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
            popSound = Content.Load<SoundEffect>("Pop");
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

            towerIdle = new Animation();
            towerIdle.AddFrame(Content.Load<Texture2D>("TestTower/IdleTower"));

            towerShoot = new Animation();

            Texture2D first = Content.Load<Texture2D>("TestTower/AttackTower1");
            Texture2D second = Content.Load<Texture2D>("TestTower/AttackTower2");

            towerShoot.AddFrame(first);
            towerShoot.AddFrame(first);
            towerShoot.AddFrame(first);
            towerShoot.AddFrame(second);
            towerShoot.AddFrame(second);
            towerShoot.AddFrame(second);

            towers = new List<Tower>();
            // towers.Add(new Tower(towerIdle, towerShoot, new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, GraphicsDevice.Viewport.Height / 2), Color.White, 1f, Content.Load<Texture2D>("Dart"), 100, 500, true, TargetTypes.First));

            balloonColors = new Dictionary<BalloonColors, Color>();
            balloonColors.Add(BalloonColors.Red, new Color(255, 0, 0));
            balloonColors.Add(BalloonColors.Blue, new Color(0, 0, 255));
            balloonColors.Add(BalloonColors.Green, new Color(0, 255, 0));
            balloonColors.Add(BalloonColors.Yellow, new Color(255, 255, 0));
            balloonColors.Add(BalloonColors.Pink, new Color(100, 0, 0));
            balloonColors.Add(BalloonColors.Black, new Color(0, 0, 0));
            balloonColors.Add(BalloonColors.White, new Color(255, 255, 255));
            balloonColors.Add(BalloonColors.Zebra, new Color(100, 100, 100));
            balloonColors.Add(BalloonColors.Lead, new Color(50, 50, 50));
            balloonColors.Add(BalloonColors.Rainbow, new Color(255, 255, 255));
            balloonColors.Add(BalloonColors.Ceramic, new Color(120, 70, 50));
            balloonColors.Add(BalloonColors.Invincible, new Color(0, 0, 0));


            // Example code
            var tower = Tower.Create<LaserTower>(Vector2.One, null, null);
            tower.Upgrade();
        }
        protected override void Update(GameTime gameTime)
        {
            currentMouse = Mouse.GetState();

            if (currentMouse.LeftButton == ButtonState.Released && lastMouse.LeftButton == ButtonState.Pressed)
            {
                Balloon balloon = new Balloon(map, 0f, balloonScale, 5, pop, popSound, balloonColors, BalloonColors.Ceramic);
                balloons.Add(balloon);
                balloons[balloons.Count - 1].Place(map);
            }

            if (balloons.Count != 0)
            {
                List<Balloon> rem = new List<Balloon>();
                foreach (Balloon balloon in balloons)
                {
                    if (balloon.Popped || balloon.HasFinished)
                    {
                        rem.Add(balloon);
                    }
                    balloon.Move(map);
                    balloon.Update();
                }
                if (rem.Count != 0)
                {
                    foreach (Balloon balloon in rem)
                    {
                        balloons.Remove(balloon);
                    }
                }
                Balloon bestCandidate = null;

                foreach (Tower tower in towers)
                {
                    bestCandidate = tower.BestShot(balloons);
                    if (bestCandidate != null)
                    {
                        tower.Shoot(tower.BestShot(balloons));
                    }
                    else
                    {
                        tower.State = AnimationStates.Idle;
                    }
                    tower.Update();
                    tower.UpdateProjectiles();
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
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
            foreach (Tower tower in towers)
            {
                tower.DrawProjectiles(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}