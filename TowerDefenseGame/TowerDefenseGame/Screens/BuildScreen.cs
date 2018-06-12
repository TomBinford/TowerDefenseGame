using Microsoft.Xna.Framework;
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
        //Rotate piece with r and only allow snaps for correct rotations

        Level level;
        Grid Grid;
        bool GridVisible;
        bool DraggingPiece;
        
        Themes currentTheme;

        Sprite ThemeMenu;
        Button[] Trees;

        Sprite[] Pieces;
        Button[] PieceButtons;

        Sprite TurretPoint;
        
        RoadTypes DraggedType;
        Sprite DraggedPiece;

        public BuildScreen()
        {
            GridVisible = true;
            Trees = new Button[3];
            Pieces = new Sprite[5];
            PieceButtons = new Button[Pieces.Length];
            currentTheme = Themes.Cemetery;
            level = new Level();
        }

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (GameState.CurrentKeyboard.IsKeyDown(Keys.G) && GameState.OldKeyboard.IsKeyUp(Keys.G) && GameState.CurrentKeyboard.IsKeyDown(Keys.LeftControl))
            {
                GridVisible = !GridVisible;
            }
            if (GameState.CurrentKeyboard.IsKeyDown(Keys.R) && GameState.OldKeyboard.IsKeyUp(Keys.R))
            {
                DraggedPiece.Angle += MathHelper.ToRadians(45);
                DraggedPiece.Angle %= (float)Math.PI * 2;
            }
            #region Trees
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
            if (new Rectangle(new Point((int)(ThemeMenu.Position.X - ThemeMenu.Texture.Width / 2f), (int)(ThemeMenu.Position.Y - ThemeMenu.Texture.Height)), new Point(ThemeMenu.Texture.Bounds.Size.X, ThemeMenu.Texture.Bounds.Size.Y * 2)).Contains(GameState.CurrentMouse.Position))
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
            #endregion Trees
            for (int i = 0; i < PieceButtons.Length; i++)
            {
                Pieces[i].Scale = new Vector2(Math.Min(40 / (float)LevelTextures.RoadImages[currentTheme][(RoadTypes)i].Width, 40 / (float)LevelTextures.RoadImages[currentTheme][(RoadTypes)i].Height)) * PieceButtons[i].Scale / PieceButtons[i].NormalScale;
                if (PieceButtons[i].IsClicked(GameState.CurrentMouse, GameState.OldMouse))
                {
                    DraggedType = (RoadTypes)i;
                    DraggedPiece.Texture = LevelTextures.RoadImages[currentTheme][DraggedType];
                    DraggingPiece = true;
                }
            }
            if (GameState.CurrentMouse.LeftButton == ButtonState.Pressed && DraggingPiece)
            {
                DraggedPiece.Position = GameState.CurrentMouse.Position.ToVector2();
            }
            else if(DraggingPiece == true)
            {
                level.Decorations.Add(new Sprite(DraggedPiece.Texture, DraggedPiece.Position, DraggedPiece.Tint, DraggedPiece.Angle, DraggedPiece.Scale));
                DraggingPiece = false;
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
            for (int i = 0; i < Pieces.Length; i++)
            {
                PieceButtons[i].Draw(spriteBatch);
                Pieces[i].Draw(spriteBatch);
            }
            DraggedPiece.Draw(spriteBatch);
            level.Draw(spriteBatch, TurretPoint);
        }

        public override void Load(ContentManager Content)
        {
            Grid = new Grid(Content.Load<Texture2D>("GUI/Build/Pixel"), new Vector2(50), new Rectangle(Point.Zero, GameState.Screen.Bounds.Size), Color.Black);
            Texture2D texture = Content.Load<Texture2D>("GUI/Build/EmptyButton");
            for (int i = 0; i < PieceButtons.Length; i++)
            {
                PieceButtons[i] = new Button(new Rectangle(new Point((int)(texture.Width / 4f), (int)(texture.Width / 4f + i * texture.Height / 1.5f)), texture.Bounds.Size), texture, Color.White, 0.5f, 0.48f);
                Pieces[i] = new Sprite(LevelTextures.RoadImages[currentTheme][(RoadTypes)i], PieceButtons[i].Position, Color.White, 0f, new Vector2(Math.Min(40 / (float)LevelTextures.RoadImages[currentTheme][(RoadTypes)i].Width, 40 / (float)LevelTextures.RoadImages[currentTheme][(RoadTypes)i].Height)));
            }
            ThemeMenu = new Sprite(texture, new Vector2(100, GameState.Screen.Height - (texture.Height)), Color.White, 0, new Vector2(1, 2));

            Trees[0] = new Button(new Rectangle(new Point((int)(ThemeMenu.Position.X - (LevelTextures.TreeImages[currentTheme == Themes.Cemetery ? Themes.Village : currentTheme - 1].Width / 4f)), (int)(ThemeMenu.Position.Y - ThemeMenu.Texture.Height / 1.5f)), new Point(50)), null, Color.White, 1f, 1f);
            Trees[1] = new Button(new Rectangle(new Point((int)(ThemeMenu.Position.X - (LevelTextures.TreeImages[currentTheme].Width / (4f * 1.5f))), (int)(ThemeMenu.Position.Y - ThemeMenu.Texture.Height / 1.5f) + 80), new Point(50)), null, Color.White, 1f, 1f);
            Trees[2] = new Button(new Rectangle(new Point((int)(ThemeMenu.Position.X - (LevelTextures.TreeImages[currentTheme == Themes.Village ? Themes.Cemetery : currentTheme + 1].Width / 4f)), (int)(ThemeMenu.Position.Y - ThemeMenu.Texture.Height / 1.5f) + 160), new Point(50)), null, Color.White, 1f, 1f);

            DraggedPiece = new Sprite(null, Vector2.Zero, Color.White, 0f, 50f / LevelTextures.RoadImages[currentTheme][RoadTypes.Straight].Width);

            TurretPoint = new Sprite(Content.Load<Texture2D>("Themes/Cemetery/Dot"), Vector2.Zero, Color.White, 0f, DraggedPiece.Scale);

            UpdateTrees();
        }

        private void UpdateTrees()
        {
            Trees[0].Texture = LevelTextures.TreeImages[currentTheme == Themes.Cemetery ? Themes.Village : currentTheme - 1];
            Trees[0].NormalScale = new Vector2(Math.Max(40 / (float)Trees[0].Texture.Height, 40 / (float)Trees[0].Texture.Width));
            Trees[0].ClickedScale = Trees[0].NormalScale;
            Trees[0].Scale = Trees[0].NormalScale;
            Trees[1].Texture = LevelTextures.TreeImages[currentTheme];
            Trees[1].NormalScale = new Vector2(Math.Min(40 / (float)Trees[1].Texture.Height, 40 / (float)Trees[1].Texture.Width) * 2f);
            Trees[1].ClickedScale = Trees[1].NormalScale;
            Trees[1].Scale = Trees[1].NormalScale;
            Trees[2].Texture = LevelTextures.TreeImages[currentTheme == Themes.Village ? Themes.Cemetery : currentTheme + 1];
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