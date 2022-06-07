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
        private char[][][][] rows = new char[][][][]
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
                new char[][] { new char[] { '3', 'x', '2' }, new char[] { 'x', 'x', '6' }, new char[] { 'x', 'x', '1' } } // Second x in third group should be an 8
            }
        };

        public void SolveSudoku()
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

                        }
                    }
                }
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
                foreach (var columnBundle in row)
                {
                    foreach (var column in columnBundle)
                    {
                        if (column == searchChar)
                        {
                            return true;
                        }
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
    }

    class SudokuDiscontinued
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

        public void SolveSudoku()
        {
            for(int row = 0; row < rows.Count; row++)
            {
                for (int column = 0; column < rows[row].Length; column++)
                {
                    var testValue = rows[row][column];
                    if (testValue == 'x') continue;

                    for (int newNum = 1; newNum < 9; newNum++)
                    {
                        if (CheckColumnFor((char)column, (char)newNum)) continue;
                        if (CheckRowFor((char)row, (char)newNum)) continue;
                        if (CheckSquareFor((char)row, (char)column, (char)newNum)) continue;

                        rows[row][column] = 'F';
                    }
                }
            }
        }

        public bool CheckSquareFor(char rowNum, char colNum, char controlNum)
        {
            // Find rowGroup
            var groupCounter = 0;
            var currentGroup = 0;
            var rowGroup = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                groupCounter++;
                if (groupCounter == 3)
                {
                    groupCounter = 0;
                    currentGroup++;
                }
                if ((int)rowNum == i) rowGroup = currentGroup;
            }

            // Find columnGroup
            groupCounter = 0;
            currentGroup = 0;
            var columnGroup = 0;
            for (int i = 0; i < rows[rowNum].Length; i++)
            {
                groupCounter++;
                if (groupCounter == 3)
                {
                    groupCounter = 0;
                    currentGroup++;
                }
                if ((int)rowNum == i) columnGroup = currentGroup;
            }

            // Find startindex for each group
            var startRowIndex = rowGroup * 3;
            var startColumnIndex = columnGroup * 3;
            startRowIndex = 0;
            startColumnIndex = 0;

            // Check square for number
            for (int i = startRowIndex; i < startRowIndex + 3; i++)
            {
                for (int j = startColumnIndex; j < startColumnIndex + 3; j++)
                {
                    if (rows[i][j] == controlNum) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks row "rowNum" for the number "controlNum"
        /// </summary>
        /// <returns>True num is found, else false is returned</returns>
        private bool CheckRowFor(char rowNum, char controlNum)
        {
            foreach (var column in rows[rowNum])
            {
                if (column == controlNum)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks column "colNum" for the number "controlNum"
        /// </summary>
        /// <returns>True num is found, else false is returned</returns>
        private bool CheckColumnFor(char colNum, char controlNum)
        {
            foreach (var row in rows)
            {
                if (row[colNum] == controlNum)
                {
                    return true;
                }
            }
            return false;
        }

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
