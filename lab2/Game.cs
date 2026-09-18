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

        public Game(int size)
        {
            this.size = size;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            state = GameState.Start;
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