using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public static class ScreenManager
    {
        private static ScreenTypes currentScreen;

        private static Dictionary<ScreenTypes, BaseScreen> Screens = new Dictionary<ScreenTypes, BaseScreen>()
        {
            [ScreenTypes.Settings] = new SettingsScreen()
        };
        
        public static void Load(ContentManager Content, ScreenTypes startingScreen)
        {
            foreach (var c in Screens)
            {
                c.Value.Load(Content);
            }
            currentScreen = startingScreen;
        }
        
        public static void Draw(SpriteBatch spriteBatch)
        {
            Screens[currentScreen].Draw(spriteBatch);
        }

        public static void Update(GameTime gameTime)
        {
            ScreenTypes returnValue = Screens[currentScreen].Update(gameTime);
            if(returnValue != ScreenTypes.None)
            {
                currentScreen = returnValue;
            }
        }
    }
}