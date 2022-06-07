using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku theSudoku = new Sudoku();
            Console.WriteLine(theSudoku.ToString());
        }
    }

    class Sudoku
    {
        private List<char[]> rows = new List<char[]>
            {
                new char[] { 'x', 'x', '6', 'x', 'x', 'x', '8', '7', 'x' },
                new char[] { 'x', '7', '5', '3', 'x', 'x', 'x', '6', '9' },
                new char[] { '2', 'x', 'x', 'x', 'x', 'x', 'x', '3', 'x' },

                new char[] { '6', 'x', 'x', 'x', 'x', 'x', 'x', 'x', '8' },
                new char[] { 'x', 'x', 'x', 'x', '7', 'x', 'x', 'x', 'x' },
                new char[] { '1', 'x', 'x', 'x', 'x', 'x', 'x', '9', 'x' },

                new char[] { 'x', 'x', 'x', 'x', '8', 'x', 'x', 'x', 'x' },
                new char[] { 'x', '9', '1', 'x', 'x', '2', 'x', '5', '7' },
                new char[] { '3', 'x', '2', 'x', 'x', '6', 'x', 'x', '1' },
            };

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var column in rows)
            {
                foreach (var columnSquare in column)
                {
                    if (columnSquare == 'x')
                    {
                        sb.Append("_" + " ");
                    }
                    else
                    {
                        sb.Append(columnSquare + " ");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
