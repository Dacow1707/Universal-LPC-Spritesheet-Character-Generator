using System.Collections.Generic;
using LPC.Spritesheet.Generator.Enums;
using LPC.Spritesheet.Generator.Interfaces;

namespace LPC.Spritesheet.Generator
{
    public class CharacterSpriteDefinition : ICharacterSpriteDefinition
    {
        public List<ISpriteSheet> Layers { get; set; }
        public Gender Gender { get; set; }

        public CharacterSpriteDefinition(Gender gender)
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