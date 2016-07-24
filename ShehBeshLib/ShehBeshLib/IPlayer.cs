
using System.Diagnostics;

namespace ShehBeshLib
{
    public interface IPlayer
    {
        CellState State { get; set; }
        TurnData GetTurn(TurnData[] turns);
    }
}