using System.Collections.Generic;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public class CharacterSprite : ICharacterSprite
    {
        public List<ISpriteSheet> Layers { get; set; }
        public Gender Gender { get; set; }

        public CharacterSprite(Gender gender)
        {
            Layers = new List<ISpriteSheet>();
            Gender = gender;
        }

        public void AddLayer(ISpriteSheet layer)
        {
            Layers.Add(layer);
        }
    }
}