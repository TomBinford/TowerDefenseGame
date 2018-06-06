﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public class BuildScreen : BaseScreen
    {
        //Road on left
        //Decorations on right
        //Scroll theme(snow, desert, etc.) on bottom left with trees from each theme signifying which one is selected DONE

        Level level;
        Grid Grid;
        bool GridVisible;
        
        Themes currentTheme;
        Dictionary<Themes, Texture2D> TreeImages;

        Sprite ThemeMenu;
        Button[] Trees;

        Dictionary<Themes, Dictionary<RoadTypes, Texture2D>> RoadPieces;
        Sprite[] Pieces;

        public BuildScreen()
        {
            GridVisible = true;
            Trees = new Button[3];
            Pieces = new Sprite[5];
            RoadPieces = new Dictionary<Themes, Dictionary<RoadTypes, Texture2D>>();
            currentTheme = Themes.Cemetery;
            level = new Level();
        }

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (GameState.CurrentKeyboard.IsKeyDown(Keys.G) && GameState.OldKeyboard.IsKeyUp(Keys.G) && GameState.CurrentKeyboard.IsKeyDown(Keys.LeftControl))
            {
                GridVisible = !GridVisible;
            }
            if (Trees[0].IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                currentTheme = currentTheme == Themes.Cemetery ? Themes.Village : currentTheme - 1;
                UpdateTrees();
            }
            if (Trees[2].IsClicked(GameState.CurrentMouse, GameState.OldMouse))
            {
                currentTheme = currentTheme == Themes.Village ? Themes.Cemetery : currentTheme + 1;
                UpdateTrees();
            }
            int mouseDifference = GameState.CurrentMouse.ScrollWheelValue - GameState.OldMouse.ScrollWheelValue;
            if (Math.Abs(GameState.CurrentMouse.X - ThemeMenu.Position.X) <= ThemeMenu.Texture.Width / 2f && Math.Abs(GameState.CurrentMouse.Y - ThemeMenu.Position.Y) <= ThemeMenu.Texture.Height / 2f)
            {
                if (mouseDifference > 0)
                {
                    currentTheme = currentTheme == Themes.Cemetery ? Themes.Village : currentTheme - 1;
                    UpdateTrees();
                }
                if (mouseDifference < 0)
                {
                    currentTheme = currentTheme == Themes.Village ? Themes.Cemetery : currentTheme + 1;
                    UpdateTrees();
                }
            }
            return ScreenTypes.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (GridVisible)
            {
                Grid.Draw(spriteBatch);
            }
            ThemeMenu.Draw(spriteBatch);
            Trees[0].Draw(spriteBatch);
            for (int i = 0; i < Trees.Length; i++)
            {
                Trees[i].Draw(spriteBatch);
            }
            foreach (Sprite s in Pieces)
            {
                s.Draw(spriteBatch);
            }
        }

        public override void Load(ContentManager Content)
        {
            Grid = new Grid(Content.Load<Texture2D>("GUI/Build/Pixel"), new Vector2(50), new Rectangle(Point.Zero, GameState.Screen.Bounds.Size), Color.Black);
            TreeImages = new Dictionary<Themes, Texture2D>();
            for (Themes theme = Themes.Cemetery; theme <= Themes.Village; theme++)
            {
                RoadPieces.Add(theme, new Dictionary<RoadTypes, Texture2D>());
                for (RoadTypes type = RoadTypes.Straight; type <= RoadTypes.Zig; type++)
                {
                    RoadPieces[theme].Add(type, Content.Load<Texture2D>($"Themes/Cemetery/Road/{type.ToString()}"));
                }
                TreeImages.Add(theme, Content.Load<Texture2D>($"Themes/{theme.ToString()}/Tree"));
            }
            for (int i = 0; i < Pieces.Length; i++)
            {
                Pieces[i] = new Sprite(RoadPieces[currentTheme][(RoadTypes)i], Vector2.Zero, Color.White);
            }
            Texture2D texture = Content.Load<Texture2D>("GUI/Build/EmptyButton");
            ThemeMenu = new Sprite(texture, new Vector2(100, GameState.Screen.Height - (texture.Height)), Color.White, 0, new Vector2(1, 2));

            Trees[0] = new Button(new Rectangle(new Point((int)(ThemeMenu.Position.X - (TreeImages[currentTheme == Themes.Cemetery ? Themes.Village : currentTheme - 1].Width / 4f)), (int)(ThemeMenu.Position.Y - ThemeMenu.Texture.Height / 1.5f)), new Point(50)), null, Color.White, 1f, 1f);
            Trees[1] = new Button(new Rectangle(new Point((int)(ThemeMenu.Position.X - (TreeImages[currentTheme].Width / (4f * 1.5f))), (int)(ThemeMenu.Position.Y - ThemeMenu.Texture.Height / 1.5f) + 80), new Point(50)), null, Color.White, 1f, 1f);
            Trees[2] = new Button(new Rectangle(new Point((int)(ThemeMenu.Position.X - (TreeImages[currentTheme == Themes.Village ? Themes.Cemetery : currentTheme + 1].Width / 4f)), (int)(ThemeMenu.Position.Y - ThemeMenu.Texture.Height / 1.5f) + 160), new Point(50)), null, Color.White, 1f, 1f);

            UpdateTrees();
        }

        private void UpdateTrees()
        {
            Trees[0].Texture = TreeImages[currentTheme == Themes.Cemetery ? Themes.Village : currentTheme - 1];
            Trees[0].NormalScale = new Vector2(Math.Max(40 / (float)Trees[0].Texture.Height, 40 / (float)Trees[0].Texture.Width));
            Trees[0].ClickedScale = Trees[0].NormalScale;
            Trees[0].Scale = Trees[0].NormalScale;
            Trees[1].Texture = TreeImages[currentTheme];
            Trees[1].NormalScale = new Vector2(Math.Min(40 / (float)Trees[1].Texture.Height, 40 / (float)Trees[1].Texture.Width) * 2f);
            Trees[1].ClickedScale = Trees[1].NormalScale;
            Trees[1].Scale = Trees[1].NormalScale;
            Trees[2].Texture = TreeImages[currentTheme == Themes.Village ? Themes.Cemetery : currentTheme + 1];
            Trees[2].NormalScale = new Vector2(Math.Max(40 / (float)Trees[2].Texture.Height, 40 / (float)Trees[2].Texture.Width));
            Trees[2].ClickedScale = Trees[2].NormalScale;
            Trees[2].Scale = Trees[2].NormalScale;
        }

        public override void UpdatePositions()
        {
            Grid.Bounds = GameState.Screen.Bounds;
        }

        public override void GetFocus()
        {

        }
    }
}