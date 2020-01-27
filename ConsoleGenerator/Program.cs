using LPC.Spritesheet.Generator;
using LPC.Spritesheet.Generator.Enums;
using LPC.Spritesheet.Generator.Interfaces;
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
            //if (Directory.Exists(output))
            //{
            //    Directory.Delete(output, true);
            //}
            Directory.CreateDirectory(output);

            var sw = new Stopwatch();
            sw.Start();
            var generator = new CharacterSpriteGenerator(new EmbeddedResourceManager());
            var count = 27;

            var images = new List<Image>();
            for (int i = 0; i < count; i++)
            {
                Console.Write("<");

                var character = generator.GetRandomCharacterSprite(Race.Elf);
                var image = ImageRenderer.GetSingleSprite(character, Animation.Walk, Orientation.Front, 2);
                image.Save($"Chars\\{i}.png", ImageFormat.Png);

                var imageF = ImageRenderer.GetFullSpriteSheet(character);
                imageF.Save($"Chars\\{i}F.png", ImageFormat.Png);
                Console.Write(">");

                var text = new List<string>
                {
                    $"Gender: {character.Gender}",
                    $"Race: {character.Race}",
                };
                text.AddRange(character.Layers.Select(l => $"{l.SpriteLayer} : {l.DisplayName} [{l.Gender}]"));
                File.WriteAllLines($"Chars\\{i} Dump.txt", text);
            }

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