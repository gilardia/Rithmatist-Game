using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Rithmatist.Entities.Rithmatics.Chalkling;

namespace Rithmatist.Entities.Rithmatics
{
    class RithmaticFactory
    {
        public delegate void onCreation(RithmaticLine entity);
        public static onCreation onWardingCreation = null;
        public static onCreation onForbiddanceCreation = null;
        public static onCreation onMakingCreation = null;
        public static onCreation onVigorCreation = null;
        static List<RithmaticLine> lines = new List<RithmaticLine>();
        public static void Update(GameTime theGameTime)
        {
            foreach (RithmaticLine line in lines)
            {
                line.Update(theGameTime);
            }
        }
        public static void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (RithmaticLine line in lines)
            {
                line.Draw(theSpriteBatch);
            }
        }
        public static void Update()
        {
        }
        public static RithmaticLine CreateLineOfWarding(Animation.DrawingSystem.AssetCreator assetCreator, Vector2 position, float radius)
        {
            RithmaticLine entity =  new LineOfWarding(assetCreator, position, radius);
            if (onWardingCreation != null)
                onWardingCreation(entity);
            lines.Add(entity);
            return entity;
        }
        public static RithmaticLine CreateLineOfWarding(Animation.DrawingSystem.AssetCreator assetCreator, List<Vector2> points)
        {
            RithmaticLine entity = new LineOfWarding(assetCreator, points);
            if (onWardingCreation != null)
                onWardingCreation(entity);
            lines.Add(entity);
            return entity;
        }
        public static RithmaticLine CreateLineOfForbiddance(Animation.DrawingSystem.AssetCreator assetCreator, Vector2 p1, Vector2 p2)
        {
            RithmaticLine entity = new LineOfForbiddance(assetCreator, p1, p2);
            if (onForbiddanceCreation != null)
                onForbiddanceCreation(entity);
            lines.Add(entity);
            return entity;
        }
        public static RithmaticLine CreateLineOfForbiddance(Animation.DrawingSystem.AssetCreator assetCreator, List<Vector2> points)
        {
            RithmaticLine entity = new LineOfForbiddance(assetCreator, points);
            if (onForbiddanceCreation != null)
                onForbiddanceCreation(entity);
            lines.Add(entity);
            return entity;
        }
        public static RithmaticLine CreateLineOfMaking(ChalklingStats stats, ChalklingInstructions instructions, Vector2 position)
        {
            RithmaticLine entity = new LineOfMaking(instructions, stats, position);
            if (onMakingCreation != null)
                onMakingCreation(entity);
            lines.Add(entity);
            return entity;
        }
        public static RithmaticLine CreateLineOfVigor(Animation.DrawingSystem.AssetCreator assetCreator, Vector2 p1, Vector2 p2, float amplitude, float frequency, float rotation)
        {
            RithmaticLine entity = new LineOfVigor(assetCreator, p1, p2, amplitude, frequency, rotation);
            if (onVigorCreation != null)
                onVigorCreation(entity);
            lines.Add(entity);
            return entity;
        }
        public static RithmaticLine CreateLineOfVigor(Animation.DrawingSystem.AssetCreator assetCreator, Vector2 intendedDirection, List<Vector2> points)
        {
            RithmaticLine entity = new LineOfVigor(assetCreator, points);
            if (onVigorCreation != null)
                onVigorCreation(entity);
            lines.Add(entity);
            return entity;
        }
    }
}
