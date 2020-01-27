using LPC.Spritesheet.Generator.Enums;
using LPC.Spritesheet.Generator.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;

namespace LPC.Spritesheet.Generator
{
    public static class HybridRenderer
    {
        public static Texture2D GetFullSheetTexture(ICharacterSpriteDefinition sprite)
        {
            return GetSpriteSheet(sprite, new RectInt(0, 0, Settings.SheetWidth, Settings.SheetHeight));
        }

        public static Texture2D GetPartialSpriteSheet(ICharacterSpriteDefinition sprite, Interfaces.Animation animation, Orientation orientation)
        {
            var (row, _) = Settings.SpriteSheetAnimationDefinition[(animation, orientation)];
            return GetSpriteSheet(sprite, new RectInt(0, row * Settings.SpriteWidth, Settings.SheetWidth, Settings.SpriteHeight));
        }

        public static Texture2D GetSingleSprite(ICharacterSpriteDefinition sprite, Interfaces.Animation animation, Orientation orientation, int frame)
        {
            var (row, frames) = Settings.SpriteSheetAnimationDefinition[(animation, orientation)];

            if (frame >= frames)
            {
                throw new IndexOutOfRangeException($"Out of range, Cannot get more than frame count ({frames - 1})");
            }
            return GetSpriteSheet(sprite, new RectInt(frame * Settings.SpriteWidth, row * Settings.SpriteWidth, Settings.SpriteWidth, Settings.SpriteHeight));
        }

        public static Texture2D GetSpriteSheet(ICharacterSpriteDefinition sprite, RectInt rectangle)
        {
            var srcRectange = new RectInt(0, 0, rectangle.width, rectangle.height);
            var newImage = new Texture2D(srcRectange.width, srcRectange.height, TextureFormat.RGBA32, true)
            {
                alphaIsTransparency = true
            };

            var renderedImage = ImageRenderer.GetSpriteSheet(sprite, new Rectangle(rectangle.x, rectangle.y, rectangle.width, rectangle.height)).ImageToByteArray();
            newImage.LoadImage(renderedImage);

            return newImage;
        }

        public static byte[] ImageToByteArray(this Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static Texture2D GetTexture(byte[] spriteData, RectInt rectangle)
        {
            var texture = new Texture2D(rectangle.width, rectangle.height);
            texture.LoadImage(spriteData);
            return texture;
        }
    }
}