using LPC.Spritesheet.Generator;
using LPC.Spritesheet.Generator.Enums;
using LPC.Spritesheet.ResourceManager;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ConsoleGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var genders = new List<Gender> { Gender.Male, Gender.Female };
            foreach (var gender in genders)
            {
                Console.WriteLine(gender);

                var output = $"Out\\Clothes\\{gender}";
                if (Directory.Exists(output))
                {
                    Directory.Delete(output, true);
                }
                Directory.CreateDirectory(output);

                var generator = new CharacterSpriteGenerator(new EmbeddedResourceManager());
                var count = 25;

                for (int i = 0; i < count; i++)
                {
                    Console.Write(i);

                    var character = new CharacterSpriteDefinition(gender, Race.Any);
                    AddLayer(SpriteLayer.Clothes, gender, generator, character);
                    AddLayer(SpriteLayer.Legs, gender, generator, character);
                    AddLayer(SpriteLayer.Shoes, gender, generator, character);

                    if (RandomHelper.Random.Next(10) > 7)
                    {
                        AddLayer(SpriteLayer.Belts, gender, generator, character);
                    }

                    if (RandomHelper.Random.Next(10) > 9)
                    {
                        AddLayer(SpriteLayer.Cape, gender, generator, character);
                    }

                    var imageF = ImageRenderer.GetFullSpriteSheet(character);
                    imageF.Save($"{output}\\{i}.png", ImageFormat.Png);

                    Console.WriteLine("- Done");
                }
            }
        }

        private static void AddLayer(SpriteLayer layer, Gender gender, CharacterSpriteGenerator generator, CharacterSpriteDefinition character)
        {
            var sprites = generator.GetSprites(layer, Race.Any, gender).ToList();
            character.AddLayer(sprites[RandomHelper.Random.Next(0, sprites.Count)]);
        }
    }
}