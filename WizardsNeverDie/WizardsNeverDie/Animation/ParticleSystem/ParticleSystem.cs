using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rithmatist.Animation.ParticleSystem
{
    public sealed class ParticleSystem
    {
        private static readonly ParticleSystem instance = new ParticleSystem();
        private static readonly Random random = new Random();
        public static ParticleSystem Instance
        {
            get
            {
                return instance;
            }
        }
        public static Random Random
        {
            get { return random; }
        }

        Queue<Particle> freeParticles;
        List<Particle> particles;
        public int FreeParticleCount
        {
            get { return freeParticles.Count; }
        }
        SpriteFont font;
        Vector2 center;
        public void Initialize(Game game)
        {
            center = new Vector2(game.Window.ClientBounds.Center.X, game.Window.ClientBounds.Center.Y);
            // calculate the total number of particles we will ever need, using the
            // max number of effects and the max number of particles per effect.
            // once these particles are allocated, they will be reused, so that
            // we don't put any pressure on the garbage collector.
            font = game.Content.Load<SpriteFont>("font");
            particles = new List<Particle>();
            freeParticles = new Queue<Particle>();
            for (int i = 0; i < 100; i++)
            {
                Particle particle = new Particle();
                particles.Add(particle);
                freeParticles.Enqueue(particle);
            }
        }
        public Particle InitializeParticle(Vector2 position, Vector2 velocity, Vector2 acceleration,
                float lifetime, Vector2 scale, float rotationSpeed, Color color, Texture2D texture,
                bool sinDisplacement, Vector2 amplitude, Vector2 frequency,
                bool alphaFade, bool sizeFade, float fadeIn, float fadeOut)
        {
            Particle particle;
            if (freeParticles.Count > 0)
                particle = freeParticles.Dequeue();
            else
            {
                particle = new Particle();
                particles.Add(particle);
            }
            particle.Initialize(position, velocity, acceleration, lifetime, scale, rotationSpeed, color, texture, sinDisplacement, amplitude, frequency, alphaFade, sizeFade, fadeIn, fadeOut);
            return particle;
        }
        public static Vector2 PickRandomDirection()
        {
            float angle = RandomBetween(0, MathHelper.TwoPi);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static float RandomBetween(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }
        public void Update(GameTime gameTime)
        {
            foreach (Particle p in particles)
            {
                if (p.Active)
                {
                    p.Update(gameTime);
                    if (!p.Active)
                    {
                        freeParticles.Enqueue(p);
                    }
                }
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch SpriteBatch)
        {
            foreach (Particle p in particles)
            {
                if (!p.Active)
                    continue;
                SpriteBatch.Draw(p.texture, p.DisplayPosition, null,  p.Color,
                    p.Rotation, p.Origin, p.Scale /4, SpriteEffects.None, 0.0f);
            }
        }
    }
}
