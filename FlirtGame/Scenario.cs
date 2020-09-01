using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using Screen = FlirtGame.Screen;
using Coroutine = System.Collections.IEnumerator;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace FlirtGame
{
    public class Scenario
    {
        public static Scenario FromString(string source)
        {
            var output = new Scenario();
            XmlDocument document = new XmlDocument();
            source = $"<root> {source} </root>";
            document.LoadXml(source);

            output.loadedScenes = new Dictionary<string, Screen>();
            foreach (XmlNode node in document.FirstChild.ChildNodes)
                output.loadedScenes.Add(node.Attributes["id"].Value, new Screen(output, node));

            output.currentScreen = output.GetScreen("A");
            output.currentProcess = output.currentScreen.commands.Dequeue().ExecutionProcess();

            return output;
        }
        private Dictionary<string, Screen> loadedScenes;
        public Screen GetScreen(string name)
        {
            try
            {
                return loadedScenes[name];
            }
            catch (Exception)
            {
                throw new Exception($"could not find screen named {name}");
            }
        }
        private Screen _currentScreen;
        public Screen currentScreen
        {
            get => _currentScreen;
            set
            {
                _currentScreen = value;
                _currentScreen.Reset();
            }
        }
        private Coroutine currentProcess;

        public void Update()
        {
            if (!currentProcess.MoveNext())
                if(currentScreen.commands.Count >0)
                    currentProcess = currentScreen.commands.Dequeue().ExecutionProcess(); 
        }
    }

    public class Screen
    {
        public Screen(Scenario scenario, XmlNode source)
        {
            if (source.Name.ToLower() != "screen") throw new Exception($"misplaced command {source.Name}");

            this.scenario = scenario;
            this.ID = source.Attributes["id"].Value;

            commands = new Queue<Command>();

            for (int i = 0; i < source.ChildNodes.Count; i++)
                commands.Enqueue(Command.ParseCommand(this, source.ChildNodes[i]));

            _commandsBackup = commands.ToArray();
        }

        public void Reset() => commands = new Queue<Command>(_commandsBackup);

        public Scenario scenario;
        public string ID;
        private Command[] _commandsBackup;
        public Queue<Command> commands;
    }

    public abstract class Command
    {
        public static Command ParseCommand(Screen screen,XmlNode source)
        {
            switch (source.Name.ToLower())
            {
                case "message": return new Message(screen,source);
                case "m": return new Message(screen,source);
                case "setscreen": return new SetScreen(screen,source);
                case "question": return new Question(screen,source);
                case "q": return new Question(screen,source);
                default: throw new Exception($"unrecognized command {source.Name}");
            }
        }
        public Screen Screen { get; set; }
        public abstract Coroutine ExecutionProcess();
    }
    public class Message : Command
    {
        static Color bgColor = new Color(77,77,77,255);
        static Color highlightedColor = new Color(100,100,100,255);

        public Message(Screen screen,XmlNode source)
        {
            this.Screen = screen;
            bodyText = source.InnerText;
            this.iconName = source.Attributes["icon"] == null ? "" : source.Attributes["icon"].Value;
        }
        private string bodyText;
        private string iconName;

        public override Coroutine ExecutionProcess()
        { 
            var container = new Container(GamePlayScreen.Canvas, new Vector2(8, GamePlayScreen.Canvas.Area.Size.Y));
            var messagebox = new MessageBox(container,ContentLibrary.GetSprite(SpriteList.messageBoxSmall),new Point(33*8,4*8),new Point(100*8,24*8 + 4),new[] { bodyText },"press to continue");
            messagebox.TintColor = bgColor;
            var icon = new Image(container, ContentLibrary.GetSprite(iconName), Vector2.Zero, 8,SpriteEffects.FlipHorizontally);


            float targetOffset = 32*8 + 20;
            int time = 7;
            for (int i = 0; i < time; i++)
            {
                foreach (var c in GamePlayScreen.Canvas.Children)
                {
                    c.Position -= new Vector2(0, targetOffset / time);
                }
                yield return 0;
            }

            bool locked = true;
                                                                                                                                                                                                                                                                                       
            messagebox.OnJustPressed = () => messagebox.TintColor = highlightedColor;
            messagebox.OnReleased += () => locked = false;
            messagebox.OnReleased += () => messagebox.TintColor = bgColor;
            messagebox.OnReleased += () => messagebox.ShowBottomText = false;

            while (locked) { yield return 0; }

            messagebox.Active = false;
        }
    }
    public class SetScreen : Command
    {
        public SetScreen(Screen screen,XmlNode source)
        {
            this.Screen = screen;
            this.targetScreen = source.Attributes["target"].Value;
        }
        string targetScreen;
        public override Coroutine ExecutionProcess()
        {
            Screen.scenario.currentScreen = Screen.scenario.GetScreen(targetScreen);
            yield return 0;
        }
    }
    public class Question : Command
    {
        public Question(Screen screen,XmlNode source)
        {
            this.Screen = screen;

            responses = new List<Response>();
            foreach (XmlNode n in source.ChildNodes)
                responses.Add(new Response(n));

            question = source.Attributes["question"].Value;
        }
        private string question;
        List<Response> responses;

        public override Coroutine ExecutionProcess()
        {
            int targetOffset = 0;
            var container = new Container(GamePlayScreen.Canvas, new Vector2((1080 - 120 * 8) / 2, GamePlayScreen.Canvas.Area.Size.Y));
            var questionBox = new MessageBox(container, ContentLibrary.GetSprite(SpriteList.questionMessageBox), Point.Zero, new Point(120*8, 16*8), new[] { question }, "");
            questionBox.TintColor = new Color(77, 77, 77, 255);
            targetOffset += questionBox.Size.Y + 10;

            bool locked = true;
            Response chosenResponse = null;
            for (int i = 0; i < responses.Count; i++)
            {
                var r = responses[i];
                var responseBox = new MessageBox(container, ContentLibrary.GetSprite(SpriteList.choiceMessageBox), new Point(0, targetOffset), new Point(120 * 8, 16 * 8), new[] { $" {i+1}   {r.ResponseText}" }, "");
                responseBox.TintColor = Color.MediumBlue;
                targetOffset += responseBox.Size.Y + 10;
                responseBox.OnJustPressed += () => responseBox.TintColor = new Color(0, 0, 215);
                responseBox.OnReleased += () => responseBox.TintColor = Color.MediumBlue;
                responseBox.OnReleased += () => locked = false;
                responseBox.OnReleased += () => chosenResponse = r;
                responseBox.OnReleased += () => { if (r.Target.Length > 0) Screen.scenario.currentScreen = Screen.scenario.GetScreen(r.Target); };
            }

            //show the container:
            targetOffset += 20;
            int time = 10;
            for (int i = 0; i < time; i++)
            {
                foreach (var c in GamePlayScreen.Canvas.Children)
                    c.Position -= new Vector2(0, targetOffset / time);
                yield return 0;
            }

            //wait until the player chooses:
            while (locked) yield return 0;

            //retract the container:
            time = 7;
            for (int i = 0; i < time; i++)
            {
                foreach (var c in GamePlayScreen.Canvas.Children)
                    c.Position += new Vector2(0, targetOffset / time);
                yield return 0;
            }
            container.DestroyChilderen();

            targetOffset = 0;
            //show the response
            var messagebox = new MessageBox(container,ContentLibrary.GetSprite(SpriteList.messageBoxSmall),new Point(0,4*8),new Point(100 * 8,24*8 + 4),new[] { chosenResponse.ResponseText },"");
            messagebox.TintColor = Color.MediumBlue;
            var icon = new Image(container, ContentLibrary.GetSprite(chosenResponse.IconName), new Vector2(101*8,0), 8,SpriteEffects.None);
            targetOffset = 32 * 8 + 20;

            container.Position = new Vector2((1080 - (32 + 1 + 100) * 8) / 2, container.Position.Y);

            for (int i = 0; i < time; i++)
            {
                foreach (var c in GamePlayScreen.Canvas.Children)
                    c.Position -= new Vector2(0, targetOffset / time);
                yield return 0;
            }

            for (int i = 0; i < 10; i++) yield return 0; //wait for 10 frames. to make the conversation feel natural
        }
        public class Response
        {
            public Response(XmlNode source)
            {
                ResponseText = source.InnerText;
                Target = source.Attributes["target"] == null ? "": source.Attributes["target"].Value;
                IconName = source.Attributes["icon"] == null ? "" : source.Attributes["icon"].Value;
            }
            public string ResponseText { get; set; }
            public string Target { get; set; }
            public string IconName { get; set; }
        }
    }
}
