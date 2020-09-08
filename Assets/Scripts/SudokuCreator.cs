using System;

namespace SudokuConsole
{
    class SudokuCreator
    {
        #region Instance
        private static SudokuCreator instance;
        private static Random random;

        public static SudokuCreator Instance()
        {
            if (instance == null)
                instance = new SudokuCreator();

            if (random == null)
                random = new Random();

            return instance;
        }
        #endregion

        #region sudoku
        private int[,] sudoku = new int[9, 9];

        public int[,] GetRandomSudoku()
        {
            //create row 1
            int[] row1 = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Shuffle(row1);

            // row 1
            for (int i = 0; i < 9; i++) sudoku[0, i] = row1[i];

            // row 2 and 3
            CompleteSection(0);

            // row 4
            int[] added4 = new int[9];
            for (int d = 0; d < 9; d++)
            {
                int[] topValues = new int[3];
                for (int e = 0; e < 3; e++) topValues[e] = sudoku[e, d];
                for (int f = 0; f < 9; f++)
                {
                    int item = sudoku[0, f];
                    if (!Find(topValues, item) && !Find(added4, item))
                    {
                        sudoku[3, d] = item;
                        added4[d] = item;
                        break;
                    }
                }
                sudoku[3, 6] = sudoku[0, 8];
                sudoku[3, 8] = sudoku[0, 7];
            }

            // row 5 and 6
            CompleteSection(3);

            // row 7
            int[] added7 = new int[9];
            for (int h = 0; h < 9; h++)
            {
                int[] topValues2 = new int[6];
                for (int i = 0; i < 6; i++) topValues2[i] = sudoku[i, h];
                for (int j = 0; j < 9; j++)
                {
                    int item = sudoku[3, j];
                    if (!Find(topValues2, item) && !Find(added7, item))
                    {
                        sudoku[6, h] = item;
                        added7[h] = item;
                        break;
                    }
                }
            }

            // row 8 and 9
            CompleteSection(6);

            return sudoku;
        }

        #region private area
        private void Swap(int[] ar, int i, int j)
        {
            var store = ar[i];
            ar[i] = ar[j];
            ar[j] = store;
        }

        private void Shuffle(int[] ar)
        {
            for (int i = ar.Length; i > 0; i--)
                Swap(ar, 0, random.Next(0, i));
        }

        private bool Find(int[] ar, int num)
        {
            for (int i = 0; i < ar.Length; i++)
                if (ar[i] == num) return true;

            return false;
        }

        private void CompleteSection(int section)
        {
            int start3 = 3, start6 = 6;
            for (int i = 0; i < 9; i++)
            {
                sudoku[section + 1, i] = sudoku[section, start3];
                sudoku[section + 2, i] = sudoku[section, start6];
                start3++;
                start6++;
                if (start3 == 9) start3 = 0;
                if (start6 == 9) start6 = 0;
            }
        }
        #endregion
        #endregion
    }
}