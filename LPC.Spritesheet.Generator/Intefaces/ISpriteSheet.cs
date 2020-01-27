using LPC.Spritesheet.Generator.Enums;
using System.Drawing;

namespace LPC.Spritesheet.Generator.Interfaces
{
    public interface ISpriteSheet
    {
        SpriteLayer SpriteLayer { get; set; }

        Gender Gender { get; set; }
        Race Race { get; set; }

        string DisplayName { get; set; }

        byte[] SpriteData { get; set; }

        string[] Tags { get; set; }
    }


}