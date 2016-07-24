using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShehBeshLib
{
    public class GameField
    {
        public List<GameCell> Cells { get; set; }

        public GameCell HeadPlayerA
        {
            get { return Cells[0]; } 
            set { Cells[0] = value; }
        }

        public GameCell HeadPlayerB
        {
            get { return Cells[12]; }
            set { Cells[12] = value; }
        }

        public GameCell OutPlayerA { get; set; }
        public GameCell OutPlayerB { get; set; }

        public GameField()
        {
            Cells = new List<GameCell>(24);
            for (var i = 0; i < 24; i++)
            {
                Cells.Add(new GameCell());
            }
            OutPlayerA = new GameCell();
            OutPlayerB = new GameCell();
        }
    }
}
