using System;
using System.Collections.Generic;
using System.IO;

namespace LPC.Spritesheet.ResourceManager
{
    public class FolderResourceManager : IResourceManager
    {
        public const string ImageExtension = "*.png";

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

        public Stream GetImageStream(string path)
        {
            var ms = new MemoryStream();

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);
            }

            return ms;
        }

        public IEnumerable<string> GetSprites(string path, SearchOption option)
        {
            return Directory.EnumerateFiles(Path.Combine(SheetRoot, path), ImageExtension, option);
        }
    }
}