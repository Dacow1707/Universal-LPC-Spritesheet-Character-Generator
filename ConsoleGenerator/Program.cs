using LPC.Spritesheet.Generator;
using LPC.Spritesheet.Interfaces;
using LPC.Spritesheet.ResourceManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ConsoleGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var output = "Chars";
            if (Directory.Exists(output))
            {
                Directory.Delete(output, true);
            }
            Directory.CreateDirectory(output);

            var sw = new Stopwatch();
            sw.Start();
            // var resourceManager = new FolderResourceManager();
            var resourceManager = new EmbeddedResourceManager();
            var generator = new CharacterSpriteGenerator(resourceManager);
            var renderer = new UnityTexture2dRenderer(resourceManager);
            var count = 1;

            var images = new List<Image>();
            for (int i = 0; i < count; i++)
            {
                var character = generator.GetRandomCharacterSprite();
                var single = renderer.GetSingleSprite(character, Animation.Walk, Orientation.Front, 1);

                //single.Save($"Chars\\{i}.png", ImageFormat.Png);
                //renderer.GetFullSpriteSheet(character)
                //        .Save($"Chars\\{i} Full.png", ImageFormat.Png);
                //var counter = 0;
                //foreach (var ani in RendererConstants.SpriteSheetAnimationDefinition)
                //{
                //    renderer.GetPartialSpriteSheet(character, ani.Key.animation,ani.Key.orientation).Save($"Chars\\{counter}-{ani.Key.animation}-{ani.Key.orientation}.png");
                //    counter++;
                //}
                //renderer.GetFullSpriteSheet(character)
                //        .Save($"Chars\\{i} Full.png", ImageFormat.Png);

                //Console.Write("#");
                //var text = new List<string>
                //{
                //    $"Gender: {character.Gender}"
                //};
                //text.AddRange(character.Layers.Select(l => $"{l.SpriteLayer} : {l.DisplayName} [{l.Gender}]"));
                //File.WriteAllLines($"Chars\\{i} Dump.txt", text);
            }

            //MergeImages(images, (int)Math.Sqrt(count)).Save($"Chars\\Merged.png", ImageFormat.Png);
            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"Generated {count} sprites in {sw.Elapsed}");
            Console.ReadKey();
        }

        private static Bitmap MergeImages(IEnumerable<Image> images, int columns)
        {
            const int spriteSize = 64;
            var enumerable = images.ToList();

            var width = columns * spriteSize;
            var height = (enumerable.Count / columns) * spriteSize;

            var bitmap = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bitmap))
            {
                var localWidth = 0;
                var y = 0;
                foreach (var image in enumerable)
                {
                    if (localWidth >= width)
                    {
                        localWidth = 0;
                        y++;
                    }

                    g.DrawImage(image, localWidth, y * spriteSize);
                    localWidth += image.Width;

                    
                }
            }
            return bitmap;
        }
    }
}