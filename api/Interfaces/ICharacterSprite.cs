using System.Collections.Generic;

namespace Universal.LPC.Spritesheet.Character.Generator.Interfaces
{
    public interface ICharacterSprite
    {
        Gender Gender { get; set; }

        List<ISpriteSheet> Layers { get; set; }
    }
}