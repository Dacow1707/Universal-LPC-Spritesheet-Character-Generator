using System.Drawing;

namespace Universal.LPC.Spritesheet.Character.Generator.Interfaces
{
    public interface ISpriteSheet
    {
        string Category { get; set; }

        string[] Tags { get; set; }

        string FileName { get; set; }

        string DisplayName { get; set; }

        Image Image { get;}
    }
}