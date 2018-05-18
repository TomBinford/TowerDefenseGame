using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public abstract class BaseScreen
    {
        public BaseScreen()
        {
        }

        public ScreenTypes PreviousScreen;

        public abstract void Load(ContentManager Content);

        public abstract void GetFocus();

        public abstract void UpdatePositions();

        public abstract ScreenTypes Update(GameTime gameTime);
        
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}