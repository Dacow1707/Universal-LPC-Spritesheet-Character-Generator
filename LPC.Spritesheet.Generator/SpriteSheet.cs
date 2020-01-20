using LPC.Spritesheet.Generator.Enums;
using LPC.Spritesheet.Generator.Interfaces;
using System.IO;

namespace LPC.Spritesheet.Generator
{
    public class SpriteSheet : ISpriteSheet
    {
        public SpriteLayer SpriteLayer { get; set; }
        public string DisplayName { get; set; }

        public Gender Gender { get; set; }
        public byte[] SpriteData { get; set; }

        public SpriteSheet(string displayName, Stream stream, Gender gender, SpriteLayer layer)
        {
            DisplayName = displayName;
            Gender = gender;
            SpriteLayer = layer;
            SpriteData = ReadStream(stream);
        }

        public static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}