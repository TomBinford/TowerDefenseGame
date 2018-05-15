using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    static class ScreenManager
    {
        private static List<BaseScreen> screens;
        private static BaseScreen activeScreen = new MainMenuScreen();

        public static void Load(ContentManager Content)
        {
            foreach(BaseScreen screen in screens)
            {
                screen.Load(Content);
            }
        }

        public static BaseScreen ActiveScreen
        {
            get
            {
                return activeScreen;
            }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Active screen may not be set to null");
                }
                activeScreen = value;
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (activeScreen != null)
            {
                activeScreen.Update(gameTime);
            }
            else
            {
                throw new NullReferenceException("Active screen not set");
            }
        }
    }
}