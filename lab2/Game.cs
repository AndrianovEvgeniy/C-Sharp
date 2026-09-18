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