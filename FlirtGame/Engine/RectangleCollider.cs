using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    //[System.Obsolete("Not emplemented",true)]
    public class RectangleCollider
    {
        public RectangleCollider(IEntity parent, Vector2 offset,Vector2 size, GraphicsDevice graphicsDevice)
        {
            this.Size = size;
            this.Parent = parent;
            this.Offset = offset;
#if DEBUG
           this.texture = new Texture2D(graphicsDevice, 1,1);
            texture.SetData<Color>(new Color[1] { new Color(255, 255, 255, 40) });
#endif
        }
#if DEBUG
        private Texture2D texture;
#endif
        private IEntity Parent;
        public bool Active { get; set; } = true;
        public Vector2 Offset { get; set; }
        public Vector2 Position => Parent.Position + Vector2.TransformNormal(Offset, Matrix.CreateRotationZ(Parent.Rotation));
        public Vector2 Size { get; set; }
        public Rectangle GetRect() => new Rectangle((Position - Size / 2).ToPoint(),Size.ToPoint());

        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            spriteBatch.Draw(texture, GetRect(), Active ? Color.Yellow : Color.Gray);
#endif
        }

    }
}
