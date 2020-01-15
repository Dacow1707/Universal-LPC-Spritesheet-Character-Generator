using System.Collections.Generic;

namespace Universal.LPC.Spritesheet.Character.Generator.Interfaces
{
    public interface ICharacterSprite
    {
        List<ISpriteSheet> Layers { get; set; }
    }
}