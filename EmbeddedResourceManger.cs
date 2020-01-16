using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LPC.Spritesheet.ResourceManager
{
    public class EmbeddedResourceManager : IResourceManager
    {
        public const string Prefix = "LPC.Spritesheet.ResourceManager.spritesheets.";
        private string[] _resourceStrings;
        private Assembly _thisAssembly;

        public string[] ResourceStrings
        {
            get
            {
                if (_resourceStrings == null)
                {
                    _resourceStrings = ThisAssembly.GetManifestResourceNames();
                }
                return _resourceStrings;
            }
        }

        public Assembly ThisAssembly
        {
            get
            {
                if (_thisAssembly == null)
                {
                    _thisAssembly = GetType().Assembly;
                }

                return _thisAssembly;
            }
        }

        public Image GetImage(string path)
        {
            try
            {
                return Image.FromStream(ThisAssembly.GetManifestResourceStream(path));
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<string> GetSprites(string path, SearchOption option)
        {
            var fullPath = Prefix + path.Replace("\\", ".").Replace("/", ".");

            var resources = ResourceStrings.Where(r => r.StartsWith(fullPath, StringComparison.OrdinalIgnoreCase));

            if (option == SearchOption.TopDirectoryOnly)
            {
                // input: SpriteResources.spritesheets.body.female.tanned.png
                // after the split the fullPath is removed we are left with .tanned.png
                // we then split and count the '.' which should return an empty string, tanned and png
                // for deeper levels it should add more .
                // a potential bug here is if the filename includes a '.' it will not be included
                resources = resources.Where(r => r.Replace(fullPath, string.Empty).Split('.').Length == 3);
            }

            return resources;
        }
    }
}