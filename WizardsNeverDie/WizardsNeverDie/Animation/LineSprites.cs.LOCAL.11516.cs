using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rithmatist.Farseer;
using FarseerPhysics.Dynamics;
using System.Collections.Generic;

namespace Rithmatist.Animation
{
    public class Line: BasicSprite
    {
        public Vector2 Origin;
        public Texture2D Texture;
        public Color primaryColor = Color.White;
        public Color secondaryColor = Color.Silver;
        public int count;
        public delegate Vector2[] GetPosition();
        public delegate float[] GetRotation();
        public delegate byte[] GetColorAlpha();
        public GetPosition getPosition;
        public GetRotation getRotation;
        public GetColorAlpha getColorAlpha;
        public float _percent = 100;
        public float Percent
        {
            set { _percent = MathHelper.Clamp(value, 0, 100);}
        }
        public Line(Texture2D Texture, int count, GetPosition getPosition, GetRotation getRotation)
        {
            this.Texture = Texture;
            this.Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
            this.count = count;
            this.getPosition += getPosition;
            this.getRotation += getRotation;
        }
        public Line(Texture2D Texture)
        {
            this.Texture = Texture;
            this.Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);

        }
        public override void Update(GameTime theGameTime)
        {
            throw new System.NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            int max = count;
            Vector2[] positions = getPosition();
            float[] rotations = getRotation();
            byte[] alphas = getColorAlpha();
            for (int x = 0; x < max * (_percent/100); x ++)
            {
                secondaryColor.A = alphas[x];
                spriteBatch.Draw(Texture, Utility.ConvertUnits.ToDisplayUnits(positions[x]), null,
                                               primaryColor, rotations[x], Origin, 1f,
                                               SpriteEffects.None, 0f);
            }
            for (int x = (int)(max * ( _percent/100)); x < max; x++)
            {
                primaryColor.A = alphas[x];
                spriteBatch.Draw(Texture, Utility.ConvertUnits.ToDisplayUnits(positions[x]), null,
                                               secondaryColor, rotations[x], Origin, 1f,
                                               SpriteEffects.None, 0f);
            }
        }
    }
}
