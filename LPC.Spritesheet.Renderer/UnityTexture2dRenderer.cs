using LPC.Spritesheet.Generator;
using LPC.Spritesheet.Interfaces;
using LPC.Spritesheet.ResourceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LPC.Spritesheet.Renderer
{
    public class UnityTexture2dRenderer
    {
        public UnityTexture2dRenderer(IResourceManager resourceManager)
        {
            ResourceManager = resourceManager;
        }

        public IResourceManager ResourceManager { get; set; }

        public Texture2D GetFullSpriteSheet(ICharacterSprite sprite)
        {
            return GetSpriteSheet(sprite, new RectInt(0, 0, RendererConstants.SheetWidth, RendererConstants.SheetHeight));
        }

        public Texture2D GetPartialSpriteSheet(ICharacterSprite sprite, Interfaces.Animation animation, Orientation orientation)
        {
            var (row, _) = RendererConstants.SpriteSheetAnimationDefinition[(animation, orientation)];
            return GetSpriteSheet(sprite, new RectInt(0, row * RendererConstants.SpriteWidth, RendererConstants.SheetWidth, RendererConstants.SpriteHeight));
        }

        public Texture2D GetSingleSprite(ICharacterSprite sprite, Interfaces.Animation animation, Orientation orientation, int frame)
        {
            var (row, frames) = RendererConstants.SpriteSheetAnimationDefinition[(animation, orientation)];

            if (frame >= frames)
            {
                throw new IndexOutOfRangeException($"Out of range, Cannot get more than frame count ({frames - 1})");
            }
            return GetSpriteSheet(sprite, new RectInt(frame * RendererConstants.SpriteWidth, row * RendererConstants.SpriteWidth, RendererConstants.SpriteWidth, RendererConstants.SpriteHeight));
        }

        public Texture2D GetSpriteSheet(ICharacterSprite sprite, RectInt rectangle)
        {
            var srcRectange = new RectInt(0, 0, rectangle.width, rectangle.height);
            var newImage = new Texture2D(srcRectange.width, srcRectange.height, TextureFormat.RGBA32, true)
            {
                alphaIsTransparency = true
            };

            try
            {
                var layers = new List<Texture2D>();
                layers.AddRange(RendererConstants.GetOrderedLayers(sprite.Layers).Select(l => GetTexture(l.SpriteData, rectangle)));

                // the first layer ignores the alpha rule, overriding everything
                var firstLayer = true;

                foreach (var layer in layers)
                {
                    for (int x = 0; x < rectangle.width; x++)
                    {
                        for (int y = 0; y < rectangle.height; y++)
                        {
                            var pixel = layer.GetPixel(rectangle.x + x, rectangle.y + y);

                            if (pixel.a == 1 || firstLayer)
                            {
                                newImage.SetPixel(x, y, pixel);
                            }
                        }
                    }
                    firstLayer = false;
                }

                newImage.filterMode = FilterMode.Point;
                newImage.Apply();
                return newImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Texture2D GetTexture(byte[] spriteData, RectInt rectangle)
        {
            var texture = new Texture2D(rectangle.width, rectangle.height);
            texture.LoadImage(spriteData);
            return texture;
        }
    }
}