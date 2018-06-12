using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseGame
{
    public static class LevelTextures
    {
        public static Dictionary<Themes, Dictionary<RoadTypes, Texture2D>> RoadImages;
        public static Dictionary<Themes, Texture2D> TreeImages;

        public static void Load(ContentManager Content)
        {
            RoadImages = new Dictionary<Themes, Dictionary<RoadTypes, Texture2D>>();
            TreeImages = new Dictionary<Themes, Texture2D>();
            for (Themes theme = Themes.Cemetery; theme <= Themes.Village; theme++)
            {
                RoadImages.Add(theme, new Dictionary<RoadTypes, Texture2D>());
                for (RoadTypes type = RoadTypes.Straight; type <= RoadTypes.Zig; type++)
                {
                    RoadImages[theme].Add(type, Content.Load<Texture2D>($"Themes/Cemetery/Road/{type.ToString()}"));
                }
                TreeImages.Add(theme, Content.Load<Texture2D>($"Themes/{theme.ToString()}/Tree"));
            }
        }
    }
}