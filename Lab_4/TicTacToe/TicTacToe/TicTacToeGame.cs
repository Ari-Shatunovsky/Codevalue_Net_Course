using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum State
    {
        NextTurn = 1,
        PlayerXWin,
        PlayerOWin,
        Draw
    }

    public enum GameCell
    {
        Empty = 0,
        X,
        O
    }

    public enum Player
    {
        X = 1,
        O
    }

    public class TicTacToeGame
    {
        public int BoardSize { get; private set; }
        private GameCell[,] board;
        public Player CurrentPlayer { get; private set; }

        public TicTacToeGame()
        {
            BoardSize = 3;
            board = new GameCell[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    board[i, j] = GameCell.Empty;
                }
            }
            CurrentPlayer = Player.X;
        }

        public bool MakeTurn(int i, int j)
        {
            i --;
            j --;
            if (board[i, j] == GameCell.Empty)
            {
                board[i, j] = CurrentPlayer == Player.X ? GameCell.X : GameCell.O;
                CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DrawBoard()
        {
            Console.WriteLine("______");
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Console.Write("|");
                    switch (board[i, j])
                    {
                        case GameCell.Empty:
                            Console.Write(" ");
                            break;
                        case GameCell.O:
                            Console.Write("O");
                            break;
                        case GameCell.X:
                            Console.Write("X");
                            break;
                    }
                }
                Console.Write("|");
                Console.WriteLine("");
                Console.WriteLine("______");
            }
        }

        public State CheckState()
        {
            GameCell verticalCell;
            GameCell horizontalCell;
            GameCell diagonalCell = board[0, 0];
            bool isNextTurn = false;
            for (int i = 0; i < BoardSize; i++)
            {
                horizontalCell = board[i, 0];
                verticalCell = board[0, i];

                if (diagonalCell != board[i, i])
                {
                    diagonalCell = GameCell.Empty;
                }

                for (int j = 0; j < BoardSize; j++)
                {
                    if (horizontalCell != board[i, j])
                    {
                        horizontalCell = GameCell.Empty;
                    }
                    if (verticalCell != board[j, i])
                    {
                        verticalCell = GameCell.Empty;
                    }
                    if (board[i, j] == GameCell.Empty)
                    {
                        isNextTurn = true;
                    }
                }
                if (horizontalCell == GameCell.X || verticalCell == GameCell.X)
                {
                    return State.PlayerXWin;
                }
                if (horizontalCell == GameCell.O || verticalCell == GameCell.O)
                {
                    return State.PlayerOWin;
                }
            }
            if (diagonalCell == GameCell.X)
            {
                return State.PlayerXWin;
            }
            if (diagonalCell == GameCell.O)
            {
                return State.PlayerOWin;
            }
            if (isNextTurn)
            {
                return State.NextTurn;
            }
            return State.Draw;
        }
    }
}
