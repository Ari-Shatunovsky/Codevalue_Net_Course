using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ShehBeshLib
{
    public enum TurnState
    {
       Succsess = 1,
       NoPossibleTurn
    }

    public enum TurnType
    {
        TwoDiceSum = 1,
        BiggerDice,
        SmallestDice,
    }

    public class GameLogic
    {
        public event EventHandler TurnSucceed;

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
            GameCell startCell = field.Cells[startIndex];
            int cellIndex = (startIndex + dice)%24;
            GameCell endCell;
            if (player == CellState.PlayerA && cellIndex == 0)
            {
                endCell = field.OutPlayerA;
            } else if (player == CellState.PlayerB && cellIndex == 12)
            {
                endCell = field.OutPlayerB;
            }
            else
            {
                endCell = field.Cells[cellIndex];
            }

            
            startCell.NumberOfCheckers --;
            endCell.State = player;
            endCell.NumberOfCheckers ++;
            if (startCell.NumberOfCheckers == 0)
            {
                startCell.State = CellState.Neytral;
            }
            TurnSucceed?.Invoke(this, null);
            return true;
        }

        public TurnData[] GetPossibleTurns(CellState player, int[] dice, GameField field)
        {
            List<TurnData> turns = new List<TurnData>();
            for (int i = 0; i < field.Cells.Count ; i++)
            {
                if (IsPossibleMove(player, i, dice[0], field))
                {
                    turns.Add(new TurnData(TurnType.BiggerDice, i, dice[0]));
                }
                if (IsPossibleMove(player, i, dice[1], field))
                {
                    turns.Add(new TurnData(TurnType.SmallestDice, i, dice[1]));
                }
                if (IsPossibleMove(player, i, dice[0] + dice[1], field))
                {
                    turns.Add(new TurnData(TurnType.TwoDiceSum, i, dice[0] + dice[1]));
                }
            }
            return turns.ToArray();
        }

        public bool IsPossibleMove(CellState player, int startIndex, int dice, GameField field)
        {
            if (startIndex > field.Cells.Count || dice == 0)
            {
                return false;
            }
            GameCell startCell = field.Cells[startIndex];
            if (startCell.State == player)
            {
                if (player == CellState.PlayerB && startIndex + dice == 12)
                {
                    return true;
                }

                if (player == CellState.PlayerA && startIndex + dice == 24)
                {
                    return true;
                }
            }
            if (startIndex + dice > field.Cells.Count && player == CellState.PlayerA)
            {
                return false;
            }
            if (startIndex < 12 && startIndex + dice > 12 && player == CellState.PlayerB)
            {
                return false;
            }
            
            GameCell endCell = field.Cells[(startIndex + dice)%24];

            if ((startCell.State != player && startCell.State != CellState.Neytral) || startCell.NumberOfCheckers <= 0 ||
                (endCell.State != player && endCell.State != CellState.Neytral))
            {
                return false;
            }

            return true;
        }

        public void MakeTurn(IPlayer player, int[] dices, GameField field)
        {
            TurnData[] turns = GetPossibleTurns(player.State, dices, field);
            if (turns.Length == 0)
            {
                return;
            }
            TurnData turn = player.GetTurn(turns);
            MakeMove(player.State, turn.StartIndex, turn.Dice, field);
            switch (turn.TurnType)
            {
                case TurnType.TwoDiceSum:
                    dices[0] = 0;
                    dices[1] = 0;
                    break;
                case TurnType.BiggerDice:
                    dices[1] = 0;
                    break;
                default:
                    dices[0] = 0;
                    break;
            }
            turns = GetPossibleTurns(player.State, dices, field);
            if (turns.Length == 0)
            {
                return;
            }
            turn = player.GetTurn(turns);
            MakeMove(player.State, turn.StartIndex, turn.Dice, field);
        }
    }
}