using LPC.Spritesheet.Generator;
using LPC.Spritesheet.ResourceManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ConsoleGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Directory.CreateDirectory("Chars");
            var sw = new Stopwatch();
            sw.Start();

            // var resourceManager = new FolderResourceManager();
            var resourceManager = new EmbeddedResourceManager();
            var generator = new CharacterSpriteGenerator(resourceManager);
            var drawEngine = new DefaultSpriteDrawDefinition(resourceManager);
            for (int i = 0; i < 100; i++)
            {
                var character = generator.GetRandomCharacterSprite();

                drawEngine.GetSingleSprite(character, Animation.Walk, Orientation.Front, 1)
                          .Save($"Chars\\{i}.png", ImageFormat.Png);

                drawEngine.GetFullSpriteSheet(character)
                          .Save($"Chars\\{i} Full.png", ImageFormat.Png);

                Console.Write("#");

                var text = new List<string>
                {
                    $"Gender: {character.Gender}"
                };
                text.AddRange(character.Layers.Select(l => $"{l.SpriteLayer} : {l.DisplayName} [{l.Gender}]"));
                File.WriteAllLines($"Chars\\{i} Dump.txt", text);
            }

            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"Done in {sw.Elapsed}");
            Console.ReadKey();
        }
    }
}