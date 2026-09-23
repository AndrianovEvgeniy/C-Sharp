using System;

namespace CatAndMouse
{
    public enum State
    {
        Winner,
        Loser,
        Playing
    }

    public class Player
    {
        public string name;
        public int location;
        public State state = State.Playing;
        public int distanceTraveled = 0;

        public Player(string name)
        {
            this.name = name;
            this.location = -1;
        }

        public void Move(int steps, int fieldSize)
        {
            if (location == -1)   // первый вызов — это размещение, не ход
            {
                location = ((steps - 1) % fieldSize + fieldSize) % fieldSize + 1;
                return;
            }

            distanceTraveled += Math.Abs(steps);
            int newPos = location + steps;
            location = ((newPos - 1) % fieldSize + fieldSize) % fieldSize + 1;
        }
    }
}