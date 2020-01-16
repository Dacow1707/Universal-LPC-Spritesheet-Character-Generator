using System;
using System.IO;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public static class GeneratorConstants
    {
        private static string _sheetRoot;

        public static string SheetRoot
        {
            get
            {
                if (string.IsNullOrEmpty(_sheetRoot))
                {
                    _sheetRoot = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\spritesheets"));
                }
                return _sheetRoot;
            }
        }

        public const string ImageExtension = "*.png";
    }
}