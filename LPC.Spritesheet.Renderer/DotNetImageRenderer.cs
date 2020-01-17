using LPC.Spritesheet.Interfaces;
using LPC.Spritesheet.ResourceManager;
using System;
using System.Drawing;

namespace LPC.Spritesheet.Generator
{
    public class DotNetImageRenderer : RenderBase
    {
        public DotNetImageRenderer(IResourceManager resourceManager) : base(resourceManager)
        {
        }

        public Image GetFullSpriteSheet(ICharacterSprite sprite)
        {
            return GetSpriteSheet(sprite, new Rectangle(0, 0, RendererConstants.SheetWidth, RendererConstants.SheetHeight));
        }

        public Image GetPartialSpriteSheet(ICharacterSprite sprite, Animation animation, Orientation orientation)
        {
            var (row, _) = RendererConstants.SpriteSheetAnimationDefinition[(animation,orientation)];
            return GetSpriteSheet(sprite, new Rectangle(0, row * RendererConstants.SpriteWidth, RendererConstants.SheetWidth, RendererConstants.SpriteHeight));
        }

        public Image GetSingleSprite(ICharacterSprite sprite, Animation animation, Orientation orientation, int frame)
        {
            var (row, frames) = RendererConstants.SpriteSheetAnimationDefinition[(animation, orientation)];

            if (frame >= frames)
            {
                throw new IndexOutOfRangeException($"Out of range, Cannot get more than frame count ({frames - 1})");
            }
            return GetSpriteSheet(sprite, new Rectangle(frame * RendererConstants.SpriteWidth, row * RendererConstants.SpriteWidth, RendererConstants.SpriteWidth, RendererConstants.SpriteHeight));
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

                    foreach (var layer in RendererConstants.GetOrderedLayers(sprite.Layers))
                    {
                        using (var stream = ResourceManager.GetImageStream(layer.FileName))
                        using (var image = Image.FromStream(stream))
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