using System;

namespace CatAndMouse
{
    public enum State
    {
        Winner,
        Loser,
        Playing,
        NotInGame
    }

    public class Player
    {
        public string name;
        public int location;
        public State state = State.NotInGame;
        public int distanceTraveled = 0;

        public Player(string name)
        {
            this.name = name;
            this.location = -1;
        }

        public void Move(int steps, int fieldSize)
        {
            if (state == State.NotInGame)
            {
                // Установка начальной позиции
                location = ((steps - 1) % fieldSize + fieldSize) % fieldSize + 1;
                state = State.Playing;
                return;
            }

            distanceTraveled += Math.Abs(steps);
            int newPos = location + steps;
            // Приведение к диапазону 1..fieldSize (по кругу)
            location = ((newPos - 1) % fieldSize + fieldSize) % fieldSize + 1;
        }
    }
}