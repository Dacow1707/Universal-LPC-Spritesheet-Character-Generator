using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public class SpriteSheet : ISpriteSheet
    {
        public SpriteLayer SpriteLayer { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }

        private string[] _tags;

        public string[] Tags
        {
            get
            {
                if (_tags == null)
                {
                    if (string.IsNullOrEmpty(FileName))
                    {
                        _tags = new[] { "Blank" };
                    }
                    else
                    {
                        var path = Path.GetDirectoryName(FileName).Replace(GeneratorConstants.SheetRoot, string.Empty)
                                                              .Trim(new[] { '/', '\\' })
                                                              .Trim();

                        _tags = path.Split(new[] { '\\', '/' });
                    }
                }

                return _tags;
            }
        }

        public Gender Gender { get; set; }

        public Image GetImage()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                return null;
            }
            return Image.FromFile(FileName);
        }

        public bool Matches(string[] blacklist, string[] whitelist)
        {
            var white = whitelist.Length == 0 || Tags.Any(t => whitelist.Contains(t, StringComparer.OrdinalIgnoreCase));
            var black = blacklist.Length == 0 || Tags.Any(t => !blacklist.Contains(t, StringComparer.OrdinalIgnoreCase));

            return white && black;
        }

        public SpriteSheet(string displayName, string file, Gender gender, SpriteLayer layer)
        {
            DisplayName = displayName;
            FileName = file;
            Gender = gender;
            SpriteLayer = layer;
        }
    }
}