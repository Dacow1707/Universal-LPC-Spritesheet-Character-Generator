using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace LPC.Spritesheet.ResourceManager
{
    public interface IResourceManager
    {
        Stream GetImageStream(string path);

        IEnumerable<string> GetSprites(string path, SearchOption option);
    }
}