using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku theSudoku = new Sudoku();
            theSudoku.SolveSudoku();
        }
    }

    class Sudoku
    {
        Random random = new Random();
        private bool solveDidChange = true;
        private char[][][][] Dis_rows = new char[][][][]
        {
            new char[][][]
            {
                new char[][] { new char[] { 'x', 'x', '6' }, new char[] { 'x', 'x', 'x' }, new char[] { '8', '7', 'x' } },
                new char[][] { new char[] { 'x', '7', '5' }, new char[] { '3', 'x', 'x' }, new char[] { 'x', '6', '9' } },
                new char[][] { new char[] { '2', 'x', 'x' }, new char[] { 'x', 'x', 'x' }, new char[] { 'x', '3', 'x' } }
            },
            new char[][][]
            {
                new char[][] { new char[] { '6', 'x', 'x' }, new char[] { 'x', 'x', 'x' }, new char[] { 'x', 'x', '8' } },
                new char[][] { new char[] { 'x', 'x', 'x' }, new char[] { 'x', '7', 'x' }, new char[] { 'x', 'x', 'x' } },
                new char[][] { new char[] { '1', 'x', 'x' }, new char[] { 'x', 'x', 'x' }, new char[] { 'x', '9', 'x' } }
            },
            new char[][][]
            {
                new char[][] { new char[] { 'x', 'x', 'x' }, new char[] { 'x', '8', 'x' }, new char[] { 'x', 'x', 'x' } },
                new char[][] { new char[] { 'x', '9', '1' }, new char[] { 'x', 'x', '2' }, new char[] { 'x', '5', '7' } },
                new char[][] { new char[] { '3', 'x', '2' }, new char[] { 'x', 'x', '6' }, new char[] { 'x', 'x', '1' } }
            }
        };

        private char[][][][] rows = new char[][][][]
        {
            new char[][][]
            {
                new char[][] { new char[] { 'x', 'x', 'x' }, new char[] { '6', 'x', 'x' }, new char[] { 'x', 'x', 'x' } },
                new char[][] { new char[] { 'x', '2', 'x' }, new char[] { 'x', 'x', 'x' }, new char[] { '1', '5', '6' } },
                new char[][] { new char[] { 'x', 'x', 'x' }, new char[] { '3', '4', 'x' }, new char[] { 'x', 'x', 'x' } }
            },
            new char[][][]
            {
                new char[][] { new char[] { 'x', 'x', '4' }, new char[] { '1', '5', 'x' }, new char[] { 'x', 'x', 'x' } },
                new char[][] { new char[] { '3', 'x', '6' }, new char[] { 'x', 'x', '9' }, new char[] { 'x', '2', 'x' } },
                new char[][] { new char[] { '7', 'x', '9' }, new char[] { 'x', 'x', 'x' }, new char[] { '7', 'x', '1' } }
            },
            new char[][][]
            {
                new char[][] { new char[] { 'x', 'x', '5' }, new char[] { 'x', '7', '6' }, new char[] { '2', '1', 'x' } },
                new char[][] { new char[] { 'x', 'x', 'x' }, new char[] { '4', '1', 'x' }, new char[] { 'x', 'x', 'x' } },
                new char[][] { new char[] { 'x', 'x', 'x' }, new char[] { 'x', 'x', '2' }, new char[] { 'x', 'x', '9' } }
            }
        };


        /// <summary>
        /// The main method that internaly solves the sudoku. This method also displays the "primary solve", which is as far the program can be sure it's 100% correct.
        /// </summary>
        public void SolveSudoku()
        {
            Console.WriteLine(ToString() + "\n");
            WriteLineInWhite("Preparing to solve the sudoku...");
            Console.WriteLine();

            int solveTry = 1;
            while (!SolveSequence())
            {
                WriteLineInRed("Solve failed");
                Console.WriteLine();
                WriteLineInRed($"Solve {solveTry} result:");
                solveTry++;
                Console.WriteLine("\n" + ToString() + "\n");

                WriteLineInWhite("Trying again...");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Tries to solve the sudoku by bruteforce
        /// </summary>
        /// <returns>Returns true if sudoku is successfully solved, else it returns false</returns>
        private bool SolveSequence()
        {
            while (solveDidChange)
            {
                solveDidChange = false;
                PrimarySolveSquares();
            }
            WriteLineInWhite("Primary solve done:\n");
            Console.WriteLine(ToString());

            WriteLineInWhite("\nBeginning bruteforce-sequence in 3...");
            Thread.Sleep(1000);
            WriteLineInWhite("\nBeginning bruteforce-sequence in 2...");
            Thread.Sleep(1000);
            WriteLineInWhite("\nBeginning bruteforce-sequence in 1...");
            Thread.Sleep(1000);
            WriteLineInWhite("\nBruteforce-sequence started.");

            solveDidChange = true;
            while (solveDidChange)
            {
                solveDidChange= false;
                PrimarySolveSquares();
                if (!solveDidChange) SecondarySolveSquares();
            }
            return CheckIfDone();
        }

        /// <summary>
        /// Tries to fit in numbers in the sudoku. The number has to be right for it to be assigned.
        /// </summary>
        private void PrimarySolveSquares()
        {
            // Go through every unown number character in the sudoku
            for (int rowGroup = 0; rowGroup < rows.Length; rowGroup++)
            {
                for (int row = 0; row < rows[rowGroup].Length; row++)
                {
                    for (int columnGroup = 0; columnGroup < rows[rowGroup][row].Length; columnGroup++)
                    {
                        for (int column = 0; column < rows[rowGroup][row][columnGroup].Length; column++)
                        {
                            var testValue = rows[rowGroup][row][columnGroup][column].ToString();
                            if (testValue != "x") continue;

                            // Check if number 1 thru 9 could be in the gridspot
                            for (int i = 1; i <= 9; i++)
                            {
                                if (CheckRowFor(char.Parse(i.ToString()), rowGroup, row)) continue;
                                if (CheckColumnFor(char.Parse(i.ToString()), columnGroup, column)) continue;
                                if (CheckVicinitySquareFor(char.Parse(i.ToString()), rowGroup, columnGroup)) continue;

                                if (CheckIfOnlyAllowed(char.Parse(i.ToString()), rowGroup, row, columnGroup, column))
                                {
                                    rows[rowGroup][row][columnGroup][column] = char.Parse(i.ToString());
                                    solveDidChange = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Goes through every square and checks is's value.
        /// </summary>
        /// <returns>Returns true if no unown values are found, else it returns false</returns>
        private bool CheckIfDone()
        {
            for (int rowGroup = 0; rowGroup < rows.Length; rowGroup++)
            {
                for (int row = 0; row < rows[rowGroup].Length; row++)
                {
                    for (int columnGroup = 0; columnGroup < rows[rowGroup][row].Length; columnGroup++)
                    {
                        for (int column = 0; column < rows[rowGroup][row][columnGroup].Length; column++)
                        {
                            var testValue = rows[rowGroup][row][columnGroup][column].ToString();
                            if (testValue == "x") return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Tries to fit in numbers in the sudoku. The number does not need to be the only fit for it to be assinged. A Random variable assigns a random value that is tried for a few criterias.
        /// </summary>
        private void SecondarySolveSquares()
        {
            // Go through every unown number character in the sudoku
            for (int rowGroup = 0; rowGroup < rows.Length; rowGroup++)
            {
                for (int row = 0; row < rows[rowGroup].Length; row++)
                {
                    for (int columnGroup = 0; columnGroup < rows[rowGroup][row].Length; columnGroup++)
                    {
                        for (int column = 0; column < rows[rowGroup][row][columnGroup].Length; column++)
                        {
                            var testValue = rows[rowGroup][row][columnGroup][column].ToString();
                            if (testValue != "x") continue;

                            for (int i = 1; i <= 9; i++)
                            {
                                var j = random.Next(1, 10);
                                if (CheckRowFor(char.Parse(j.ToString()), rowGroup, row)) continue;
                                if (CheckColumnFor(char.Parse(j.ToString()), columnGroup, column)) continue;
                                if (CheckVicinitySquareFor(char.Parse(j.ToString()), rowGroup, columnGroup)) continue;

                                rows[rowGroup][row][columnGroup][column] = char.Parse(j.ToString());
                                solveDidChange = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if it's the only square in group that is allowed to be searchChar.
        /// </summary>
        /// <param name="searchChar">The character you want to match</param>
        /// <param name="rowGroup">In what row group the row is located</param>
        /// <param name="row">In what row, inside the row group, the searchChar is located</param>
        /// <param name="columnGroup">In what column group the column is located</param>
        /// <param name="column">In what column, inside the column group, the searchChar is located</param>
        /// <returns>Returns true if it's the only one, else it returns false</returns>
        private bool CheckIfOnlyAllowed(char searchChar, int rowGroup, int row, int columnGroup, int column)
        {
            for (int currRow = 0; currRow < rows[rowGroup].Length; currRow++)
            {
                for (int currCol = 0; currCol < rows[rowGroup][currRow][columnGroup].Length; currCol++)
                {
                    if (rows[rowGroup][currRow][columnGroup][currCol] != 'x' || (currCol == column && currRow == row)) continue;
                    if (!CheckRowFor(searchChar, rowGroup, currRow) && !CheckColumnFor(searchChar, columnGroup, currCol))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks the neighbouring rows and columns. It checks if searchChar could be found.
        /// </summary>
        /// <param name="searchChar">The character you want to match</param>
        /// <param name="rowGroup">In what row group the row is located</param>
        /// <param name="row">In what row, inside the row group, the searchChar is located</param>
        /// <param name="columnGroup">In what column group the column is located</param>
        /// <param name="column">In what column, inside the column group, the searchChar is located</param>
        /// <returns>Returns true if ALL siblings contains searchChar</returns>
        private bool CheckSiblings(char searchChar, int rowGroup, int row, int columnGroup, int column)
        {
            bool isPossible = true;

            if (!(rowGroup == 0 && row == 0)) CheckTop();
            if (!(rowGroup == 2 && row == 2)) CheckBottom();
            if (!(columnGroup == 0 && column == 0)) CheckLeft();
            if (!(columnGroup == 2 && column == 2)) CheckRight();

            if (isPossible) return true;
            return false;

            void CheckTop()
            {
                if (row == 0)
                {
                    if (!CheckRowFor(searchChar, --rowGroup, 2)) isPossible = false;
                    return;
                }
                if (!CheckRowFor(searchChar, rowGroup, --row)) isPossible = false;
            }

            void CheckBottom()
            {
                if (row == 2)
                {
                    if (!CheckRowFor(searchChar, ++rowGroup, 0)) isPossible = false;
                    return;
                }
                if (!CheckRowFor(searchChar, rowGroup, ++row)) isPossible = false;
            }

            void CheckRight()
            {
                if (column == 2)
                {
                    if (!CheckColumnFor(searchChar, ++columnGroup, 0)) isPossible = false;
                    return;
                }
                if (!CheckColumnFor(searchChar, columnGroup, ++column)) isPossible = false;
            }

            void CheckLeft()
            {
                if (column == 0)
                {
                    if (!CheckColumnFor(searchChar, --columnGroup, 2)) isPossible = false;
                    return;
                }
                if (!CheckColumnFor(searchChar, columnGroup, --column)) isPossible = false;
            }
        }

        /// <summary>
        /// Checks for a character in a row
        /// </summary>
        /// <param name="searchChar">The character you want to match</param>
        /// <param name="rowGroup">In what row group the row is located</param>
        /// <param name="row">In what row, inside the row group, the searchChar is located</param>
        /// <returns>Returns true if character is found, else false is returned</returns>
        private bool CheckRowFor(char searchChar, int rowGroup, int row)
        {
            foreach (var columnGroup in rows[rowGroup][row])
            {
                foreach (var columnItem in columnGroup)
                {
                    if (columnItem == searchChar)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for a character in a column
        /// </summary>
        /// <param name="searchChar">The character you want to match</param>
        /// <param name="columnGroup">In what column group the column is located</param>
        /// <param name="column">In what column, inside the column group, the searchChar is located</param>
        /// <returns>Returns true if character is found, else false is returned</returns>
        private bool CheckColumnFor(char searchChar, int columnGroup, int column)
        {
            foreach (var rowGroup in rows)
            {
                foreach (var row in rowGroup)
                {
                    if (row[columnGroup][column] == searchChar)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for a character in the vicinity square, AKA the 3x3 squares
        /// </summary>
        /// <param name="searchChar">The character you want to match</param>
        /// <param name="rowGroup">In what rowGroup the searchChar is located</param>
        /// <param name="columnGroup">In what columnGroup the searchChar is located</param>
        /// <returns>Returns true if character is found, else false is returned</returns>
        private bool CheckVicinitySquareFor(char searchChar, int rowGroup, int columnGroup)
        {
            foreach (var row in rows[rowGroup])
            {
                foreach (var column in row[columnGroup])
                {
                    if (column == searchChar)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int rowGroup = 0; rowGroup < rows.Length; rowGroup++)
            {
                for (int row = 0; row < rows[rowGroup].Length; row++)
                {
                    for (int columnGroup = 0; columnGroup < rows[rowGroup][row].Length; columnGroup++)
                    {
                        for (int column = 0; column < rows[rowGroup][row][columnGroup].Length; column++)
                        {                 
                            if (rows[rowGroup][row][columnGroup][column] == 'x')
                            {
                                sb.Append("_" + " ");
                            }
                            else
                            {
                                sb.Append(rows[rowGroup][row][columnGroup][column].ToString() + ' ');
                            }
                        }
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        private void WriteLineInWhite(string text)
        {
            ConsoleColor pastFore = Console.ForegroundColor;
            ConsoleColor pastBack = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(text);

            Console.ForegroundColor = pastFore;
            Console.BackgroundColor = pastBack;
        }

        private void WriteLineInRed(string text)
        {
            ConsoleColor pastFore = Console.ForegroundColor;
            ConsoleColor pastBack = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;

            Console.WriteLine(text);

            Console.ForegroundColor = pastFore;
            Console.BackgroundColor = pastBack;
        }
    }
}
