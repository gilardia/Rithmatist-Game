using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Rithmatist.Utility;
using System;
/* TO DO
 * 1. DONE: Particles should fade in and out
 * 2. DONE: Particles should go for a distance based on the width/height of the object
 * 3. DONE: Particle density should be dependant on the element and intensity (This requires adjusting for the area)
 * 4. Better particle sprites should be added
 * 5. DONE: Particle spawning should be favored towards the the side opposite of the intensity so that particles will last longer and be more even
 * 6. DONE Particles should be more object oriented with different particles for each element
 * 7. DONE: Particle colors should be on a gradient dependant on the color.
 */
namespace Rithmatist.Animation.ParticleSystem
{
    public class Particle
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public float fadeIn;
        public float fadeOut;
        public bool alphaFade;
        public bool sizeFade;
        public bool sinDisplacement;
        public Vector2 frequency;
        public Vector2 amplitude;
        public Vector2 phase;

        protected float lifetime;
        protected float timeSinceStart;
        public Color color;
        public Texture2D texture;
        public float Rotation;
        protected float RotationVelocity;
        protected bool Dying = false;
        public Vector2 Origin;
        protected Vector2 _Scale;
        protected Vector2 _ModifiedScale;
        protected Color _Color;
        protected Color _ModifiedColor;
        protected Vector2 _Position;
        protected Vector2 _ModifiedPosition;
        public Vector2 Scale
        {
            get
            {
                return _ModifiedScale;
            }
        }
        public Color Color
        {
            get{
                return _ModifiedColor;
            }
        }
        public Vector2 Position
        {
            set
            {
                _Position = value;
            }
            get
            {
                return _ModifiedPosition;
            }
        }
        public Vector2 DisplayPosition
        {
            get
            {
                return ConvertUnits.ToDisplayUnits(_ModifiedPosition);
            }
        }
        public bool Active
        {
            get { return timeSinceStart < lifetime; }
        }
        public void Kill()
        {
            if (!Dying)
            {
                Dying = true;
                timeSinceStart = lifetime - (lifetime - timeSinceStart) / 4;
            }
        }
        public void Initialize(Vector2 position, Vector2 velocity, Vector2 acceleration,
                float lifetime, Vector2 scale, float rotationSpeed, Color color, Texture2D texture,
                bool sinDisplacement, Vector2 amplitude, Vector2 frequency,
                bool alphaFade, bool sizeFade, float fadeIn, float fadeOut)
        {
            // set the values to the requested values
            this._Position = position;
            this.Velocity = velocity;
            this.Acceleration = acceleration;
            this.lifetime = lifetime;
            this.RotationVelocity = rotationSpeed;
            this.texture = texture;
            this._Color = color;
            this._ModifiedColor = _Color;
            this.Origin = new Vector2( texture.Width / 2, texture.Height / 2);
            this._Scale = scale;
            this._ModifiedScale = _Scale;
            this.sinDisplacement = sinDisplacement;
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.phase = ((float)(ParticleSystem.Random.NextDouble() * Math.PI) * 2 * Vector2.UnitX + (float)(ParticleSystem.Random.NextDouble() * Math.PI) * 2 * Vector2.UnitY);
            this.Dying = false;
            this.timeSinceStart = 0.0f;
            this.fadeIn = Math.Min(fadeIn, .5f);
            this.fadeOut = Math.Min(fadeOut, .5f);
            this.alphaFade = alphaFade;
            this.sizeFade = sizeFade;
        }

        // update is called by the ParticleSystem on every frame. This is where the
        // particle's position and that kind of thing get updated.
        public void Update(GameTime gameTime)
        {
            UpdatePosition(gameTime);
            UpdateScale();
            UpdateAlpha();
        }
        public void Update(float elapsedTime)
        {
            UpdatePosition(elapsedTime);
            UpdateScale();
            UpdateAlpha();
        }

        private void UpdateAlpha()
        {
            if (alphaFade)
            {
                float p = timeSinceStart / lifetime;
                if (p < fadeIn)
                {
                    _ModifiedColor = _Color * (p / fadeIn);
                }
                else if (1 - p < fadeOut)
                {
                    _ModifiedColor = _Color * ((1 - p) / fadeOut);
                }
            }
        }

        private void UpdateScale()
        {
            if (sizeFade)
            {
                float p = timeSinceStart / lifetime;
                Vector2 scale = _Scale;
                if (p < fadeIn)
                {
                    _ModifiedScale = scale * p / fadeIn;
                }
                else if (1 - p < fadeOut)
                {
                    _ModifiedScale = scale * (1 - p) / fadeOut;
                }
                else
                {
                    _ModifiedScale = _Scale;
                }
            }  
        }
        private void UpdatePosition(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity += Acceleration * dt;
            _Position += Velocity * dt;
            Rotation += RotationVelocity * dt;
            float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
            if (sinDisplacement)
            {
                _ModifiedPosition.X = _Position.X + amplitude.X * (float)Math.Sin(2 * Math.PI * frequency.X * totalTime + phase.X) / 100;
                _ModifiedPosition.Y = _Position.Y + amplitude.Y * (float)Math.Sin(2 * Math.PI * frequency.Y * totalTime + phase.Y) / 100;
            }
            else
            {
                _ModifiedPosition = _Position;
            }
            timeSinceStart += dt;
        }
        private void UpdatePosition(float gameTime)
        {
            float dt = (float)gameTime;
            Velocity += Acceleration * dt;
            _Position += Velocity * dt;
            Rotation += RotationVelocity * dt;
            timeSinceStart += dt;
        }
        public void MovePosition(Vector2 vector)
        {
            _Position += vector;
        }
    }
}
