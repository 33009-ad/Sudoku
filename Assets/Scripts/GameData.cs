using UnityEngine;
using UnityEngine.UI;

public class GameData
{
    public static GameData gameData;
    private GameData()
    {
        ThemeInit();
        numberOfSolutions = solutionArr.Length / 81;
    }
    public static GameData GetObject()
    {
        if (gameData == null) gameData = new GameData();
        return gameData;
    }
    public int numberOfSolutions;
    public int[,] solutionArr =
    {
        { 8, 4, 5, 6, 3, 2, 1, 7, 9, 7, 3, 2, 9, 1, 8, 6, 5, 4, 1, 9, 6, 7, 4, 5, 3, 2, 8, 6, 8, 3, 5, 7, 4, 9, 1, 2, 4, 5, 7, 2, 9, 1, 8, 3, 6, 2, 1, 9, 8, 6, 3, 5, 4, 7, 3, 6, 1, 4, 2, 9, 7, 8, 5, 5, 7, 4, 1, 8, 6, 2, 9, 3, 9, 2, 8, 3, 5, 7, 4, 6, 1 },
        { 8, 3, 2, 4, 5, 6, 7, 9, 1, 9, 5, 7, 1, 8, 2, 4, 6, 3, 4, 1, 6, 9, 7, 3, 2, 5, 8, 6, 7, 9, 5, 4, 1, 8, 3, 2, 5, 2, 3, 7, 6, 8, 1, 4, 9, 1, 8, 4, 3, 2, 9, 5, 7, 6, 7, 6, 1, 8, 3, 4, 9, 2, 5, 2, 9, 5, 6, 1, 7, 3, 8, 4, 3, 4, 8, 2, 9, 5, 6, 1, 7 },
        { 4, 9, 5, 6, 1, 8, 2, 3, 7, 7, 2, 6, 3, 4, 9, 5, 8, 1, 3, 1, 8, 7, 2, 5, 4, 6, 9, 5, 7, 2, 8, 9, 4, 3, 1, 6, 6, 4, 3, 5, 7, 1, 8, 9, 2, 1, 8, 9, 2, 6, 3, 7, 4, 5, 8, 3, 7, 9, 5, 6, 1, 2, 4, 2, 6, 4, 1, 8, 7, 9, 5, 3, 9, 5, 1, 4, 3, 2, 6, 7, 8 },
        { 2, 5, 6, 8, 3, 1, 7, 4, 9, 8, 3, 7, 6, 4, 9, 5, 1, 2, 1, 9, 4, 7, 2, 5, 3, 8, 6, 6, 4, 1, 5, 8, 7, 9, 2, 3, 7, 2, 5, 1, 9, 3, 8, 6, 4, 3, 8, 9, 4, 6, 2, 1, 7, 5, 9, 7, 8, 2, 5, 4, 6, 3, 1, 5, 6, 2, 3, 1, 8, 4, 9, 7, 4, 1, 3, 9, 7, 6, 2, 5, 8 },
        { 3, 2, 5, 8, 6, 7, 1, 9, 4, 6, 8, 1, 4, 9, 5, 3, 2, 7, 7, 9, 4, 3, 2, 1, 5, 6, 8, 5, 3, 9, 6, 1, 4, 8, 7, 2, 2, 6, 8, 7, 3, 9, 4, 5, 1, 4, 1, 7, 5, 8, 2, 6, 3, 9, 1, 5, 3, 9, 7, 8, 2, 4, 6, 9, 4, 2, 1, 5, 6, 7, 8, 3, 8, 7, 6, 2, 4, 3, 9, 1, 5 },
        { 3, 6, 1, 7, 2, 5, 9, 4, 8, 9, 8, 7, 4, 1, 3, 2, 5, 6, 2, 5, 4, 8, 6, 9, 1, 3, 7, 8, 7, 6, 9, 3, 4, 5, 1, 2, 5, 2, 3, 1, 8, 6, 7, 9, 4, 4, 1, 9, 2, 5, 7, 6, 8, 3, 6, 4, 5, 3, 7, 1, 8, 2, 9, 7, 3, 8, 5, 9, 2, 4, 6, 1, 1, 9, 2, 6, 4, 8, 3, 7, 5 },
        { 8, 5, 7, 2, 6, 1, 3, 9, 4, 3, 1, 2, 4, 9, 5, 7, 8, 6, 9, 6, 4, 3, 7, 8, 2, 1, 5, 1, 9, 5, 7, 3, 4, 6, 2, 8, 7, 2, 8, 9, 5, 6, 1, 4, 3, 6, 4, 3, 1, 8, 2, 5, 7, 9, 5, 8, 1, 6, 4, 7, 9, 3, 2, 4, 7, 9, 5, 2, 3, 8, 6, 1, 2, 3, 6, 8, 1, 9, 4, 5, 7 },
        { 8, 9, 3, 2, 7, 5, 1, 4, 6, 6, 5, 7, 9, 4, 1, 3, 2, 8, 1, 4, 2, 6, 3, 8, 5, 9, 7, 9, 7, 1, 5, 8, 4, 6, 3, 2, 4, 6, 8, 3, 2, 9, 7, 5, 1, 3, 2, 5, 1, 6, 7, 9, 8, 4, 5, 3, 6, 4, 1, 2, 8, 7, 9, 7, 1, 4, 8, 9, 3, 2, 6, 5, 2, 8, 9, 7, 5, 6, 4, 1, 3 },
        { 3, 6, 1, 7, 2, 5, 9, 4, 8, 9, 8, 7, 4, 1, 3, 2, 5, 6, 2, 5, 4, 8, 6, 9, 1, 3, 7, 8, 7, 6, 9, 3, 4, 5, 1, 2, 5, 2, 3, 1, 8, 6, 7, 9, 4, 4, 1, 9, 2, 5, 7, 6, 8, 3, 6, 4, 5, 3, 7, 1, 8, 2, 9, 7, 3, 8, 5, 9, 2, 4, 6, 1, 1, 9, 2, 6, 4, 8, 3, 7, 5 }
    };

    #region theme
    public struct Theme
    {
        public Vector4 background;
        public Vector4[] circleBg, upText, downText;
    }
    public Theme currentTheme;
    public Theme[] theme = new Theme[9];

    private void ThemeInit()
    {
        theme[0].background = new Vector4(251, 251, 251, 255);
        theme[0].circleBg = new Vector4[4] { Vector4.zero, new Vector4(231, 231, 231, 255), new Vector4(188, 185, 166, 255), new Vector4(170, 159, 116, 255) };
        theme[0].upText = new Vector4[4] { new Vector4(122, 124, 121, 255), new Vector4(164, 164, 164, 255), theme[0].background, theme[0].background};
        theme[0].downText = new Vector4[2] { theme[0].upText[0], theme[0].upText[3] };

        theme[1].background = new Vector4(25, 25, 25, 255);
        theme[1].circleBg = new Vector4[4] { Vector4.zero, new Vector4(45, 45, 45, 255), new Vector4(100, 98, 84, 255), new Vector4(156, 143, 94, 255) };
        theme[1].upText = new Vector4[4] { new Vector4(214, 214, 214, 255), new Vector4(159, 159, 159, 255), theme[1].background, theme[1].background };
        theme[1].downText = new Vector4[2] { theme[1].upText[0], theme[1].upText[3] };

        theme[2].background = new Vector4(51, 51, 51, 255);
        theme[2].circleBg = new Vector4[4] { Vector4.zero, new Vector4(103, 103, 103, 255), new Vector4(173, 132, 99, 255), new Vector4(242, 129, 36, 255) };
        theme[2].upText = new Vector4[4] { new Vector4(233, 233, 233, 255), theme[2].background, theme[2].background, theme[2].background };
        theme[2].downText = new Vector4[2] { theme[2].upText[0], theme[2].upText[3] };

        theme[3].background = new Vector4(251, 251, 251, 255);
        theme[3].circleBg = new Vector4[4] { Vector4.zero, new Vector4(231, 231, 231, 255), new Vector4(168, 180, 188, 255), new Vector4(106, 161, 192, 255) };
        theme[3].upText = new Vector4[4] { new Vector4(120, 125, 121, 255), new Vector4(162, 162, 162, 255), theme[3].background, theme[3].background };
        theme[3].downText = new Vector4[2] { theme[3].upText[0], theme[3].upText[3] };

        theme[4].background = new Vector4(255, 255, 255, 255);
        theme[4].circleBg = new Vector4[4] { Vector4.zero, new Vector4(239, 239, 239, 255), new Vector4(220, 183, 186, 255), new Vector4(190, 100, 106, 255) };
        theme[4].upText = new Vector4[4] { new Vector4(156, 110, 113, 255), new Vector4(177, 178, 178, 255), theme[4].background, theme[4].background };
        theme[4].downText = new Vector4[2] { theme[4].upText[0], theme[4].upText[3] };

        theme[5].background = new Vector4(15, 22, 29, 255);
        theme[5].circleBg = new Vector4[4] { Vector4.zero, new Vector4(28, 38, 48, 255), new Vector4(57, 106, 127, 255), new Vector4(45, 187, 249, 255) };
        theme[5].upText = new Vector4[4] { new Vector4(91, 173, 207, 255), new Vector4(128, 149, 170, 255), theme[5].background, theme[5].background };
        theme[5].downText = new Vector4[2] { theme[5].upText[0], theme[5].upText[3] };

        theme[6].background = new Vector4(18, 18, 18, 255);
        theme[6].circleBg = new Vector4[4] { Vector4.zero, new Vector4(34, 36, 34, 255), new Vector4(45, 84, 47, 255), new Vector4(41, 157, 41, 255) };
        theme[6].upText = new Vector4[4] { new Vector4(201, 212, 203, 255), new Vector4(147, 161, 147, 255), theme[6].background, theme[6].background };
        theme[6].downText = new Vector4[2] { theme[6].upText[0], theme[6].upText[3] };

        theme[7].background = new Vector4(28, 28, 28, 255);
        theme[7].circleBg = new Vector4[4] { Vector4.zero, new Vector4(48, 48, 48, 255), new Vector4(129, 66, 87, 255), new Vector4(175, 129, 255, 255) };
        theme[7].upText = new Vector4[4] { new Vector4(228, 217, 121, 255), new Vector4(143, 143, 143, 255), theme[7].background, theme[7].background };
        theme[7].downText = new Vector4[2] { theme[7].upText[0], theme[7].upText[3] };

        theme[8].background = new Vector4(56, 59, 54, 255);
        theme[8].circleBg = new Vector4[4] { Vector4.zero, new Vector4(99, 105, 99, 255), new Vector4(138, 168, 118, 255), new Vector4(175, 201, 159, 255) };
        theme[8].upText = new Vector4[4] { new Vector4(219, 221, 219, 255), theme[8].background, theme[8].background, theme[8].background };
        theme[8].downText = new Vector4[2] { theme[8].upText[0], theme[8].upText[3] };

        int i, j;
        for(i = 0; i < 9; i++)
        {
            theme[i].background /= 255f;
            for(j = 0; j < 4; j++)
            {
                theme[i].circleBg[j] /= 255f;
                theme[i].upText[j] /= 255f;
                if (j < 2) theme[i].downText[j] /= 255f;
            }
        }

        currentTheme = theme[2];
        Camera.main.backgroundColor = currentTheme.background;
    }
    #endregion

    #region sudoku_ground
    public Vector3 upperCenter, lowerCenter, upperSquareScale, lowerSquareScale;
    public float upperSquareSize, lowerSquareSize;
    public GameObject[] upper = new GameObject[81];
    public GameObject[] lower = new GameObject[10];
    public GameObject[] upperBackground = new GameObject[81];
    public GameObject[] lowerBackground = new GameObject[10];
    public SpriteRenderer[] upperSpriteRenderer = new SpriteRenderer[81];
    public SpriteRenderer[] lowerSpriteRenderer = new SpriteRenderer[10];
    public SpriteRenderer[] borderSpriteRenderer = new SpriteRenderer[10];
    public struct SudokuGridText
    {
        public Text first, second;
    }
    public SudokuGridText[] upperText = new SudokuGridText[81];
    public SudokuGridText[] lowerText = new SudokuGridText[10];

    public LineRenderer[] lineRenderer = new LineRenderer[4];
    public Vector3[] linePos = new Vector3[4];
    public SpriteRenderer[,] smallLineSpriteRenderer = new SpriteRenderer[2, 54];
    #endregion
}