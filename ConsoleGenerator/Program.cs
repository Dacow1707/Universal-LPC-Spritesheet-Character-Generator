using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Universal.LPC.Spritesheet.Character.Generator;

namespace ConsoleGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Directory.CreateDirectory("Chars");
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 200; i++)
            {
                // get a random sprite that does NOT have one of the tags
                var character = CharacterSpriteGenerator.GetRandomCharacterSprite(new string[] { }, new string[] {  });

                DrawEngine.SpriteDrawDefinition.GetSingleSprite(character, Animation.Walk, Orientation.Front, 1)
                          .Save($"Chars\\{i}.png", ImageFormat.Png);

                //DrawEngine.SpriteDrawDefinition.GetFullSpriteSheet(character)
                //          .Save($"Chars\\{i} Full.png", ImageFormat.Png);

                Console.Write("#");

                var text = new List<string>();
                text.Add($"Gender: {character.Gender}");
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