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
        
        Texture2D m;

        List<Tower> towers;
        
        Random random = new Random();

        float balloonScale = 1f;

        Color[,] map;
        
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

            towers = new List<Tower>();
            
            // Example code
            var tower = Tower.Create<ArcherTower>(Vector2.One, null, null);
            tower.Upgrade();
        }
        protected override void Update(GameTime gameTime)
        {
            GameState.Get.CurrentMouse = Mouse.GetState();

            GameState.Get.OldMouse = GameState.Get.CurrentMouse;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}