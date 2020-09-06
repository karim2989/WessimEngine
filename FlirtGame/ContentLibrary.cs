using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Engine;

namespace FlirtGame
{
    public enum XmlFile
    {
        game1,
        ListSize
    }
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
        choiceMessageBoxLarge,
        questionMessageBox,

        person1_N_Icon,
        person1_H_Icon,
        person1_A_Icon,
        person1_S_Icon,
        person2_N_Icon,
        person2_H_Icon,
        person2_A_Icon,
        person2_S_Icon,
        person3_N_Icon,
        person3_H_Icon,
        person3_A_Icon,
        person3_S_Icon,
        person4_N_Icon,
        person4_H_Icon,
        person4_A_Icon,
        person4_S_Icon,
        person5_N_Icon,
        person5_H_Icon,
        person5_A_Icon,
        person5_S_Icon,
        person6_N_Icon,
        person6_H_Icon,
        person6_A_Icon,
        person6_S_Icon,
        person7_N_Icon,
        person7_H_Icon,
        person7_A_Icon,
        person7_S_Icon,

        person1_N_LowerFace,
        person1_H_LowerFace,
        person1_A_LowerFace,
        person1_S_LowerFace,
        person2_N_LowerFace,
        person2_H_LowerFace,
        person2_A_LowerFace,
        person2_S_LowerFace,
        person3_N_LowerFace,
        person3_H_LowerFace,
        person3_A_LowerFace,
        person3_S_LowerFace,
        person4_N_LowerFace,
        person4_H_LowerFace,
        person4_A_LowerFace,
        person4_S_LowerFace,
        person5_N_LowerFace,
        person5_H_LowerFace,
        person5_A_LowerFace,
        person5_S_LowerFace,
        person6_N_LowerFace,
        person6_H_LowerFace,
        person6_A_LowerFace,
        person6_S_LowerFace,
        person7_N_LowerFace,
        person7_H_LowerFace,
        person7_A_LowerFace,
        person7_S_LowerFace,

        person1_N_UpperFace,
        person1_H_UpperFace,
        person1_A_UpperFace,
        person1_S_UpperFace,
        person2_N_UpperFace,
        person2_H_UpperFace,
        person2_A_UpperFace,
        person2_S_UpperFace,
        person3_N_UpperFace,
        person3_H_UpperFace,
        person3_A_UpperFace,
        person3_S_UpperFace,
        person4_N_UpperFace,
        person4_H_UpperFace,
        person4_A_UpperFace,
        person4_S_UpperFace,
        person5_N_UpperFace,
        person5_H_UpperFace,
        person5_A_UpperFace,
        person5_S_UpperFace,
        person6_N_UpperFace,
        person6_H_UpperFace,
        person6_A_UpperFace,
        person6_S_UpperFace,
        person7_N_UpperFace,
        person7_H_UpperFace,
        person7_A_UpperFace,
        person7_S_UpperFace,

        person1_body,
        person2_body,
        person3_body,
        person4_body,
        person5_body,
        person6_body,
        person7_body,

        env_bar1,
        env_school,

        ListSize
    }

    public enum SoundList
    {
        ba,
        au,
        ta,
        oy,

        jump,

        ListSize
    }

    public enum TheaterCharactersList
    {
        character1,
        character2,
        character3,
        character4,
        character5,
        character6,
        character7,
        ListSize
    }

    public static class ContentLibrary
    {
        private static string[] loadedXmlFiles;
        public static string GetXml(XmlFile f) => loadedXmlFiles[(int)f];
        public static void loadXml(ContentManager content)
        {
            loadedXmlFiles = new string[(int)XmlFile.ListSize];

            loadedXmlFiles[(int)XmlFile.game1] = System.IO.File.ReadAllText("Content/game.xml");
        }

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
            loadedSprites[(int)SpriteList.messageBoxMedium]     = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), Point.Zero, new Point(100, 32));
            loadedSprites[(int)SpriteList.messageBoxSmall]      = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(0,32), new Point(100, 24));
            loadedSprites[(int)SpriteList.choiceMessageBox]     = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(0,56), new Point(120, 16));
            loadedSprites[(int)SpriteList.choiceMessageBoxLarge]= TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(0,88), new Point(120, 24));
            loadedSprites[(int)SpriteList.questionMessageBox]   = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(0,72), new Point(120, 16));

            loadedSprites[(int)SpriteList.person1_N_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 0), new Point(32, 32));
            loadedSprites[(int)SpriteList.person1_H_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 0), new Point(32, 32));
            loadedSprites[(int)SpriteList.person1_S_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 0), new Point(32, 32));
            loadedSprites[(int)SpriteList.person1_A_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 0), new Point(32, 32));

            loadedSprites[(int)SpriteList.person2_N_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 32), new Point(32, 32));
            loadedSprites[(int)SpriteList.person2_H_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 32), new Point(32, 32));
            loadedSprites[(int)SpriteList.person2_S_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 32), new Point(32, 32));
            loadedSprites[(int)SpriteList.person2_A_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 32), new Point(32, 32));

            loadedSprites[(int)SpriteList.person3_N_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 64), new Point(32, 32));
            loadedSprites[(int)SpriteList.person3_H_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 64), new Point(32, 32));
            loadedSprites[(int)SpriteList.person3_S_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 64), new Point(32, 32));
            loadedSprites[(int)SpriteList.person3_A_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 64), new Point(32, 32));

            loadedSprites[(int)SpriteList.person4_N_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 96), new Point(32, 32));
            loadedSprites[(int)SpriteList.person4_H_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 96), new Point(32, 32));
            loadedSprites[(int)SpriteList.person4_S_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 96), new Point(32, 32));
            loadedSprites[(int)SpriteList.person4_A_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 96), new Point(32, 32));

            loadedSprites[(int)SpriteList.person5_N_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 32 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person5_H_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 32 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person5_S_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 32 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person5_A_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 32 + 128), new Point(32, 32));
                                                                                                                                                       
            loadedSprites[(int)SpriteList.person6_N_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 64 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person6_H_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 64 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person6_S_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 64 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person6_A_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 64 + 128), new Point(32, 32));
                                                                                                                                                       
            loadedSprites[(int)SpriteList.person7_N_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384, 96 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person7_H_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416, 96 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person7_S_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448, 96 + 128), new Point(32, 32));
            loadedSprites[(int)SpriteList.person7_A_Icon] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480, 96 + 128), new Point(32, 32));

            loadedSprites[(int)SpriteList.person1_N_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 0), new Point(32,  19));
            loadedSprites[(int)SpriteList.person1_H_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 0), new Point(32,  19));
            loadedSprites[(int)SpriteList.person1_S_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 0), new Point(32,  19));
            loadedSprites[(int)SpriteList.person1_A_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 0), new Point(32,  19));
                                                                                                                                                                                  
            loadedSprites[(int)SpriteList.person2_N_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 32), new Point(32, 19));
            loadedSprites[(int)SpriteList.person2_H_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 32), new Point(32, 19));
            loadedSprites[(int)SpriteList.person2_S_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 32), new Point(32, 19));
            loadedSprites[(int)SpriteList.person2_A_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 32), new Point(32, 19));
                                                                                                                                                                                  
            loadedSprites[(int)SpriteList.person3_N_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 64), new Point(32, 19));
            loadedSprites[(int)SpriteList.person3_H_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 64), new Point(32, 19));
            loadedSprites[(int)SpriteList.person3_S_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 64), new Point(32, 19));
            loadedSprites[(int)SpriteList.person3_A_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 64), new Point(32, 19));
                                                                                                                                                                                  
            loadedSprites[(int)SpriteList.person4_N_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 96), new Point(32, 19));
            loadedSprites[(int)SpriteList.person4_H_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 96), new Point(32, 19));
            loadedSprites[(int)SpriteList.person4_S_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 96), new Point(32, 19));
            loadedSprites[(int)SpriteList.person4_A_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 96), new Point(32, 19));
                                                                                                                                                                                  
            loadedSprites[(int)SpriteList.person5_N_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 128+32), new Point(32, 19));
            loadedSprites[(int)SpriteList.person5_H_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 128+32), new Point(32, 19));
            loadedSprites[(int)SpriteList.person5_S_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 128+32), new Point(32, 19));
            loadedSprites[(int)SpriteList.person5_A_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 128+32), new Point(32, 19));
                                                                                                                                                               
            loadedSprites[(int)SpriteList.person6_N_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 128+64), new Point(32, 19));
            loadedSprites[(int)SpriteList.person6_H_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 128+64), new Point(32, 19));
            loadedSprites[(int)SpriteList.person6_S_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 128+64), new Point(32, 19));
            loadedSprites[(int)SpriteList.person6_A_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 128+64), new Point(32, 19));
                                                                                                                                                               
            loadedSprites[(int)SpriteList.person7_N_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 128+96), new Point(32, 19));
            loadedSprites[(int)SpriteList.person7_H_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 128+96), new Point(32, 19));
            loadedSprites[(int)SpriteList.person7_S_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 128+96), new Point(32, 19));
            loadedSprites[(int)SpriteList.person7_A_UpperFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 128+96), new Point(32, 19));

            loadedSprites[(int)SpriteList.person1_N_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 0 + 19), new Point(32,  32 - 19));
            loadedSprites[(int)SpriteList.person1_H_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 0 + 19), new Point(32,  32 - 19));
            loadedSprites[(int)SpriteList.person1_S_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 0 + 19), new Point(32,  32 - 19));
            loadedSprites[(int)SpriteList.person1_A_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 0 + 19), new Point(32,  32 - 19));
                                                                                                                                                                                  
            loadedSprites[(int)SpriteList.person2_N_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 32 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person2_H_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 32 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person2_S_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 32 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person2_A_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 32 + 19), new Point(32, 32 - 19));
                                                                                                                                                                                   
            loadedSprites[(int)SpriteList.person3_N_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 64 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person3_H_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 64 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person3_S_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 64 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person3_A_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 64 + 19), new Point(32, 32 - 19));
                                                                                                                                                                                   
            loadedSprites[(int)SpriteList.person4_N_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 96 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person4_H_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 96 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person4_S_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 96 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person4_A_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 96 + 19), new Point(32, 32 - 19));
            
                                                                                                                                                                                  
            loadedSprites[(int)SpriteList.person5_N_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 32 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person5_H_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 32 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person5_S_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 32 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person5_A_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 32 + 128 + 19), new Point(32, 32 - 19));
                                                                                                                                                                                    
            loadedSprites[(int)SpriteList.person6_N_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 64 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person6_H_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 64 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person6_S_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 64 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person6_A_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 64 + 128 + 19), new Point(32, 32 - 19));
                                                                                                                                                                                    
            loadedSprites[(int)SpriteList.person7_N_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(384 - 128, 96 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person7_H_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(416 - 128, 96 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person7_S_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(448 - 128, 96 + 128 + 19), new Point(32, 32 - 19));
            loadedSprites[(int)SpriteList.person7_A_LowerFace] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(480 - 128, 96 + 128 + 19), new Point(32, 32 - 19));

            loadedSprites[(int)SpriteList.person1_body] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(256 + 0, 128), new Point(32,32));
            loadedSprites[(int)SpriteList.person4_body] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(256 + 32, 128), new Point(32,32));
            loadedSprites[(int)SpriteList.person2_body] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(256 + 64, 128), new Point(32,32));
            loadedSprites[(int)SpriteList.person3_body] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(256 + 96, 128), new Point(32,32));
            loadedSprites[(int)SpriteList.person5_body] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(256 + 128, 128), new Point(32,32));
            loadedSprites[(int)SpriteList.person7_body] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(256 + 160, 128), new Point(32,32));
            loadedSprites[(int)SpriteList.person6_body] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(256 + 192, 128), new Point(32,32));
            
            loadedSprites[(int)SpriteList.env_bar1] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(320, 440), new Point(192, 72));
            loadedSprites[(int)SpriteList.env_school] = TileSplitter.SplitSprite(graphicsDevice, GetTexture(TextureList.atlas1), new Point(320-192, 440), new Point(192, 72));
        }

        private static SoundEffect[] loadedSoundEffects;
        public static SoundEffect GetSound(SoundList l) => loadedSoundEffects[(int)l];
        public static void LoadSounds(ContentManager content)
        {
            loadedSoundEffects = new SoundEffect[(int)SoundList.ListSize];

            loadedSoundEffects[(int)SoundList.ba] = content.Load<SoundEffect>("ba");
            loadedSoundEffects[(int)SoundList.au] = content.Load<SoundEffect>("au");
            loadedSoundEffects[(int)SoundList.ta] = content.Load<SoundEffect>("ta");
            loadedSoundEffects[(int)SoundList.oy] = content.Load<SoundEffect>("oy");

            loadedSoundEffects[(int)SoundList.jump] = content.Load<SoundEffect>("jump");
        }

        private static TheaterCharacter[] loadedTheaterCharacters;
        public static TheaterCharacter GetTheaterCharacter(TheaterCharactersList l) => loadedTheaterCharacters[(int)l];
        public static void LoadTheaterCharacter(Scenario scenario, GraphicsDevice graphicsDevice)
        {
            loadedTheaterCharacters = new TheaterCharacter[(int)TheaterCharactersList.ListSize];
            loadedTheaterCharacters[(int)TheaterCharactersList.character1] = new TheaterCharacter(scenario.CharacterNames[0], GetSprite(SpriteList.person1_body))
            {
                LowerHeadSpriteA = GetSprite(SpriteList.person1_A_LowerFace),
                LowerHeadSpriteH = GetSprite(SpriteList.person1_H_LowerFace),
                LowerHeadSpriteN = GetSprite(SpriteList.person1_N_LowerFace),
                LowerHeadSpriteS = GetSprite(SpriteList.person1_S_LowerFace),
                UpperHeadSpriteA = GetSprite(SpriteList.person1_A_UpperFace),
                UpperHeadSpriteH = GetSprite(SpriteList.person1_H_UpperFace),
                UpperHeadSpriteN = GetSprite(SpriteList.person1_N_UpperFace),
                UpperHeadSpriteS = GetSprite(SpriteList.person1_S_UpperFace),
                HeadOffset = 5,
                TalkSound = GetSound(SoundList.au).CreateInstance()
            };

            loadedTheaterCharacters[(int)TheaterCharactersList.character2] = new TheaterCharacter(scenario.CharacterNames[1], GetSprite(SpriteList.person2_body))
            {
                LowerHeadSpriteA = GetSprite(SpriteList.person2_A_LowerFace),
                LowerHeadSpriteH = GetSprite(SpriteList.person2_H_LowerFace),
                LowerHeadSpriteN = GetSprite(SpriteList.person2_N_LowerFace),
                LowerHeadSpriteS = GetSprite(SpriteList.person2_S_LowerFace),
                UpperHeadSpriteA = GetSprite(SpriteList.person2_A_UpperFace),
                UpperHeadSpriteH = GetSprite(SpriteList.person2_H_UpperFace),
                UpperHeadSpriteN = GetSprite(SpriteList.person2_N_UpperFace),
                UpperHeadSpriteS = GetSprite(SpriteList.person2_S_UpperFace),
                HeadOffset = 2,
                TalkSound = GetSound(SoundList.oy).CreateInstance()
            };

            loadedTheaterCharacters[(int)TheaterCharactersList.character3] = new TheaterCharacter(scenario.CharacterNames[2], GetSprite(SpriteList.person3_body))
            {
                LowerHeadSpriteA = GetSprite(SpriteList.person3_A_LowerFace),
                LowerHeadSpriteH = GetSprite(SpriteList.person3_H_LowerFace),
                LowerHeadSpriteN = GetSprite(SpriteList.person3_N_LowerFace),
                LowerHeadSpriteS = GetSprite(SpriteList.person3_S_LowerFace),
                UpperHeadSpriteA = GetSprite(SpriteList.person3_A_UpperFace),
                UpperHeadSpriteH = GetSprite(SpriteList.person3_H_UpperFace),
                UpperHeadSpriteN = GetSprite(SpriteList.person3_N_UpperFace),
                UpperHeadSpriteS = GetSprite(SpriteList.person3_S_UpperFace),
                HeadOffset = 1,
                TalkSound = GetSound(SoundList.ba).CreateInstance()
            };

            loadedTheaterCharacters[(int)TheaterCharactersList.character4] = new TheaterCharacter(scenario.CharacterNames[3], GetSprite(SpriteList.person4_body))
            {
                LowerHeadSpriteA = GetSprite(SpriteList.person4_A_LowerFace),
                LowerHeadSpriteH = GetSprite(SpriteList.person4_H_LowerFace),
                LowerHeadSpriteN = GetSprite(SpriteList.person4_N_LowerFace),
                LowerHeadSpriteS = GetSprite(SpriteList.person4_S_LowerFace),
                UpperHeadSpriteA = GetSprite(SpriteList.person4_A_UpperFace),
                UpperHeadSpriteH = GetSprite(SpriteList.person4_H_UpperFace),
                UpperHeadSpriteN = GetSprite(SpriteList.person4_N_UpperFace),
                UpperHeadSpriteS = GetSprite(SpriteList.person4_S_UpperFace),
                HeadOffset = 0,
                TalkSound = GetSound(SoundList.ba).CreateInstance()
            };

            loadedTheaterCharacters[(int)TheaterCharactersList.character5] = new TheaterCharacter(scenario.CharacterNames[4], GetSprite(SpriteList.person5_body))
            {
                LowerHeadSpriteA = GetSprite(SpriteList.person5_A_LowerFace),
                LowerHeadSpriteH = GetSprite(SpriteList.person5_H_LowerFace),
                LowerHeadSpriteN = GetSprite(SpriteList.person5_N_LowerFace),
                LowerHeadSpriteS = GetSprite(SpriteList.person5_S_LowerFace),
                UpperHeadSpriteA = GetSprite(SpriteList.person5_A_UpperFace),
                UpperHeadSpriteH = GetSprite(SpriteList.person5_H_UpperFace),
                UpperHeadSpriteN = GetSprite(SpriteList.person5_N_UpperFace),
                UpperHeadSpriteS = GetSprite(SpriteList.person5_S_UpperFace),
                HeadOffset = 1,
                TalkSound = GetSound(SoundList.ba).CreateInstance()
            };

            loadedTheaterCharacters[(int)TheaterCharactersList.character6] = new TheaterCharacter(scenario.CharacterNames[5], GetSprite(SpriteList.person6_body))
            {
                LowerHeadSpriteA = GetSprite(SpriteList.person6_A_LowerFace),
                LowerHeadSpriteH = GetSprite(SpriteList.person6_H_LowerFace),
                LowerHeadSpriteN = GetSprite(SpriteList.person6_N_LowerFace),
                LowerHeadSpriteS = GetSprite(SpriteList.person6_S_LowerFace),
                UpperHeadSpriteA = GetSprite(SpriteList.person6_A_UpperFace),
                UpperHeadSpriteH = GetSprite(SpriteList.person6_H_UpperFace),
                UpperHeadSpriteN = GetSprite(SpriteList.person6_N_UpperFace),
                UpperHeadSpriteS = GetSprite(SpriteList.person6_S_UpperFace),
                HeadOffset = 3,
                TalkSound = GetSound(SoundList.ba).CreateInstance()
            };

            loadedTheaterCharacters[(int)TheaterCharactersList.character7] = new TheaterCharacter(scenario.CharacterNames[6], GetSprite(SpriteList.person7_body))
            {
                LowerHeadSpriteA = GetSprite(SpriteList.person7_A_LowerFace),
                LowerHeadSpriteH = GetSprite(SpriteList.person7_H_LowerFace),
                LowerHeadSpriteN = GetSprite(SpriteList.person7_N_LowerFace),
                LowerHeadSpriteS = GetSprite(SpriteList.person7_S_LowerFace),
                UpperHeadSpriteA = GetSprite(SpriteList.person7_A_UpperFace),
                UpperHeadSpriteH = GetSprite(SpriteList.person7_H_UpperFace),
                UpperHeadSpriteN = GetSprite(SpriteList.person7_N_UpperFace),
                UpperHeadSpriteS = GetSprite(SpriteList.person7_S_UpperFace),
                HeadOffset = 0,
                TalkSound = GetSound(SoundList.ba).CreateInstance()
            };
                
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
                case "bar":case "env_bar": return GetSprite(SpriteList.env_bar1);
                case "school":case "env_school": return GetSprite(SpriteList.env_school);
                case "person1a": return GetSprite(SpriteList.person1_A_Icon);
                case "person1h": return GetSprite(SpriteList.person1_H_Icon);
                case "person1s": return GetSprite(SpriteList.person1_S_Icon);
                case "person1n": return GetSprite(SpriteList.person1_N_Icon);
                case "person2a": case "sabrinea": return GetSprite(SpriteList.person2_A_Icon);
                case "person2h": case "sabrineh": return GetSprite(SpriteList.person2_H_Icon);
                case "person2s": case "sabrines": return GetSprite(SpriteList.person2_S_Icon);
                case "person2n": case "sabrinen": return GetSprite(SpriteList.person2_N_Icon);
                case "person3a": case "wessima": return GetSprite(SpriteList.person3_A_Icon);
                case "person3h": case "wessimh": return GetSprite(SpriteList.person3_H_Icon);
                case "person3s": case "wessims": return GetSprite(SpriteList.person3_S_Icon);
                case "person3n": case "wessimn": return GetSprite(SpriteList.person3_N_Icon);
                case "person4a": return GetSprite(SpriteList.person4_A_Icon);
                case "person4h": return GetSprite(SpriteList.person4_H_Icon);
                case "person4s": return GetSprite(SpriteList.person4_S_Icon);
                case "person4n": return GetSprite(SpriteList.person4_N_Icon);
                case "person5a": return GetSprite(SpriteList.person5_A_Icon);
                case "person5h": return GetSprite(SpriteList.person5_H_Icon);
                case "person5s": return GetSprite(SpriteList.person5_S_Icon);
                case "person5n": return GetSprite(SpriteList.person5_N_Icon);
                case "person6a": return GetSprite(SpriteList.person6_A_Icon);
                case "person6h": return GetSprite(SpriteList.person6_H_Icon);
                case "person6s": return GetSprite(SpriteList.person6_S_Icon);
                case "person6n": return GetSprite(SpriteList.person6_N_Icon);
                case "person7a": return GetSprite(SpriteList.person7_A_Icon);
                case "person7h": return GetSprite(SpriteList.person7_H_Icon);
                case "person7s": return GetSprite(SpriteList.person7_S_Icon);
                case "person7n": return GetSprite(SpriteList.person7_N_Icon);
                default: throw new Exception($"could not find a sprite named {name}");
            }
        }
    }
}
