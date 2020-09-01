using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class CircleCollider
    {
        public CircleCollider(IEntity parent,Vector2 offset,float diametre, GraphicsDevice graphicsDevice)
        {
            this.Parent = parent;
            this.Offset = offset;
            this.Diametre = diametre;
#if DEBUG
            this.texture = createCircleText(graphicsDevice, (int)Diametre);
#endif
        }
#if DEBUG
        Texture2D texture;
#endif
        private IEntity Parent;
        public bool Active { get; set; } = true;
        public Vector2 Offset { get; set; }
        public float Diametre;
        public Vector2 Position => Parent.Position + Vector2.TransformNormal(Offset, Matrix.CreateRotationZ(Parent.Rotation));
        public void Draw(SpriteBatch s)
        {
#if DEBUG
            s.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Diametre, (int)Diametre),null,Active ? Color.LimeGreen : Color.Gray,0,new Vector2(Diametre/2,Diametre/2),SpriteEffects.None,0);
#endif
        }
        Texture2D createCircleText(GraphicsDevice g,int radius)
        {
            Texture2D texture = new Texture2D(g, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = new Color(255,255,255,180);
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }
    }
}
