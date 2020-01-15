using System;
using System.Drawing;
using System.IO;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public class SpriteSheet : ISpriteSheet
    {
        public string[] Tags { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }

        public Image Image
        {
            get
            {
                return Image.FromFile(FileName);
            }
        }

        public string Category { get; set; }

        public SpriteSheet(string path, string root, string category)
        {
            Category = category;
            FileName = Path.Combine(root, path);
            DisplayName = Path.GetFileNameWithoutExtension(FileName);

            Tags = path.Replace(Path.GetFileName(FileName), string.Empty).Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}