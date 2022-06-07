﻿using System;
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
            theSudoku.SolveSudoku();
            Console.WriteLine("\n\n");
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
