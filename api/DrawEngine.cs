using System;
using System.Drawing;
using Universal.LPC.Spritesheet.Character.Generator.Interfaces;

namespace Universal.LPC.Spritesheet.Character.Generator
{
    public static class DrawEngine
    {
        public static ISpriteDrawDefinition SpriteDrawDefinition { get; set; } = new DefaultSpriteDrawDefinition();

        
    }
}