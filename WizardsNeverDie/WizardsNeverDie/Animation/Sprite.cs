using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rithmatist.Animation
{
    class Sprite : BasicSprite
    {
        public Vector2 Origin;
        public Texture2D Texture;
        public Vector2 Position;
        public float Rotation;

        public Sprite(Texture2D texture, Vector2 origin, Vector2 position, float rotation)
        {
            this.Texture = texture;
            this.Origin = origin;
            this.Position = position;
            this.Rotation = rotation;
        }

        public Sprite(Texture2D sprite, Vector2 position, float rotation)
        {
            Texture = sprite;
            Origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            this.Position = position;
            this.Rotation = rotation;
        }
        public override void Update(GameTime theGameTime)
        {
            throw new System.NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Utility.ConvertUnits.ToDisplayUnits(Position), null,
                                           Color.White, Rotation, Origin, 1f,
                                           SpriteEffects.None, 0f);
        }
    }
}