using System;
using System.IO;

namespace CatAndMouse
{
    public enum GameState
    {
        Start,
        End
    }

    public class Game
    {
        public static string InputFile = "1.ChaseData.txt";
        public static string OutFile = "1.PursuitLog.txt";

        public int size;
        public Player cat;
        public Player mouse;
        public GameState state;
        private StreamWriter writer;
        public Game(int size)
        {
            this.size = size;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            state = GameState.Start;
        }
        public void Run()
        {
            string[] lines = File.ReadAllLines(InputFile);
            size = int.Parse(lines[0].Trim());

            writer = new StreamWriter(OutFile);
            writer.WriteLine("Cat and Mouse");
            writer.WriteLine();
            writer.WriteLine("Cat Mouse  Distance");
            writer.WriteLine("-------------------");

            bool mouseCaught = false;
            int caughtAt = -1;

            for (int i = 1; i < lines.Length && state != GameState.End; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                char command = parts[0][0];

                switch (command)
                {
                    case 'M':
                    case 'C':
                        int steps = int.Parse(parts[1]);
                        DoMoveCommand(command, steps);
                        if (cat.state == State.Playing && mouse.state == State.Playing
                            && cat.location == mouse.location)
                        {
                            mouseCaught = true;
                            caughtAt = cat.location;
                            state = GameState.End;
                        }
                        break;
                    case 'P':
                        DoPrintCommand();
                        break;
                }
            }

            writer.WriteLine("-------------------");
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Distance traveled:   Mouse    Cat");
            writer.WriteLine($"                        {mouse.distanceTraveled}      {cat.distanceTraveled}");
            writer.WriteLine();

            if (mouseCaught)
                writer.WriteLine($"Mouse caught at: {caughtAt}");
            else
                writer.WriteLine("Mouse evaded Cat");

            writer.Close();
        }
        private void DoMoveCommand(char command, int steps)
        {
            switch (command)
            {
                case 'M':
                    mouse.Move(steps, size);
                    break;
                case 'C':
                    cat.Move(steps, size);
                    break;
            }
        }

        private void DoPrintCommand()
        {
            string catStr = cat.state == State.NotInGame ? "??" : cat.location.ToString();
            string mouseStr = mouse.state == State.NotInGame ? "??" : mouse.location.ToString();

            if (cat.state != State.NotInGame && mouse.state != State.NotInGame)
            {
                writer.WriteLine($"{catStr,3}{mouseStr,6}{GetDistance(),10}");
            }
            else
            {
                writer.WriteLine($"{catStr,3}{mouseStr,6}");
            }
        }

        private int GetDistance()
        {
            if (cat.state == State.NotInGame || mouse.state == State.NotInGame)
                return -1; // не определено

            int diff = Math.Abs(cat.location - mouse.location);
            return Math.Min(diff, size - diff);
        }
    }
}