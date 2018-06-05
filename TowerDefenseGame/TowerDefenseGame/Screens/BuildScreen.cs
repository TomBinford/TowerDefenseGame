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
        //Scroll theme(snow, desert, etc.) on bottom left with trees from each theme signifying which one is selected

        Level level;
        Grid Grid;
        bool GridVisible;

        Themes currentTheme;
        Dictionary<Themes, Texture2D> TreeImages;

        Sprite ThemeMenu;
        Sprite[] Trees;

        public BuildScreen()
        {
            GridVisible = true;
            Trees = new Sprite[3];
        }

        public override ScreenTypes Update(GameTime gameTime)
        {
            if (GameState.CurrentKeyboard.IsKeyDown(Keys.G) && GameState.OldKeyboard.IsKeyUp(Keys.G) && GameState.CurrentKeyboard.IsKeyDown(Keys.LeftControl))
            {
                GridVisible = !GridVisible;
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
        }

        public override void Load(ContentManager Content)
        {
            Grid = new Grid(Content.Load<Texture2D>("GUI/Build/Pixel"), new Vector2(50), new Rectangle(Point.Zero, GameState.Screen.Bounds.Size), Color.Black);
            TreeImages = new Dictionary<Themes, Texture2D>();
            for (Themes theme = Themes.Cemetery; theme <= Themes.Village; theme++)
            {
                TreeImages.Add(theme, Content.Load<Texture2D>($"Themes/{theme.ToString()}/Tree"));
            }
            Texture2D texture = Content.Load<Texture2D>("GUI/Build/EmptyButton");
            ThemeMenu = new Sprite(texture, new Vector2(100, GameState.Screen.Height - (texture.Height)), Color.White, 0, new Vector2(1, 2));
            
            for (int i = 0; i < 3; i++)
            {
                Trees[i] = new Sprite(null, new Vector2(ThemeMenu.Position.X, (ThemeMenu.Position.Y - (ThemeMenu.Texture.Height / 2f)) + 100 * i), Color.White);
            }
            UpdateTrees();
        }

        private void UpdateTrees()
        {
            Trees[0].Texture = TreeImages[currentTheme == Themes.Cemetery ? Themes.Village :  currentTheme - 1];
            Trees[0].Scale = new Vector2(Math.Max(50 / (float)Trees[0].Texture.Height, 50 / (float)Trees[0].Texture.Width));
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