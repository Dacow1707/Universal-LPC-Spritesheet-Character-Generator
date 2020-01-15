using System;
using System.Drawing;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public static class DrawEngine
    {
        public static ISpriteDrawDefinition SpriteDrawDefinition { get; set; } = new DefaultSpriteDrawDefinition();

        public static Image Draw(ICharacterSprite sprite)
        {
            Image newImage = new Bitmap(SpriteDrawDefinition.Width, SpriteDrawDefinition.Height);

            try
            {
                using (var g = Graphics.FromImage(newImage))
                {
                    g.Clear(Color.Transparent);

                    foreach (var layer in SpriteDrawDefinition.GetOrderedLayers(sprite.Layers))
                    {
                        using (var image = layer.Image)
                        {
                            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
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