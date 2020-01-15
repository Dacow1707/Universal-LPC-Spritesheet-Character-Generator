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
            for (int i = 0; i < 10; i++)
            {
                var character = CharacterSpriteGenerator.GetRandomCharacterSprite("female", "child", "oversize", "eyes", "ears", "nose");
                DrawEngine.Draw(character)
                          .Save($"Chars\\Char {i}.png", ImageFormat.Png);

                File.WriteAllLines($"Chars\\Char {i}.txt", character.Layers.Select(l => $"{l.Category} : {l.DisplayName} [{l.Tags.Aggregate("", (current, next) => current + ";" + next).Trim(';')}]"));
            }
        }
    }
}