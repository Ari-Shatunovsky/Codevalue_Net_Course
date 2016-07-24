using System;
using System.Diagnostics;

namespace ShehBeshLib
{


    public class ConsoleRenderEngine : IRenderEngine

    {
        private const int FieldHeight = 24;

        public void RenderFiled(GameField field)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("   ===24==23==22==21==20==19=====18==17==16==15==14==13===");
            for (int i = 0; i < FieldHeight; i++)
            {
                RenderRow(field, i);
            }
            Console.WriteLine("   ====1===2===3===4===5===6======7===8===9==10==11==12===");
        }

        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }

        private ConsoleColor GetColorForPlayer(CellState state)
        {
            if (state == CellState.PlayerA)
            {
                return ConsoleColor.Blue;
            }
            if (state == CellState.PlayerB)
            {
                return ConsoleColor.Red;
            } 
            return ConsoleColor.Black;
        }

        private void RenderRow(GameField field, int rowId)
        {
            var cellCount = field.Cells.Count;
            if (field.OutPlayerA.NumberOfCheckers > FieldHeight - rowId - 1)
            {
                Console.ForegroundColor = GetColorForPlayer(CellState.PlayerA);
                Console.Write(" o ");
            }
            else
            {
                Console.Write("   ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("||");
            for (var i = 0; i < cellCount / 2; i++)
            {
                if (field.Cells[cellCount - i - 1].NumberOfCheckers > rowId)
                {
                    Console.ForegroundColor = GetColorForPlayer(field.Cells[cellCount - i - 1].State);
                    Console.Write("  o ");
                }
                else if (field.Cells[i].NumberOfCheckers > FieldHeight - rowId - 1)
                {
                    Console.ForegroundColor = GetColorForPlayer(field.Cells[i].State);
                    Console.Write("  o ");
                }
                else
                {
                    Console.Write("    ");
                }

                if (i != field.Cells.Count/4 - 1) continue;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("|||");

            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("||");
            if (field.OutPlayerB.NumberOfCheckers > FieldHeight - rowId - 1)
            {
                Console.ForegroundColor = GetColorForPlayer(CellState.PlayerB);
                Console.Write(" o ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }
}