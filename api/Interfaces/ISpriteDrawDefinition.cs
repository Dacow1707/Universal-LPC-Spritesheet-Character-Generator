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

        int GetOrder(string path);

        List<ISpriteSheet> GetOrderedLayers(List<ISpriteSheet> layers);

        Image GetFullSpriteSheet(ICharacterSprite sprite);

        Image GetSingleSprite(ICharacterSprite sprite, Animation animation, Orientation orientation, int frame);

        Image GetPartialSpriteSheet(ICharacterSprite sprite, Animation animation, Orientation orientation);
    }

    public enum Orientation
    {
        Front, Back, Left, Right
    }

    public enum Animation
    {
        Spellcast, Thrust, Walk, Slash, Shoot, Die
    }
}