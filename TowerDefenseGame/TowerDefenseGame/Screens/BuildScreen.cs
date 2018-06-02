using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public BuildScreen()
        {
            GridVisible = true;
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
            if (true)
            {
                Grid.Draw(spriteBatch);
            }
        }

        public override void Load(ContentManager Content)
        {
            Grid = new Grid(Content.Load<Texture2D>("GUI/Build/Pixel"), new Vector2(50), new Rectangle(Point.Zero, GameState.Screen.Bounds.Size), Color.Black);
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
