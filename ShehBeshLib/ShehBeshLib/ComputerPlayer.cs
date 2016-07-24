using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace ShehBeshLib
{
    public class ComputerPlayer : IPlayer
    {
        public CellState State { get; set; }
        public TurnData GetTurn(TurnData[] turns)
        {
            Random random = new Random();
            Thread.Sleep(200);
            return turns[random.Next(0, turns.Length)];
        }
    }
}