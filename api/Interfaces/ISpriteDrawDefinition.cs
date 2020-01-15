using System.Collections.Generic;

namespace Universal.LPC.Spritesheet.Character.Generator.Interfaces
{
    public interface ISpriteDrawDefinition
    {
        int Width { get; set; }
        int Height { get; set; }

        int GetOrder(string path);
        List<ISpriteSheet> GetOrderedLayers(List<ISpriteSheet> layers);
    }
}