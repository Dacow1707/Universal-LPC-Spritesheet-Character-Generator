using System.Collections.Generic;
using System.Linq;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public class DefaultSpriteDrawDefinition : ISpriteDrawDefinition
    {
        public int Width { get; set; } = 832;
        public int Height { get; set; } = 1344;

        public List<string> LayerOrder = new List<string>()
        {
            "behind_body",
            "body",
            "head",
            "arms",
            "legs",

            "facial",
            "torso",
            "belt",
            "feet",
            "hands",
            "oversize_xcf",
            "shoulders",
            "accessories",
            "hair",
            "weapons",

            "palettes",
        };

        public int GetOrder(string path)
        {
            return LayerOrder.IndexOf(path.Split(new[] { '/', '\\' })[0].ToLower());
        }

        public List<ISpriteSheet> GetOrderedLayers(List<ISpriteSheet> layers)
        {
            return layers.OrderBy(l => GetOrder(l.Category)).ToList();
        }
    }
}