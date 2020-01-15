using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Universal.LPC.Spritesheet.Character.Generator;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace ConsoleGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Directory.CreateDirectory("Chars");
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                // get a random sprite that does NOT have one of the tags
                // todo: 
                var character = CharacterSpriteGenerator.GetRandomCharacterSprite("none", "pregnant", "female", "child", "oversize", "eyes", "ears", "nose");

                DrawEngine.SpriteDrawDefinition.GetSingleSprite(character, Animation.Walk, Orientation.Front, 1)
                          .Save($"Chars\\{i}.png", ImageFormat.Png);

                DrawEngine.SpriteDrawDefinition.GetFullSpriteSheet(character)
                          .Save($"Chars\\{i} Full.png", ImageFormat.Png);

                Console.Write("#");

                File.WriteAllLines($"Chars\\{i} Dump.txt", character.Layers.Select(l => $"{l.Category} : {l.DisplayName} [{l.Tags.Aggregate("", (current, next) => current + ";" + next).Trim(';')}]"));
            }

            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"Done in {sw.Elapsed}");
            Console.ReadKey();
        }
    }
}