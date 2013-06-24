using Microsoft.Xna.Framework.Graphics;

namespace Rithmatist.Animation
{
    class FrameAnimation : SpriteManager
    {
        public FrameAnimation(Texture2D Texture, int frames, int animations)
            : base(Texture, frames, animations)
        {
        }
        public override void Update(Microsoft.Xna.Framework.GameTime theGameTime)
        {
        }
        public void SetFrame(int frame)
        {
            if (frame < Animations[Animation].NumOfFrames)
                FrameIndex = frame;
        }
    }
}
