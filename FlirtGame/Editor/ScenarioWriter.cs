using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Engine;
using Engine.UI;
using FlirtGame;
using UI = Engine.UI;

namespace FlirtGame.Editor
{
    public class ScenarioWriter
    {

    }
    public class ScreenWriter
    {
        public List<ScreenElement> Elements;
        public Container Container { get; set; }

        public ScreenWriter()
        {

        }
    }
    public abstract class ScreenElement
    {
        public ScreenWriter Parent { get; set; }
        public int PositionInParent { get; set; }
        public abstract UI.Container Draw();
        public abstract string Serialize();
    }
}
