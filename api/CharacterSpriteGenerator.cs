using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public static class CharacterSpriteGenerator
    {
        public static string SheetRoot
        {
            get
            {
                return Path.Combine(Environment.CurrentDirectory, @"..\..\..\spritesheets");
            }
        }

        private static Dictionary<string, List<ISpriteSheet>> _spriteDictionary;

        public static Dictionary<string, List<ISpriteSheet>> SpriteDictionary
        {
            get
            {
                if (_spriteDictionary == null)
                {
                    _spriteDictionary = new Dictionary<string, List<ISpriteSheet>>();
                    foreach (var categoryDirectory in Directory.EnumerateDirectories(SheetRoot))
                    {
                        var category = categoryDirectory.Replace(SheetRoot, string.Empty).Trim(new[] { '/', '\\' });
                        _spriteDictionary.Add(category, new List<ISpriteSheet>());

                        var categoryPath = Path.Combine(SheetRoot, category);
                        _spriteDictionary[category].AddRange(Directory.EnumerateFiles(categoryDirectory, "*.png", SearchOption.AllDirectories)
                                                                 .Select(file => new SpriteSheet(file.Replace(categoryPath, string.Empty).Trim('\\'), categoryPath, category)));
                    }
                }
                return _spriteDictionary;
            }
        }

        public static ICharacterSprite GetRandomCharacterSprite(params string[] exclusions)
        {
            var character = new CharacterSprite();

            foreach (var category in SpriteDictionary.Keys)
            {
                // if category is not 'body' we have a 33% of just skipping to add some variety
                if (category != "body" && RandomHelper.Random.Next(100) < 33)
                {
                    continue;
                }
                if (SpriteDictionary[category].Count > 0)
                {
                    var options = SpriteDictionary[category].Where(c => !c.Tags.Any(t => exclusions.Contains(t, StringComparer.OrdinalIgnoreCase))).ToList();

                    if (options.Count > 0)
                    {
                        character.AddLayer(options[RandomHelper.Random.Next(0, options.Count)]);
                    }
                }
            }

            return character;
        }
    }
}