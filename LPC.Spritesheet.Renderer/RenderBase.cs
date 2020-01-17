using LPC.Spritesheet.ResourceManager;

namespace LPC.Spritesheet.Generator
{
    public class RenderBase
    {
        public RenderBase(IResourceManager resourceManager)
        {
            ResourceManager = resourceManager;
        }

        public IResourceManager ResourceManager { get; set; }
    }
}