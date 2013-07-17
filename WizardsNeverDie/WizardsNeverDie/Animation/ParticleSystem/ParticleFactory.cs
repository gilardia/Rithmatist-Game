using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Rithmatist.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.DebugViews;
using Rithmatist.Farseer;
using Rithmatist.Level;
using Rithmatist.Animation;
using Rithmatist.Intelligence;
using System.IO;
using Rithmatist.Entities.Rithmatics;
using Rithmatist.Farseer;
using FarseerPhysics.Dynamics;

namespace Rithmatist.Animation.ParticleSystem
{
    public class ParticleFactory
    {
        public static readonly ParticleFactory Instance = new ParticleFactory();

        Texture2D chalkTexture;
        public void Initialize(ContentManager content)
        {
            chalkTexture = content.Load<Texture2D>("Sprites/Dust");
        }
        public void CreateChalkDust(int number, Vector2 position, Color color)
        {
            for (int x = 0; x < number; x++)
            {
                Vector2 force = new Vector2((float)ParticleSystem.Random.NextDouble() - .5f, (float)ParticleSystem.Random.NextDouble() - .5f);
                Vector2 acceleration = new Vector2((float)ParticleSystem.Random.NextDouble() - .5f, (float)ParticleSystem.Random.NextDouble() - .5f);
                float lifetime = 2f;
                Vector2 scale = Vector2.One;
                float rotationSpeed = (float)ParticleSystem.Random.NextDouble() * 4 - 2;
                float fadeIn = (float)ParticleSystem.Random.NextDouble() * 2;
                float fadeOut = (float)ParticleSystem.Random.NextDouble() * 2;
                bool alphaFade = true;
                bool sizeFade = false;
                bool sinDisplacement = false;
                Vector2 frequency = Vector2.Zero;
                Vector2 amplitude = Vector2.Zero;
                Vector2 phase = Vector2.Zero;
                float random = (float)ParticleSystem.Random.NextDouble();
                ParticleSystem.Instance.InitializeParticle(position, force, acceleration, lifetime, scale, rotationSpeed, color, chalkTexture, sinDisplacement, amplitude, frequency, alphaFade, sizeFade, fadeIn, fadeOut);
            }
        }
    }
}
