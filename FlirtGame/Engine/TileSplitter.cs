using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Engine
{
    class TileSplitter
    {
        public static SingleSprite SplitSprite(GraphicsDevice graphicsDevice, Texture2D atlas,Point pos,Point size)
            => new SingleSprite(graphicsDevice, atlas, new Rectangle(pos, size));

        public static SingleSprite[] SplitLeftUpToBottomRight(GraphicsDevice graphicsDevice, Texture2D atlas, Point spriteSize, Point? start = null, Point? end = null)
        {
            var _end = end == null || !end.HasValue ? new Point(atlas.Width / spriteSize.X, atlas.Height / spriteSize.Y) : end.Value;
            var _start = start == null || !start.HasValue ? Point.Zero : start.Value;
            var spriteCount = new Point(_end.X - _start.X, _end.Y - _start.Y);

            var output = new SingleSprite[spriteCount.X * spriteCount.Y];

            int frameIndex = 0;
            for (int x = _start.X; x < _end.X; x++)
                for (int y = _start.Y; y < _end.Y; y++)
                {
                    output[frameIndex] = new SingleSprite(graphicsDevice, atlas, new Rectangle(new Point(spriteSize.X * x, spriteSize.Y * y), spriteSize));
                    frameIndex++;
                }

            return output;
        }
    }
}
