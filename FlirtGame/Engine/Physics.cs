using System;
using Microsoft.Xna.Framework;

namespace Engine
{
    public static class Physics
    {
        public static bool Collide(CircleCollider c1, CircleCollider c2) => (c1.Active && c2.Active) ? CheckCollisionCircles(c1.Position, c1.Diametre/2, c2.Position, c2.Diametre/2) : false;
        public static bool Collide(CircleCollider c, RectangleCollider r) => (c.Active && r.Active) ? CheckCollisionCircleRec(c.Position, c.Diametre, r.GetRect()) : false;
        public static bool Collide(RectangleCollider r1, RectangleCollider r2) => (r1.Active && r2.Active) ? CheckCollisionRecs(r1.GetRect(), r2.GetRect()) : false;

        public static bool PointOverlap(Vector2 point, Rectangle rectangle) => CheckCollisionPointRec(point, rectangle);

        private static bool CheckCollisionPointRec(Vector2 point, Rectangle rec)
        {
            bool collision = false;

            if ((point.X >= rec.X) && (point.X <= (rec.X + rec.Width)) && (point.Y >= rec.Y) && (point.Y <= (rec.Y + rec.Height))) collision = true;

            return collision;
        }

        // Check if point is inside circle
        private static bool CheckCollisionPointCircle(Vector2 point, Vector2 center, float radius)
        {
            return CheckCollisionCircles(point, 0, center, radius);
        }

        // Check if point is inside a triangle defined by three points (p1, p2, p3)
        private static bool CheckCollisionPointTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            bool collision = false;

            float alpha = ((p2.Y - p3.Y) * (point.X - p3.X) + (p3.X - p2.X) * (point.Y - p3.Y)) /
                          ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));

            float beta = ((p3.Y - p1.Y) * (point.X - p3.X) + (p1.X - p3.X) * (point.Y - p3.Y)) /
                         ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));

            float gamma = 1.0f - alpha - beta;

            if ((alpha > 0) && (beta > 0) & (gamma > 0)) collision = true;

            return collision;
        }

        // Check collision between two rectangles
        private static bool CheckCollisionRecs(Rectangle rec1, Rectangle rec2)
        {
            bool collision = false;

            if ((rec1.X <= (rec2.X + rec2.Width) && (rec1.X + rec1.Width) >= rec2.X) &&
                (rec1.Y <= (rec2.Y + rec2.Height) && (rec1.Y + rec1.Height) >= rec2.Y)) collision = true;

            return collision;
        }

        // Check collision between two circles
        private static bool CheckCollisionCircles(Vector2 center1, float radius1, Vector2 center2, float radius2)
        {
            bool collision = false;

            float dx = center2.X - center1.X;      // X distance between centers
            float dy = center2.Y - center1.Y;      // Y distance between centers

            float distance = (float)Math.Sqrt(dx * dx + dy * dy); // Distance between centers

            if (distance <= (radius1 + radius2)) collision = true;

            return collision;
        }

        
        private static bool CheckCollisionCircleRec(Vector2 center, float radius, Rectangle rec)
        {
            throw new NotImplementedException();
#if false
            //int recCenterX = (int)(rec.X + rec.Width/* / 2.0f*/);
            //int recCenterY = (int)(rec.Y + rec.Height/* / 2.0f*/);
            int recCenterX = rec.Location.X;
            int recCenterY = rec.Location.Y;

            float dx = (float)Math.Abs(center.X - recCenterX);
            float dy = (float)Math.Abs(center.Y - recCenterY);

            if (dx > (rec.Width / 2.0f + radius)) { return false; }
            if (dy > (rec.Height / 2.0f + radius)) { return false; }

            if (dx <= (rec.Width / 2.0f)) { return true; }
            if (dy <= (rec.Height / 2.0f)) { return true; }

            float cornerDistanceSq = (dx - rec.Width / 2.0f) * (dx - rec.Width / 2.0f) +
                                     (dy - rec.Height / 2.0f) * (dy - rec.Height / 2.0f);

            return (cornerDistanceSq <= (radius * radius));
#endif
        }
    }
}
