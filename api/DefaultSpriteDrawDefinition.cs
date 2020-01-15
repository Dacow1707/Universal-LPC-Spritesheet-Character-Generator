using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public class DefaultSpriteDrawDefinition : ISpriteDrawDefinition
    {
        public int SheetWidth { get; set; } = 832;
        public int SheetHeight { get; set; } = 1344;
        public int SpriteWidth { get; set; } = 64;
        public int SpriteHeight { get; set; } = 64;

        public List<string> LayerOrder = new List<string>()
        {
            "behind_body",
            "body",
            "head",
            "arms",
            "legs",

            "facial",
            "torso",
            "belt",
            "feet",
            "hands",
            "oversize_xcf",
            "shoulders",
            "accessories",
            "hair",
            "weapons",

            "palettes",
        };

        public Dictionary<string, (int row, int frames)> SpriteSheetAnimationDefinition = new Dictionary<string, (int row, int frames)>()
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

        public Image GetSingleSprite(ICharacterSprite sprite, Animation animation, Orientation orientation, int frame)
        {
            var (row, frames) = SpriteSheetAnimationDefinition[$"{animation}_{orientation}"];

            if (frame >= frames)
            {
                throw new IndexOutOfRangeException($"Out of range, Cannot get more than frame count ({frames - 1})");
            }
            return GetSpriteSheet(sprite, new Rectangle(frame * SpriteWidth, row * SpriteWidth, SpriteWidth, SpriteHeight));
        }

        public Image GetPartialSpriteSheet(ICharacterSprite sprite, Animation animation, Orientation orientation)
        {
            var (row, frames) = SpriteSheetAnimationDefinition[$"{animation}_{orientation}"];
            return GetSpriteSheet(sprite, new Rectangle(0, row * SpriteWidth, SheetWidth, SpriteHeight));
        }

        public Image GetFullSpriteSheet(ICharacterSprite sprite)
        {
            return GetSpriteSheet(sprite, new Rectangle(0, 0, SheetWidth, SheetHeight));
        }

        public Image GetSpriteSheet(ICharacterSprite sprite, Rectangle rectangle)
        {
            var srcRectange = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            Image newImage = new Bitmap(srcRectange.Width, srcRectange.Height);

            try
            {
                using (var g = Graphics.FromImage(newImage))
                {
                    g.Clear(Color.Transparent);

                    foreach (var layer in GetOrderedLayers(sprite.Layers))
                    {
                        using (var image = layer.Image)
                        {
                            g.DrawImage(image, srcRectange, rectangle, GraphicsUnit.Pixel);
                        }
                    }

                    return newImage;
                }
            }
            catch (Exception ex)
            {
                if (newImage != null)
                {
                    newImage.Dispose();
                }
                throw ex;
            }
        }

        public int GetOrder(string path)
        {
            return LayerOrder.IndexOf(path.Split(new[] { '/', '\\' })[0].ToLower());
        }

        public List<ISpriteSheet> GetOrderedLayers(List<ISpriteSheet> layers)
        {
            return layers.OrderBy(l => GetOrder(l.Category)).ToList();
        }
    }
}