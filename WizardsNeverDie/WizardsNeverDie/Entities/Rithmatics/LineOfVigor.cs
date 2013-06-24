using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Rithmatist.Entities.Rithmatics.Chalkling;
using Rithmatist.Farseer;
using FarseerPhysics.Common;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Rithmatist.Animation;
using FarseerPhysics.Factories;

namespace Rithmatist.Entities.Rithmatics
{
    class LineOfVigor : RithmaticLine
    {
        //Size should be between .05 and .2
        public float SIZE = .1f;
        //The line vector should be determine through linear regression
        List<Vector2> points = new List<Vector2>();
        public Vector2 direction;
        public LineOfVigor(Animation.DrawingSystem.AssetCreator assetCreator, Vector2 start, Vector2 distance, float amplitude, float frequency, float rotation)
        {
            float length = distance.Length();
            Vector2 perpendicular = new Vector2(-distance.Y, distance.X);
            perpendicular.Normalize();
            for (float progress = 0; progress < length; progress += SIZE)
            {
                Vector2 point = start + distance * progress / length + perpendicular * amplitude * ((float)Math.Sin(progress * frequency) - .5f);
                points.Add(point);
            }
            this.direction = distance;
            body = new VigorBody(this, direction, points, SIZE);
            initializeDraw(assetCreator);
        }
        public LineOfVigor(Animation.DrawingSystem.AssetCreator assetCreator, List<Vector2> points)
        {
            this.points = points;
            this.direction =  getDirection(points);
            body = new VigorBody(this, direction, points, SIZE);
            initializeDraw(assetCreator);
        }
        public override void Dispose()
        {
            if(body != null)
                body.Dispose();
            body = null;
            line = null;
        }
        public void initializeDraw(Animation.DrawingSystem.AssetCreator assetCreator)
        {
            Vertices box = PolygonTools.CreateRectangle(SIZE, SIZE);
            PolygonShape shape = new PolygonShape(box, 20);
            line = new Line(assetCreator.TextureFromVertices(shape.Vertices, Animation.DrawingSystem.MaterialType.Blank, Color.WhiteSmoke, 1f));
            VigorBody vBody = body as VigorBody;
            line.count = vBody.getGraphicsCount();
            line.getPosition += vBody.getGraphicPosition;
            line.getRotation += vBody.getGraphicRotation;
            line.getColorAlpha += vBody.getColorAlpha;
        }
        public override void createBody()
        {
            (body as RithmaticBody).createBody();
            body.setCollision(Physics.CollisionGroup.LineOfVigor, Physics.CollisionGroup.LineOfWarding | Physics.CollisionGroup.LineOfMaking | Physics.CollisionGroup.LineOfForbiddance);
        }
        public override void Update(GameTime gameTime)
        {
            if(body!= null && body.Bodies.Count != 0)
                body.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(line != null)
                line.Draw(spriteBatch);
        }
    }
}
