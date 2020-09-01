using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Engine;

namespace FlirtGame
{
    public enum FontList
    {
        Sans_serif,
        Sans_serif_large,
        ListSize
    }
    public enum TextureList
    {
        atlas1,
        ListSize
    }
    public enum SpriteList
    {
        messageBoxSmall,
        messageBoxMedium,
        choiceMessageBox,
        questionMessageBox,
        person1_N,
        person1_H,
        person1_A,
        person1_S,
        person2_N,
        person2_H,
        person2_A,
        person2_S,
        person3_N,
        person3_H,
        person3_A,
        person3_S,
        ListSize
    }
    public static class ContentLibrary
    {
        private static SpriteFont[] loadedFonts;
        public static SpriteFont GetFont(FontList f) => loadedFonts[(int)f];
        public static void LoadFont(ContentManager contentManager)
        {
            loadedFonts = new SpriteFont[(int)FontList.ListSize];
            loadedFonts[(int)FontList.Sans_serif] = contentManager.Load<SpriteFont>("Sans-serif");
            loadedFonts[(int)FontList.Sans_serif_large] = contentManager.Load<SpriteFont>("robotolarge");
        }
        private static Texture2D[] loadedTexture;
        public static Texture2D GetTexture(TextureList t) => loadedTexture[(int)t];
        public static void LoadedTexture(ContentManager contentManager,GraphicsDevice graphicsDevice)
        {
            loadedTexture = new Texture2D[(int)TextureList.ListSize];
            loadedTexture[(int)TextureList.atlas1] = contentManager.Load<Texture2D>("FlirtUITileset");
        }
        private static Sprite[] loadedSprites;
        public static Sprite GetSprite(SpriteList l) => loadedSprites[(int)l];
        public static void LoadSprites(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            loadedSprites = new Sprite[(int)SpriteList.ListSize];
            loadedSprites[(int)SpriteList.messageBoxMedium] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), Point.Zero, new Point(100, 32));
            loadedSprites[(int)SpriteList.messageBoxSmall] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(0,32), new Point(100, 24));
            loadedSprites[(int)SpriteList.choiceMessageBox] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(0,56), new Point(120, 16));
            loadedSprites[(int)SpriteList.questionMessageBox] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(0,72), new Point(120, 16));

            loadedSprites[(int)SpriteList.person1_N] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 0), new Point(32, 32));
            loadedSprites[(int)SpriteList.person1_H] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 0), new Point(32, 32));
            loadedSprites[(int)SpriteList.person1_S] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 0), new Point(32, 32));
            loadedSprites[(int)SpriteList.person1_A] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 0), new Point(32, 32));

            loadedSprites[(int)SpriteList.person2_N] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 32), new Point(32, 32));
            loadedSprites[(int)SpriteList.person2_H] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 32), new Point(32, 32));
            loadedSprites[(int)SpriteList.person2_S] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 32), new Point(32, 32));
            loadedSprites[(int)SpriteList.person2_A] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 32), new Point(32, 32));

            loadedSprites[(int)SpriteList.person3_N] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 64), new Point(32, 32));
            loadedSprites[(int)SpriteList.person3_H] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 64), new Point(32, 32));
            loadedSprites[(int)SpriteList.person3_S] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 64), new Point(32, 32));
            loadedSprites[(int)SpriteList.person3_A] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 64), new Point(32, 32));
        }

        public static Sprite GetSprite(string name)
        {
            if (name == null || name.Length < 1) throw new Exception("sprite name was not assigned");

            name = name.ToLower();
            name = name.Replace("happy", "h");
            name = name.Replace("angry", "a");
            name = name.Replace("sad" , "s");
            name = name.Replace("neutral", "n");

            name = name.Replace(" ", "");// remove spaces;

            switch (name)
            {
                case "person1a": return GetSprite(SpriteList.person1_A);
                case "person1h": return GetSprite(SpriteList.person1_H);
                case "person1s": return GetSprite(SpriteList.person1_S);
                case "person1n": return GetSprite(SpriteList.person1_N);
                case "person2a": case "sabrinea": return GetSprite(SpriteList.person2_A);
                case "person2h": case "sabrineh": return GetSprite(SpriteList.person2_H);
                case "person2s": case "sabrines": return GetSprite(SpriteList.person2_S);
                case "person2n": case "sabrinen": return GetSprite(SpriteList.person2_N);
                case "person3a": case "wessima": return GetSprite(SpriteList.person3_A);
                case "person3h": case "wessimh": return GetSprite(SpriteList.person3_H);
                case "person3s": case "wessims": return GetSprite(SpriteList.person3_S);
                case "person3n": case "wessimn": return GetSprite(SpriteList.person3_N);
                default: throw new Exception($"could not find a sprite named {name}");
            }
        }
    }
}
