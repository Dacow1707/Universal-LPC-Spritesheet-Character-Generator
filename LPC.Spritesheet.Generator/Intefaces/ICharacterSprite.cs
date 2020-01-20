using LPC.Spritesheet.Generator.Enums;
using System.Collections.Generic;

namespace LPC.Spritesheet.Generator.Interfaces
{
    public interface ICharacterSpriteDefinition
    {
        Gender Gender { get; set; }

        List<ISpriteSheet> Layers { get; set; }
    }
}