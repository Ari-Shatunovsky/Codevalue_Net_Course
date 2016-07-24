using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ShehBeshLib
{
    public class ConsolePlayer : IPlayer
    {
        public CellState State { get; set; }
        public TurnData GetTurn(TurnData[] turns)
        {
            Console.WriteLine("Possible turns:");
            for (var i = 0; i < turns.Length; i++)
            {
                int endCellId = (turns[i].StartIndex + turns[i].Dice)%24;
                string endCellName = "";

                if (endCellId == 12 && State == CellState.PlayerB || endCellId == 24 && State == CellState.PlayerA)
                {
                    endCellName = "out";
                }
                else
                {
                    endCellName = ((turns[i].StartIndex + turns[i].Dice)%24 + 1).ToString();
                }
                Console.WriteLine($"{i + 1}. {turns[i].StartIndex + 1} ==> {endCellName}");
            }

            while (true)
            {
                Console.WriteLine("Enter turn number:");
                string consoleInput = Console.ReadLine();
                int turnId;
                if (consoleInput != null &&
                    int.TryParse(consoleInput, out turnId) &&
                    turnId <= turns.Length)
                {
                    return turns[turnId - 1];
                }
                Console.WriteLine("Wrong number");
            }
        }
    }
}