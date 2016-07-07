using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShehBeshLib
{
    public enum CellState
    {
        Neytral,
        PlayerA,
        PlayerB
    }

    public class GameCell
    {
        public int NumberOfCheckers { get; set; }
        public CellState State { get; set; }

        public GameCell()
        {
            State = CellState.Neytral;
        }

        public GameCell(CellState state, int numberOfCheckers)
        {
            State = state;
            NumberOfCheckers = numberOfCheckers;
        }
    }
}
