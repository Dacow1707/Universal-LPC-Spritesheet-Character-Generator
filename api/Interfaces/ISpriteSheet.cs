﻿using System.Drawing;

namespace LPC.Spritesheet.Generator.Interfaces
{
    public interface ISpriteSheet
    {
        SpriteLayer SpriteLayer { get; set; }

        string FileName { get; set; }

        Gender Gender { get; set; }

        string DisplayName { get; set; }
    }

   
}