using System;
using System.Drawing;
using System.IO;
using System.Linq;
using LPC.Spritesheet.Generator.Interfaces;

namespace LPC.Spritesheet.Generator
{
    public class SpriteSheet : ISpriteSheet
    {
        public SpriteLayer SpriteLayer { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }

        public Gender Gender { get; set; }

        public SpriteSheet(string displayName, string file, Gender gender, SpriteLayer layer)
        {
            DisplayName = displayName;
            FileName = file;
            Gender = gender;
            SpriteLayer = layer;
        }
    }
}