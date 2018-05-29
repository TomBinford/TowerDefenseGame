using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame.Screens
{
    public static class NumberDrawer
    {
        private static Texture2D[] Numbers;

        public static void Load(ContentManager Content)
        {
            Numbers = new Texture2D[10];
            for (int i = 0; i < 10; i++)
            {
                Numbers[i] = Content.Load<Texture2D>($"GUI/LevelSelect/Numbers/{i}");
            }
        }

        public static void Draw(SpriteBatch spriteBatch, int number, Vector2 position)
        {
            int totalCount = number.Length();
            int totalWidth = 0;
            float averageHeight = 0;
            for (int i = 0; i < totalCount; i++)
            {
                totalWidth += (int)(Numbers[i].Width * 1.1f);
                averageHeight += Numbers[i].Height;
            }
            averageHeight /= totalCount;
            position.X -= totalWidth / 2f;
            position.Y -= averageHeight / 2f;
            for (int i = 0; i < totalCount; i++)
            {
                spriteBatch.Draw(Numbers[number.DigitAt(i)], position, Color.White);
                position.X += Numbers[number.DigitAt(i)].Width * 1.1f;
            }
        }
    }
}