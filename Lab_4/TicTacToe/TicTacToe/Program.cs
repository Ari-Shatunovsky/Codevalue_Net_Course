using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToeGame game = new TicTacToeGame();

            while (game.CheckState() == State.NextTurn)
            {
                string playerName = game.CurrentPlayer == Player.X ? "Player X" : "Player Y";
                Console.WriteLine("{0} Make turn (x, y):", playerName);
                string userInput = Console.ReadLine();
                if (userInput != null)
                {
                    string[] coordinatesArray = userInput.Split(',');
                    if (coordinatesArray.Length == 2)
                    {
                        int x = -1;
                        int y = -1;
                        int.TryParse(coordinatesArray[0], out x);
                        int.TryParse(coordinatesArray[1], out y);
                        if (x > 0 && x <= game.BoardSize && y > 0 && y <= game.BoardSize)
                        {
                            if (game.MakeTurn(x, y))
                            {
                                game.DrawBoard();
                            }
                            else
                            {
                                Console.WriteLine("Cell is already used");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong coodrinates");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong format");
                    }
                }
            }

            switch (game.CheckState())
            {
                case State.PlayerXWin:
                    Console.WriteLine("Player X win!");
                    break;
                case State.PlayerOWin:
                    Console.WriteLine("Player O win!");
                    break;
                case State.Draw:
                    Console.WriteLine("Game is Draw");
                    break;
            }
        }
    }
}
