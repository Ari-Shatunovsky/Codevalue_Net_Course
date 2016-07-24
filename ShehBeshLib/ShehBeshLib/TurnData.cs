using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShehBeshLib
{
    public class TurnData
    {
        public TurnData(TurnType turnType, int startIndex, int dice)
        {
            TurnType = turnType;
            StartIndex = startIndex;
            Dice = dice;
        }

        public TurnType TurnType { get; set; }
        public int StartIndex { get; set; }
        public int Dice { get; set; }
    }
}
