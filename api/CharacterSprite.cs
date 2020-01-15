using System.Collections.Generic;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public class CharacterSprite : ICharacterSprite
    {
        public List<ISpriteSheet> Layers { get; set; }

        public CharacterSprite()
        {
            Layers = new List<ISpriteSheet>();
        }

        public void AddLayer(ISpriteSheet layer)
        {
            Layers.Add(layer);
        }
    }
}