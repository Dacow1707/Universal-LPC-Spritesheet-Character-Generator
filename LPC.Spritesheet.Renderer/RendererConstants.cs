using LPC.Spritesheet.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LPC.Spritesheet.Generator
{
    public static class RendererConstants
    {
        public static Dictionary<string, (int row, int frames)> SpriteSheetAnimationDefinition = new Dictionary<string, (int row, int frames)>()
        {
            { "Spellcast_Back", (0,7) },
            { "Spellcast_Left", (1,7) },
            { "Spellcast_Front", (2,7) },
            { "Spellcast_Right", (3,7) },

            { "Thrust_Back", (4,8) },
            { "Thrust_Left", (5,8) },
            { "Thrust_Front", (6,8) },
            { "Thrust_Right", (7,8) },

            { "Walk_Back", (8,9) },
            { "Walk_Left", (9,9) },
            { "Walk_Front", (10,9) },
            { "Walk_Right", (11,9) },

            { "Slash_Back", (12,6) },
            { "Slash_Left", (13,6) },
            { "Slash_Front", (14,6) },
            { "Slash_Right", (15,6) },

            { "Shoot_Back", (16,13) },
            { "Shoot_Left", (17,13) },
            { "Shoot_Front", (18,1) },
            { "Shoot_Right", (19,13) },

            { "Die_Back", (20,6) },
            { "Die_left", (20,6) },
            { "Die_Front", (20,6) },
            { "Die_Right", (20,6) },
        };

        public static int SheetHeight { get; set; } = 1344;

        public static int SheetWidth { get; set; } = 832;

        public static int SpriteHeight { get; set; } = 64;

        public static int SpriteWidth { get; set; } = 64;

        public static List<ISpriteSheet> GetOrderedLayers(List<ISpriteSheet> layers)
        {
            return layers.OrderBy(l => (int)l.SpriteLayer).ToList();
        }
    }
}