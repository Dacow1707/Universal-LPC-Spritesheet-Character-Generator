using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SpriteResources
{
    public class FolderResourceManager : IResourceManager
    {
        public Image GetImage(string path)
        {
            return Image.FromFile(path);
        }

        public IEnumerable<string> GetSprites(string path, SearchOption option)
        {
            return Directory.EnumerateFiles(Path.Combine(SheetRoot, path), ImageExtension, option);
        }

      

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