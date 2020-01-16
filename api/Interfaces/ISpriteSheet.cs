using System.Drawing;

namespace Universal.LPC.Spritesheet.Character.Generator.Interfaces
{
    public interface ISpriteSheet
    {
        SpriteLayer SpriteLayer { get; set; }

        string FileName { get; set; }

        Gender Gender { get; set; }

        string DisplayName { get; set; }
    }

   
}