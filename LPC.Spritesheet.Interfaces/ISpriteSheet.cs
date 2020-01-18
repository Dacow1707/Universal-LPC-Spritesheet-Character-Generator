using System.Drawing;

namespace LPC.Spritesheet.Interfaces
{
    public interface ISpriteSheet
    {
        SpriteLayer SpriteLayer { get; set; }

        Gender Gender { get; set; }

        string DisplayName { get; set; }

        byte[] SpriteData { get; set; }
    }


}