using System.Collections.Generic;

namespace LPC.Spritesheet.Generator.Interfaces
{
    public interface ICharacterSprite
    {
        Gender Gender { get; set; }

        List<ISpriteSheet> Layers { get; set; }
    }
}