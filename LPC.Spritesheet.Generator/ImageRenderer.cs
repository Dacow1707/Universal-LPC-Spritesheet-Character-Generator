using LPC.Spritesheet.Generator.Enums;
using LPC.Spritesheet.Generator.Interfaces;
using System;
using System.Drawing;
using System.IO;
using Animation = LPC.Spritesheet.Generator.Interfaces.Animation;
using Color = System.Drawing.Color;
using Graphics = System.Drawing.Graphics;

namespace LPC.Spritesheet.Generator
{
    public static class ImageRenderer
    {
        public static Image GetFullSpriteSheet(ICharacterSpriteDefinition sprite)
        {
            return GetSpriteSheet(sprite, new Rectangle(0, 0, Settings.SheetWidth, Settings.SheetHeight));
        }

        public static Image GetPartialSpriteSheet(ICharacterSpriteDefinition sprite, Animation animation, Orientation orientation)
        {
            var (row, _) = Settings.SpriteSheetAnimationDefinition[(animation, orientation)];
            return GetSpriteSheet(sprite, new Rectangle(0, row * Settings.SpriteWidth, Settings.SheetWidth, Settings.SpriteHeight));
        }

        public static Image GetSingleSprite(ICharacterSpriteDefinition sprite, Animation animation, Orientation orientation, int frame)
        {
            var (row, frames) = Settings.SpriteSheetAnimationDefinition[(animation, orientation)];

            if (frame >= frames)
            {
                throw new IndexOutOfRangeException($"Out of range, Cannot get more than frame count ({frames - 1})");
            }
            return GetSpriteSheet(sprite, new Rectangle(frame * Settings.SpriteWidth, row * Settings.SpriteWidth, Settings.SpriteWidth, Settings.SpriteHeight));
        }

        public static Image GetSpriteSheet(ICharacterSpriteDefinition sprite, Rectangle rectangle)
        {
            var srcRectange = new Rectangle(0, 0, rectangle.Width, rectangle.Height);
            Image newImage = new Bitmap(srcRectange.Width, srcRectange.Height);

            try
            {
                using (var g = Graphics.FromImage(newImage))
                {
                    g.Clear(Color.Transparent);

                    foreach (var layer in Settings.GetOrderedLayers(sprite.Layers))
                    {
                        using (var image = Image.FromStream(new MemoryStream(layer.SpriteData)))
                        {
                            if (image != null)
                            {
                                g.DrawImage(image, srcRectange, rectangle, GraphicsUnit.Pixel);
                            }
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
    }
}