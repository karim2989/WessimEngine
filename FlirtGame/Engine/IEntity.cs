using Microsoft.Xna.Framework;

namespace Engine
{
    public interface IEntity
    {
        Vector2 Position { get; set; }
        Vector2 Direction { get; }
        float Rotation { get; }
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
