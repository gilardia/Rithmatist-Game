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
    class LineOfForbiddance : RithmaticLine
    {
        //Size should be between .05 and .2
        public const float WIDTH = .1f;
        public float HEIGHT = .1f;
        public List<Vector2> initialPoints = new List<Vector2>();

        List<LineDamage> totalDamage = new List<LineDamage>();
        public const float HEALTH = 50;
        public float length;
        public float getHealth(Vector2 position)
        {
            float health = HEALTH;
            Vector2 localPosition = position - body.Position;
            foreach (LineDamage damage in totalDamage)
                health -= damage.getDamage(localPosition);
            return health;
        }
        public LineOfForbiddance(Animation.DrawingSystem.AssetCreator assetCreator, Vector2 origin, Vector2 distance)
        {
            this.length = distance.Length();
            initialPoints.Add(origin);
            initialPoints.Add(origin + distance);
            body = new ForbiddanceBody(this, initialPoints, WIDTH);
            initializeDraw(assetCreator);
        }
        public LineOfForbiddance(Animation.DrawingSystem.AssetCreator assetCreator, List<Vector2> points)
        {
            this.length = 0;
            for (int i = 0; i < points.Count - 1; i++)
                this.length += Vector2.Distance(points[i], points[i + 1]);
            this.initialPoints = points;
            body = new ForbiddanceBody(this, initialPoints, WIDTH);
            initializeDraw(assetCreator);
        }
        public override void createBody()
        {
            Vertices path = pointsToLine(initialPoints);
            body = BasicBody.CreatePolygonBody(this, path, WIDTH);
            body.setCollision(Physics.CollisionGroup.LineOfForbiddance, Physics.CollisionGroup.LineOfMaking | Physics.CollisionGroup.LineOfVigor | Physics.CollisionGroup.Player);
            foreach (Body b in body.Bodies)
                b.BodyType = BodyType.Static;
        }
        public void initializeDraw(Animation.DrawingSystem.AssetCreator assetCreator)
        {
            Vertices box = PolygonTools.CreateRectangle(WIDTH, HEIGHT);
            PolygonShape shape = new PolygonShape(box, 20);
            line = new Line(assetCreator.TextureFromVertices(shape.Vertices, Animation.DrawingSystem.MaterialType.Blank, Color.WhiteSmoke, 1f));
            ForbiddanceBody fBody = body as ForbiddanceBody;
            line.count = fBody.getGraphicsCount();
            line.getPosition += fBody.getGraphicPosition;
            line.getRotation += fBody.getGraphicRotation;
            line.getColorAlpha += fBody.getColorAlpha;
        }
        private Vertices pointsToLine(List<Vector2> points)
        {
            Vertices path = new Vertices();
            Vector2 direction = getDirection(points);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);
            perpendicular.Normalize();
            for (int x = 0; x < points.Count; x++)
            {
                path.Add(points[x] + perpendicular * WIDTH / 2);
            }
            for (int x = points.Count - 1; x >= 0; x--)
            {
                path.Add(points[x] - perpendicular * WIDTH / 2);
            }
            return path;
        }
        public override void Dispose()
        {
            body.Dispose();
            body = null;
            line.secondaryColor.A = 0;
            line = null;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (line != null)
                line.Draw(spriteBatch);
        }
    }
}
