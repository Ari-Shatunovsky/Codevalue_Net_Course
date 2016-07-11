using System;

namespace ShehBeshLib
{
    public enum TurnType
    {
        OneCheckerSum,
        TwoCheckers,
        OneCheckerOneDice,
        MissTurn
    }

    public class GameLogic
    {

        public GameField InitGameField()
        {
            GameField gameField = new GameField
            {
                HeadPlayerA = new GameCell(CellState.PlayerA, 15),
                HeadPlayerB = new GameCell(CellState.PlayerB, 15)
            };

            return gameField;
        }

        public int[] RollDice()
        {
            Random random = new Random();
            int[] result = new int[2];
            result[0] = random.Next(1, 7);
            result[1] = random.Next(1, 7);
            Array.Sort(result);
            return result;
        }

        public bool MakeMove(CellState player, int startIndex, int dice, GameField field)
        {
            if (startIndex >= field.Cells.Count || startIndex + dice >= field.Cells.Count)
            {
                return false;
            }

            GameCell startCell = field.Cells[startIndex]; 
            GameCell endCell = field.Cells[startIndex + dice];

            if (startCell.State != player || startCell.NumberOfCheckers <= 0 ||
                (endCell.State != player && endCell.State != CellState.Neytral))
            {
                return false;
            }

            startCell.NumberOfCheckers --;
            endCell.State = player;
            endCell.NumberOfCheckers ++;
            return true;
        }

        public bool IsPossibleMove(CellState player)

//        public bool MakeTurn(CellState player, TurnType type, int[] dice, GameField field)
//        {
//            if (TurnType.OneCheckerSum)
//            {
//                
//            }
//        }
    }
}