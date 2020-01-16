using SpriteResources;
using System.Collections.Generic;
using System.Drawing;

namespace Universal.LPC.Spritesheet.Character.Generator.Interfaces
{
    public interface ISpriteDrawDefinition
    {
        int SheetWidth { get; set; }
        int SheetHeight { get; set; }

        int SpriteWidth { get; set; }
        int SpriteHeight { get; set; }

        List<ISpriteSheet> GetOrderedLayers(List<ISpriteSheet> layers);

        Image GetFullSpriteSheet(ICharacterSprite sprite);

        Image GetSingleSprite(ICharacterSprite sprite, Animation animation, Orientation orientation, int frame);

        Image GetPartialSpriteSheet(ICharacterSprite sprite, Animation animation, Orientation orientation);

        IResourceManager ResourceManager { get; set; }
    }
}