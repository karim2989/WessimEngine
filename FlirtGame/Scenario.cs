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
            {
                if (node.Name.ToLower() == "screen") output.loadedScenes.Add(node.Attributes["id"].Value, new Screen(output, node));
                if(node.Name.ToLower() == "mapnames") output.CharacterNames = node.InnerText.Split(';');
            }

            output.currentScreen = output.GetScreen("A");
            output.currentProcess = output.currentScreen.commands.Dequeue().ExecutionProcess();

            return output;
        }

        public Dictionary<string, string> Variables = new Dictionary<string, string>();
        public string[] CharacterNames = new string[4] { "mi7riz","sabrine","wessim","mi7irziya" };
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
                if (value.ID == "A")
                    GamePlayScreen.Canvas.DestroyChildren();
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
                case "setemotion": return new SetEmotion(screen,source);
                case "setvariable": return new SetVar(screen, source);
                case "setvar": return new SetVar(screen, source);
                case "question": return new Question(screen,source);
                case "q": return new Question(screen,source);
                case "fullscreenmessage": return new FullscrrenMessage(screen,source);
                default: throw new Exception($"unrecognized command {source.Name}");
            }
        }
        public Screen Screen { get; set; }
        public abstract Coroutine ExecutionProcess();

        protected string[] segmentateMessage(string unProcessedText,int segmentLength)
        {
            unProcessedText = $"{unProcessedText} ";
            List<string> output = new List<string>();
            while (unProcessedText.Length > 0)
            {
                for (int i = Math.Min(segmentLength, unProcessedText.Length) - 1; i >= 0; i--)
                {
                    if(unProcessedText[i] == ' ' || char.IsPunctuation(unProcessedText[i]))
                    {
                        output.Add(unProcessedText.Substring(0, i + 1));
                        unProcessedText = unProcessedText.Substring(i+1);
                        i = 0;
                    }
                }
            }
            return output.ToArray();
        }
    }
    
    public class Message : Command
    {
        static Color bgColor = new Color(77,77,77,255);
        static Color highlightedColor = new Color(100,100,100,255);

        public Message(Screen screen,XmlNode source)
        {
            this.Screen = screen;
            bodyText = source.InnerText;

            this.waitForPress = source.Attributes["wait"] == null ? true : source.Attributes["wait"].Value != "no";
            this.iconName = source.Attributes["icon"] == null ? "" : source.Attributes["icon"].Value;

            iconName = iconName.ToLower();

            for (int i = 0; i < screen.scenario.CharacterNames.Length; i++)
                iconName = iconName.Replace($"{screen.scenario.CharacterNames[i]} ", $"person{i+1}");

            iconName = iconName.Replace(" ", "");
            iconName = iconName.Replace("lvl1", "");
            iconName = iconName.Replace("lvl2", "");
        }
        private bool waitForPress;
        private string bodyText;
        private string iconName;

        public override Coroutine ExecutionProcess()
        { 
            foreach (var variable in Screen.scenario.Variables)
                bodyText = bodyText.Replace($"${variable.Key};", variable.Value);
            var container = new Container(GamePlayScreen.Canvas, new Vector2(8, GamePlayScreen.Canvas.Area.Size.Y));
            var messagebox = new MessageBox(container,ContentLibrary.GetSprite(SpriteList.messageBoxSmall),new Point(33*8,4*8),new Point(100*8,24*8 + 4), segmentateMessage(bodyText,28),"press to continue");
            messagebox.TintColor = bgColor;
            var icon = new Image(container, ContentLibrary.GetSprite(iconName), Vector2.Zero, 8,SpriteEffects.FlipHorizontally);
            if (iconName.Contains("person1")) { GamePlayScreen.Theater.TheaterCharacters[0].Talk(); GamePlayScreen.Theater.TheaterCharacters[0].SetEmotion(iconName.Replace("person1", "")[0]); }
            if (iconName.Contains("person2")) { GamePlayScreen.Theater.TheaterCharacters[1].Talk(); GamePlayScreen.Theater.TheaterCharacters[1].SetEmotion(iconName.Replace("person2", "")[0]); }
            if (iconName.Contains("person3")) { GamePlayScreen.Theater.TheaterCharacters[2].Talk(); GamePlayScreen.Theater.TheaterCharacters[2].SetEmotion(iconName.Replace("person3", "")[0]); }
            if (iconName.Contains("person4")) { GamePlayScreen.Theater.TheaterCharacters[3].Talk(); GamePlayScreen.Theater.TheaterCharacters[3].SetEmotion(iconName.Replace("person4", "")[0]); }
            if (iconName.Contains("person5")) { GamePlayScreen.Theater.TheaterCharacters[4].Talk(); GamePlayScreen.Theater.TheaterCharacters[4].SetEmotion(iconName.Replace("person5", "")[0]); }
            if (iconName.Contains("person6")) { GamePlayScreen.Theater.TheaterCharacters[5].Talk(); GamePlayScreen.Theater.TheaterCharacters[5].SetEmotion(iconName.Replace("person6", "")[0]); }
            if (iconName.Contains("person7")) { GamePlayScreen.Theater.TheaterCharacters[6].Talk(); GamePlayScreen.Theater.TheaterCharacters[6].SetEmotion(iconName.Replace("person7", "")[0]); }

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

            if (!waitForPress) messagebox.OnReleased();

            while (locked) yield return 0;

            messagebox.Active = false;
        }
    }
    public class SetVar : Command
    {
        public SetVar(Screen screen,XmlNode source)
        {
            Screen = screen;
            target = source.Attributes["target"].Value;
            value = source.Attributes["value"].Value;
        }

        string target;
        string value;

        public override Coroutine ExecutionProcess()
        {
            var vars = Screen.scenario.Variables;
            if (vars.ContainsKey(target))
                vars[target] = value;
            else
                vars.Add(target, value);
            yield return 0;
        }
    }
    public class SetEmotion : Command
    {
        public SetEmotion(Screen screen,XmlNode source)
        {
            Screen = screen;
            this.emotion = source.Attributes["emotion"] == null ? "" : source.Attributes["emotion"].Value;

            emotion = emotion.ToLower();

            for (int i = 0; i < screen.scenario.CharacterNames.Length; i++)
                emotion = emotion.Replace($"{screen.scenario.CharacterNames[i]} ", $"person{i+1}");

            emotion = emotion.Replace(" ", "");
        }
        string emotion;
        public override Coroutine ExecutionProcess()
        {
            if (emotion.Contains("person1")) GamePlayScreen.Theater.TheaterCharacters[0].SetEmotion(emotion.Replace("person1", "")[0]);
            if (emotion.Contains("person2")) GamePlayScreen.Theater.TheaterCharacters[1].SetEmotion(emotion.Replace("person2", "")[0]);
            if (emotion.Contains("person3")) GamePlayScreen.Theater.TheaterCharacters[2].SetEmotion(emotion.Replace("person3", "")[0]);
            if (emotion.Contains("person4")) GamePlayScreen.Theater.TheaterCharacters[3].SetEmotion(emotion.Replace("person4", "")[0]);
            yield return 0;
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
                responses.Add(new Response(screen,n));

            question = source.Attributes["question"] == null ? "" : source.Attributes["question"].Value;
        }
        private string question;
        List<Response> responses;

        public override Coroutine ExecutionProcess()
        {
            int targetOffset = 0;
            var container = new Container(GamePlayScreen.Canvas, new Vector2((1080 - 120 * 8) / 2, GamePlayScreen.Canvas.Area.Size.Y));
            var questionBox = new MessageBox(container, ContentLibrary.GetSprite(SpriteList.questionMessageBox), Point.Zero, new Point(120 * 8, 16 * 8), new[] { question }, "");
            questionBox.TintColor = new Color(77, 77, 77, 255);
            targetOffset += questionBox.Size.Y + 10;

            bool locked = true;
            Response chosenResponse = null;
            for (int i = 0; i < responses.Count; i++)
            {
                var r = responses[i];

                foreach (var variable in Screen.scenario.Variables)
                    r.ResponseText = r.ResponseText.Replace($"${variable.Key};", variable.Value);

                var responseBox = new MessageBox(container, ContentLibrary.GetSprite(SpriteList.choiceMessageBox), new Point(0, targetOffset), new Point(120 * 8, 16 * 8), segmentateMessage($" {i + 1}   {r.ResponseText}",40), "");
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
            var messagebox = new MessageBox(container, ContentLibrary.GetSprite(SpriteList.messageBoxSmall), new Point(0, 4 * 8), new Point(100 * 8, 24 * 8 + 4), new[] { chosenResponse.ResponseText }, "");
            messagebox.TintColor = Color.MediumBlue;
            var icon = new Image(container, ContentLibrary.GetSprite(chosenResponse.IconName), new Vector2(101 * 8, 0), 8, SpriteEffects.None);
            targetOffset = 32 * 8 + 20;

            container.Position = new Vector2((1080 - (32 + 1 + 100) * 8) / 2, container.Position.Y);

            for (int i = 0; i < time; i++)
            {
                foreach (var c in GamePlayScreen.Canvas.Children)
                    c.Position -= new Vector2(0, targetOffset / time);
                yield return 0;
            }

            var iconName = chosenResponse.IconName;
            if (iconName.Contains("person1")) { GamePlayScreen.Theater.TheaterCharacters[0].Talk(); GamePlayScreen.Theater.TheaterCharacters[0].SetEmotion(iconName.Replace("person1", "")[0]); }
            if (iconName.Contains("person2")) { GamePlayScreen.Theater.TheaterCharacters[1].Talk(); GamePlayScreen.Theater.TheaterCharacters[1].SetEmotion(iconName.Replace("person2", "")[0]); }
            if (iconName.Contains("person3")) { GamePlayScreen.Theater.TheaterCharacters[2].Talk(); GamePlayScreen.Theater.TheaterCharacters[2].SetEmotion(iconName.Replace("person3", "")[0]); }
            if (iconName.Contains("person4")) { GamePlayScreen.Theater.TheaterCharacters[3].Talk(); GamePlayScreen.Theater.TheaterCharacters[3].SetEmotion(iconName.Replace("person4", "")[0]); }
            if (iconName.Contains("person5")) { GamePlayScreen.Theater.TheaterCharacters[4].Talk(); GamePlayScreen.Theater.TheaterCharacters[4].SetEmotion(iconName.Replace("person5", "")[0]); }
            if (iconName.Contains("person6")) { GamePlayScreen.Theater.TheaterCharacters[5].Talk(); GamePlayScreen.Theater.TheaterCharacters[5].SetEmotion(iconName.Replace("person6", "")[0]); }
            if (iconName.Contains("person7")) { GamePlayScreen.Theater.TheaterCharacters[6].Talk(); GamePlayScreen.Theater.TheaterCharacters[6].SetEmotion(iconName.Replace("person7", "")[0]); }

            for (int i = 0; i < 60; i++) yield return 0; //wait for 60 frames. to make the conversation feel natural
        }
        public class Response
        {
            public Response(Screen screen,XmlNode source)
            {
                ResponseText = source.InnerText;
                Target = source.Attributes["target"] == null ? "": source.Attributes["target"].Value;
                IconName = source.Attributes["icon"] == null ? "" : source.Attributes["icon"].Value;

                IconName = IconName.ToLower();

                for (int i = 0; i < screen.scenario.CharacterNames.Length; i++)
                    IconName = IconName.Replace($"{screen.scenario.CharacterNames[i]} ", $"person{i+1}");

                IconName = IconName.Replace(" ", "");
                IconName = IconName.Replace("lvl1", "");
                IconName = IconName.Replace("lvl2", "");
            }
            public string ResponseText { get; set; }
            public string Target { get; set; }
            public string IconName { get; set; }
        }
    }
    public class FullscrrenMessage : Command
    {
        public FullscrrenMessage(Screen screen,XmlNode source)
        {
            Screen = screen;
            message = source.InnerText;
        }
        private string message;
        public override Coroutine ExecutionProcess()
        {
            var s = Screen.scenario;

            GamePlayScreen.Theater.Active = false;

            foreach (var variable in Screen.scenario.Variables)
                message = message.Replace($"${variable.Key};", variable.Value);

            var desposableContainer = new Container(GamePlayScreen.Canvas, Vector2.Zero);
            var messagebox = new MessageBox(desposableContainer, ContentLibrary.GetSprite(SpriteList.messageBoxMedium), new Point(-1000, -607 -1000), new Point(1080 + 2000, 1920 + 2000), segmentateMessage(message,35), "",true);
            messagebox.TintColor = Color.DimGray;
            bool locked = true;

            messagebox.OnReleased += () => locked = false;
            messagebox.OnReleased += () => messagebox.ShowBottomText = false;

            while (locked) yield return 0;

            desposableContainer.DestroyChilderen();
            GamePlayScreen.Theater.Active = true;
        }
    }
}
