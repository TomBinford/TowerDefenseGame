using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SpriteLibrary;

namespace TowerDefenseGame
{
    public class Balloon : Sprite
    {
        Animation pop;

        public SoundEffect popSound;

        public bool popped;

        bool popStarted;

        public bool hasFinished;

        Directions lastDirection;

        public bool isCamo;
        public bool isRegen;
        public BalloonColors color;

        public Rectangle hitbox;
        public float radius;

        Color[] colors;

        public Balloon(Color[,] map, float angle, float scale, float radius, Animation animation, SoundEffect popSound, Color[] colors, BalloonColors color, bool camo = false, bool regen = false)
            : base(animation.frames[0], Vector2.Zero, colors[(int)color], angle, scale)
        {
            hitbox = animation.frames[0].Bounds;
            position = Place(map);
            this.color = color;
            pop = animation;
            popped = false;
            popStarted = false;
            hasFinished = false;
            lastDirection = Directions.None;
            isCamo = camo;
            isRegen = regen;
            this.colors = colors;
            tint = colors[(int)color];
            this.popSound = popSound;
        }

        public void Update()
        {
            if (popStarted && !popped)
            {
                Pop();
            }
        }

        public void Pop()
        {
            if (!popStarted)
            {
                popSound.Play(1, 0.7f, 0);
            }
            popStarted = true;
            ChangeTexture(pop.CurrentFrame);
            pop.Advance();
            tint = Color.White;
            if (pop.frame == 0)
            {
                if (color == BalloonColors.Red)
                {
                    popped = true;
                }
                else
                {
                    if (color != BalloonColors.Invincible)
                    {
                        color--;
                    }
                    tint = colors[(int)color];
                    popStarted = false;
                    ChangeTexture(pop.frames[0]);
                }
            }
        }

        public Vector2 Place(Color[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == new Color(0, 255, 0))
                    {
                        position = new Vector2(x, y);
                        return position;
                    }
                }
            }
            return Vector2.Zero;
        }

        private bool ColorSurrounds(Color[,] map, Color search, bool move = false)
        {
            if (position.X + 1 < map.GetLength(0))
            {
                if (map[(int)position.X + 1, (int)position.Y] == search && lastDirection != Directions.Left)
                {
                    if (move)
                    {
                        lastDirection = Directions.Right;
                        position.X++;
                    }
                    return true;
                }
            }
            if (position.X > 0)
            {
                if (map[(int)position.X - 1, (int)position.Y] == search && lastDirection != Directions.Right)
                {
                    if (move)
                    {
                        lastDirection = Directions.Left;
                        position.X--;
                    }
                    return true;
                }
            }
            if (position.Y + 1 < map.GetLength(1))
            {
                if (map[(int)position.X, (int)position.Y - 1] == search && lastDirection != Directions.Up)
                {
                    if (move)
                    {
                        lastDirection = Directions.Down;
                        position.Y--;
                    }
                    return true;
                }
            }
            if (position.Y > 0)
            {
                if (map[(int)position.X, (int)position.Y + 1] == search && lastDirection != Directions.Down)
                {
                    if (move)
                    {
                        lastDirection = Directions.Up;
                        position.Y++;
                    }
                    return true;
                }
            }
            return false;
        }

        public void Move(Color[,] map)
        {
            if (hasFinished)
            {
                return;
            }
            Color currentColor = map[(int)position.X, (int)position.Y];
            Color black = new Color(0, 0, 0);
            Color start = new Color(0, 255, 0);
            Color end = new Color(255, 0, 0);
            ColorSurrounds(map, new Color(0, 0, 0), true);
            if (ColorSurrounds(map, new Color(255, 0, 0), false))
            {
                hasFinished = true;
            }
        }
    }
}