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
        public Animation PopAnimation;

        private SoundEffect popSound;

        public bool Popped;

        public bool PopStarted;

        public bool HasFinished;

        private Directions lastDirection;

        public int MovesMade;

        public bool IsCamo;
        public bool IsRegen;
        public BalloonColors BalloonColor;
        public float Radius;

        Dictionary<BalloonColors, Color> colors;

        public Balloon(Color[,] map, float angle, float scale, float Radius, Animation animation, SoundEffect popSound, Dictionary<BalloonColors, Color> colors, BalloonColors BalloonColor, bool camo = false, bool regen = false)
            : base(animation.Frames[0], Vector2.Zero, colors[BalloonColor], angle, scale)
        {
            Position = Place(map);
            this.BalloonColor = BalloonColor;
            PopAnimation = animation;
            Popped = false;
            PopStarted = false;
            HasFinished = false;
            lastDirection = Directions.None;
            IsCamo = camo;
            IsRegen = regen;
            this.colors = colors;
            Tint = colors[BalloonColor];
            this.popSound = popSound;
            MovesMade = 0;
        }

        public void Update()
        {
            if (!Popped)
            {
                if (PopStarted)
                {
                    Pop();
                }
            }
            else
            {
                Texture = PopAnimation.Frames[0];
            }
        }

        public void Pop()
        {
            if (!PopStarted)
            {
                popSound.Play(1, 0.7f, 0);
            }
            PopStarted = true;
            Texture = PopAnimation.CurrentFrame;
            PopAnimation.Advance();
            Tint = Color.White;
            if (PopAnimation.Frame == 0)
            {
                if (BalloonColor == BalloonColors.Red)
                {
                    Popped = true;
                }
                else
                {
                    if (BalloonColor != BalloonColors.Invincible)
                    {
                        BalloonColor--;
                    }
                    Tint = colors[BalloonColor];
                    PopStarted = false;
                    Texture = PopAnimation.Frames[0];
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
                        Position = new Vector2(x, y);
                        return Position;
                    }
                }
            }
            return Vector2.Zero;
        }

        private bool ColorSurrounds(Color[,] map, Color search, bool move = false)
        {
            if (Position.X + 1 < map.GetLength(0))
            {
                if (map[(int)Position.X + 1, (int)Position.Y] == search && lastDirection != Directions.Left)
                {
                    if (move)
                    {
                        lastDirection = Directions.Right;
                        Position.X++;
                    }
                    return true;
                }
            }
            if (Position.X > 0)
            {
                if (map[(int)Position.X - 1, (int)Position.Y] == search && lastDirection != Directions.Right)
                {
                    if (move)
                    {
                        lastDirection = Directions.Left;
                        Position.X--;
                    }
                    return true;
                }
            }
            if (Position.Y + 1 < map.GetLength(1))
            {
                if (map[(int)Position.X, (int)Position.Y - 1] == search && lastDirection != Directions.Up)
                {
                    if (move)
                    {
                        lastDirection = Directions.Down;
                        Position.Y--;
                    }
                    return true;
                }
            }
            if (Position.Y > 0)
            {
                if (map[(int)Position.X, (int)Position.Y + 1] == search && lastDirection != Directions.Down)
                {
                    if (move)
                    {
                        lastDirection = Directions.Up;
                        Position.Y++;
                    }
                    return true;
                }
            }
            return false;
        }

        public void Move(Color[,] map)
        {
            if (HasFinished)
            {
                return;
            }
            Color currentColor = map[(int)Position.X, (int)Position.Y];
            ColorSurrounds(map, new Color(0, 0, 0), true);
            MovesMade++;
            if (ColorSurrounds(map, new Color(255, 0, 0), false))
            {
                HasFinished = true;
            }
        }
    }
}