using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace SpriteResources
{
    public interface IResourceManager
    {
        Image GetImage(string path);

        IEnumerable<string> GetSprites(string path, SearchOption option);
    }
}