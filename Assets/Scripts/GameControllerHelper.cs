using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class GameControllerHelper : MonoBehaviour
{
    [System.NonSerialized]
    public GameData gameData;

    public void Start1()
    {
        gameData = GameData.GetObject();
        gameData.upperCenter = upperTransform.position;
        gameData.lowerCenter = lowerTransform.position;
    }

    #region sudoku ground creation
    public GameObject upperPrefab, lowerPrefab, whiteLinePrefab;
    public Transform upperTransform, lowerTransform, lineTransform;
    public Material lineMat;
    public void CreateGrid()
    {
        int i, j;
        gameData.upper[0] = Instantiate(upperPrefab);
        gameData.upper[0].transform.parent = upperTransform;
        gameData.upper[0].transform.localScale = gameData.upperSquareScale;
        gameData.upper[0].transform.position = gameData.upperCenter + Vector3.up * gameData.upperSquareSize * 4 + Vector3.left * gameData.upperSquareSize * 4;
        gameData.upperText[0].first =  gameData.upper[0].transform.Find("First").GetComponent<Text>();
        gameData.upperText[0].second =  gameData.upper[0].transform.Find("Second").GetComponent<Text>();
        gameData.upperBackground[0] = gameData.upper[0].transform.Find("Background").gameObject;
        gameData.upperSpriteRenderer[0] = gameData.upperBackground[0].GetComponent<SpriteRenderer>();
        gameData.upper[0].name = "e-00";
        for(i = 1; i < 81; i++)
        {
            gameData.upper[i] = Instantiate(gameData.upper[0]);
            gameData.upper[i].transform.parent = upperTransform;
            gameData.upper[i].transform.position = gameData.upperCenter + Vector3.up * gameData.upperSquareSize * (4 - i / 9) + Vector3.left * gameData.upperSquareSize * (4 - i % 9);
            gameData.upperText[i].first = gameData.upper[i].transform.Find("First").GetComponent<Text>();
            gameData.upperText[i].second = gameData.upper[i].transform.Find("Second").GetComponent<Text>();
            gameData.upperBackground[i] = gameData.upper[i].transform.Find("Background").gameObject;
            gameData.upperSpriteRenderer[i] = gameData.upperBackground[i].GetComponent<SpriteRenderer>();
            gameData.upper[i].name = "e-" + i.ToString().PadLeft(2, '0');
        }

        gameData.lower[0] = Instantiate(lowerPrefab);
        gameData.lower[0].transform.parent = lowerTransform;
        gameData.lower[0].transform.localScale = gameData.lowerSquareScale;
        gameData.lower[0].transform.position = gameData.lowerCenter + Vector3.down * gameData.lowerSquareSize * 0.5f + Vector3.right * gameData.lowerSquareSize * 2;
        gameData.lowerText[0].first = gameData.lower[0].transform.Find("First").GetComponent<Text>();
        gameData.lowerText[0].second = gameData.lower[0].transform.Find("Second").GetComponent<Text>();
        gameData.lowerBackground[0] = gameData.lower[0].transform.Find("Background").gameObject;
        gameData.lowerSpriteRenderer[0] = gameData.lowerBackground[0].GetComponent<SpriteRenderer>();
        gameData.borderSpriteRenderer[0] = gameData.lower[0].transform.Find("Circle Border").GetComponent<SpriteRenderer>();
        gameData.lower[0].name = "i-0";
        gameData.lowerText[0].first.text = "X";
        gameData.lowerText[0].second.text = "";
        for (i = 0; i < 9; i++)
        {
            j = i + 1;
            gameData.lower[j] = Instantiate(gameData.lower[0]);
            gameData.lower[j].transform.parent = lowerTransform;
            gameData.lower[j].transform.position = gameData.lowerCenter + Vector3.up * gameData.lowerSquareSize * (0.5f - i / 5) + Vector3.left * gameData.lowerSquareSize * (2 - i % 5);
            gameData.lowerText[j].first = gameData.lower[j].transform.Find("First").GetComponent<Text>();
            gameData.lowerText[j].second = gameData.lower[j].transform.Find("Second").GetComponent<Text>();
            gameData.lowerBackground[j] = gameData.lower[j].transform.Find("Background").gameObject;
            gameData.lowerSpriteRenderer[j] = gameData.lowerBackground[j].GetComponent<SpriteRenderer>();
            gameData.borderSpriteRenderer[j] = gameData.lower[j].transform.Find("Circle Border").GetComponent<SpriteRenderer>();
            gameData.lower[j].name = "i-" + j;
            gameData.lowerText[j].first.text = j.ToString();
        }

        for (i = 0; i < 4; i++)
        {
            gameData.lineRenderer[i] = new GameObject("Line-" + (i + 1)).AddComponent<LineRenderer>();
            gameData.lineRenderer[i].transform.position = Vector3.zero;
            gameData.lineRenderer[i].positionCount = 2;
            gameData.lineRenderer[i].transform.parent = lineTransform;
            gameData.lineRenderer[i].startWidth = 0.02f;
            gameData.lineRenderer[i].endWidth = 0.02f;
            gameData.lineRenderer[i].material = lineMat;
        }
        gameData.lineRenderer[0].SetPosition(0, gameData.upperCenter + Vector3.up * gameData.upperSquareSize * 1.5f + Vector3.left * gameData.upperSquareSize * 4.5f);
        gameData.lineRenderer[1].SetPosition(0, gameData.upperCenter + Vector3.down * gameData.upperSquareSize * 4.5f + Vector3.left * gameData.upperSquareSize * 1.5f);
        gameData.lineRenderer[2].SetPosition(0, gameData.upperCenter + Vector3.down * gameData.upperSquareSize * 1.5f + Vector3.right * gameData.upperSquareSize * 4.5f);
        gameData.lineRenderer[3].SetPosition(0, gameData.upperCenter + Vector3.up * gameData.upperSquareSize * 4.5f + Vector3.right * gameData.upperSquareSize * 1.5f);
        gameData.linePos[0] = gameData.upperCenter + Vector3.up * gameData.upperSquareSize * 1.5f + Vector3.right * gameData.upperSquareSize * 4.5f;
        gameData.linePos[1] = gameData.upperCenter + Vector3.up * gameData.upperSquareSize * 4.5f + Vector3.left * gameData.upperSquareSize * 1.5f;
        gameData.linePos[2] = gameData.upperCenter + Vector3.down * gameData.upperSquareSize * 1.5f + Vector3.left * gameData.upperSquareSize * 4.5f;
        gameData.linePos[3] = gameData.upperCenter + Vector3.down * gameData.upperSquareSize * 4.5f + Vector3.right * gameData.upperSquareSize * 1.5f;

        Vector2 posA, posB;
        int ind;
        GameObject a, b;
        for (i = 0; i < 8; i++)
        {
            if (i % 3 == 2) continue;
            posA = gameData.upperCenter + Vector3.left * gameData.upperSquareSize * 4 + Vector3.up * gameData.upperSquareSize * (3.5f - i);
            posB = gameData.upperCenter + Vector3.up * gameData.upperSquareSize * 4 + Vector3.left * gameData.upperSquareSize * (3.5f - i);
            for (j = 0; j < 9; j++)
            {
                ind = (i - i / 3) * 9 + j;
                a = Instantiate(whiteLinePrefab, posA, Quaternion.identity);
                b = Instantiate(whiteLinePrefab, posB, Quaternion.Euler(0, 0, 90));
                gameData.smallLineSpriteRenderer[0, ind] = a.GetComponent<SpriteRenderer>();
                gameData.smallLineSpriteRenderer[1, ind] = b.GetComponent<SpriteRenderer>();
                a.transform.localScale = Vector2.one;
                b.transform.localScale = Vector2.one;
                a.transform.parent = lineTransform;
                b.transform.parent = lineTransform;
                posA += Vector2.right * gameData.upperSquareSize;
                posB += Vector2.down * gameData.upperSquareSize;
            }
        }
    }
    #endregion

    #region load new game
    int numberOfHiddenNumbers = 40;
    public bool[] isFixed = new bool[81];
    public void LoadNewGame(int numberOfHiddenNumbers = -1)
    {
        int i, j, ind;
        int[] numArr = new int[81];
        if (numberOfHiddenNumbers >= 0) this.numberOfHiddenNumbers = numberOfHiddenNumbers;
        else numberOfHiddenNumbers = this.numberOfHiddenNumbers;

        for (i = 0; i < 81; i++)
        {
            isFixed[i] = false;
            numArr[i] = i;
        }
        for (i = 0; i < numberOfHiddenNumbers; i++)
        {
            j = Random.Range(i, 81);
            (numArr[i], numArr[j]) = (numArr[j], numArr[i]);
            isFixed[numArr[i]] = true;
        }
        ind = Random.Range(0, gameData.numberOfSolutions);

        for (i = 0; i < 81; i++)
        {
            if (isFixed[i])
            {
                gameData.upperText[i].first.text = gameData.solutionArr[ind, i] + "";
            }
            else
            {
                gameData.upperText[i].first.text = "";
            }
            gameData.upperText[i].second.text = "";
        }
    }
    #endregion

    #region line animation
    public void AnimateLine(float duration)
    {
        int i;
        for (i = 0; i < 81; i++)
        {
            gameData.upperText[i].first.color = Color.clear;
            gameData.upperText[i].second.color = Color.clear;
            gameData.upperBackground[i].transform.localScale = Vector2.zero;
        }
        for (i = 0; i < 4; i++)
            StartCoroutine(Co_AnimateLine(i, duration));
    }
    private IEnumerator Co_AnimateLine(int ind, float duration)
    {
        float processed, startTime = Time.time;
        while (true)
        {
            processed = (Time.time - startTime) / duration;
            gameData.lineRenderer[ind].SetPosition(1, Vector3.Lerp(gameData.lineRenderer[ind].GetPosition(0), gameData.linePos[ind], processed));
            if (processed >= 1) break;
            yield return null;
        }
        if(ind == 0) AnimateUpperGround(0.5f);
    }
    #endregion

    #region sudoku upper ground animation
    public void AnimateUpperGround(float duration)
    {
        int i;
        for(i = 0; i < 81; i++)
        {
            if(isFixed[i])
            {
                gameData.upperBackground[i].transform.localScale = Vector3.zero;
                gameData.upperText[i].first.color = Color.clear;
                StartCoroutine(AnimateAfterWaitingPeriod(i, int.Parse(gameData.upperText[i].first.text) / 15f));
            }
        }
    }
    private IEnumerator AnimateAfterWaitingPeriod(int index, float duration)
    {
        yield return new WaitForSeconds(duration);
        gameData.upperBackground[index].transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack, 2f).OnComplete(() => gameData.upperText[index].first.DOColor(gameData.currentTheme.upText[1], 0.2f));
    }
    #endregion

    #region time counter coroutine
    public IEnumerator TimeCounter(Text gamePanelTime)
    {
        int hour = 0, min = 0, sec = 0;
        while (true)
        {
            gamePanelTime.text = (hour > 0 ? hour + "H " : "") + (hour + min > 0 ? min + "M " : "") + sec + "S";
            yield return new WaitForSeconds(1);
            sec++;
            if (sec == 60)
            {
                min++;
                sec = 0;
            }
        }
    }
    #endregion

    #region function of adding data in edit mode
    public string AddData(string str, int n, ref bool contains)
    {
        contains = false;
        if (str.Contains(n.ToString()))
        {
            string newStr = "";
            foreach (char ch in str)
            {
                if (ch != ' ' && ch != n + 48)
                    newStr += ch;
            }
            return (newStr.Length < 5 ? newStr : newStr.Substring(0, 2) + ' ' + newStr.Substring(2));
        }
        if (str.Length > 8) return str;
        contains = true;
        if (str.Length != 4)
            return (str + n);
        return (str.Substring(0, 2) + ' ' + str.Substring(2) + n);
    }
    #endregion

    #region highlight helper function
    public void HighlightHelper(int index, bool isUpper, int brightnessLevel)
    {
        if (isUpper)
        {
            if (isFixed[index])
            {
                if (brightnessLevel == 0)
                {
                    gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[1];
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[1];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[1];
                }
                else
                {
                    gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[2];
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[2];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[2];
                }
            }
            else
            {
                if (brightnessLevel == 0)
                {
                    gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[0];
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[0];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[0];
                }
                else if (brightnessLevel == 1)
                {
                    gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[2];
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[2];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[2];
                }
                else
                {
                    gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[3];
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[3];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[3];
                }
            }
        }
        else
        {
            if (brightnessLevel == 0)
            {
                gameData.lowerSpriteRenderer[index].color = gameData.currentTheme.circleBg[0];
                gameData.borderSpriteRenderer[index].color = gameData.currentTheme.upText[0];
                gameData.lowerText[index].first.color = gameData.currentTheme.downText[0];
                gameData.lowerText[index].second.color = gameData.currentTheme.downText[0];
            }
            else
            {
                gameData.lowerSpriteRenderer[index].color = gameData.currentTheme.circleBg[3];
                gameData.borderSpriteRenderer[index].color = Color.clear;
                gameData.lowerText[index].first.color = gameData.currentTheme.downText[1];
                gameData.lowerText[index].second.color = gameData.currentTheme.downText[1];
            }
        }
    }
    //----------------------------------------------------------------------------both class are same axcept animation duration
    public void HighlightHelper(int index, bool isUpper, int brightnessLevel, float waitingPeriod)
    {
        new WaitForSeconds(waitingPeriod);
        float animationDuration = 0.2f;
        if (isUpper)
        {
            if (isFixed[index])
            {
                if (brightnessLevel == 0)
                {
                    gameData.upperSpriteRenderer[index].DOColor(gameData.currentTheme.circleBg[1], animationDuration * 2);
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[1];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[1];
                }
                else
                {
                    gameData.upperSpriteRenderer[index].DOColor(gameData.currentTheme.circleBg[2], animationDuration * 2);
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[2];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[2];
                }
            }
            else
            {
                if (brightnessLevel == 0)
                {
                    gameData.upperBackground[index].transform.DOScale(Vector2.zero, animationDuration).OnComplete(() => {
                        gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[0];
                        gameData.upperText[index].first.color = gameData.currentTheme.upText[0];
                        gameData.upperText[index].second.color = gameData.currentTheme.upText[0];
                    });
                }
                else if (brightnessLevel == 1)
                {
                    gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[2];
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[2];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[2];
                    gameData.upperBackground[index].transform.localScale = Vector3.zero;
                    gameData.upperBackground[index].transform.DOScale(Vector2.one, animationDuration);
                }
                else
                {
                    gameData.upperSpriteRenderer[index].color = gameData.currentTheme.circleBg[3];
                    gameData.upperText[index].first.color = gameData.currentTheme.upText[3];
                    gameData.upperText[index].second.color = gameData.currentTheme.upText[3];
                    gameData.upperBackground[index].transform.localScale = Vector3.zero;
                    gameData.upperBackground[index].transform.DOScale(Vector2.one, animationDuration);
                }
            }
        }
        else
        {
            if (brightnessLevel == 0)
            {
                gameData.lowerSpriteRenderer[index].color = gameData.currentTheme.circleBg[0];
                gameData.borderSpriteRenderer[index].color = gameData.currentTheme.upText[0];
                gameData.lowerText[index].first.color = gameData.currentTheme.downText[0];
                gameData.lowerText[index].second.color = gameData.currentTheme.downText[0];
            }
            else
            {
                gameData.lowerSpriteRenderer[index].color = gameData.currentTheme.circleBg[3];
                gameData.borderSpriteRenderer[index].color = Color.clear;
                gameData.lowerText[index].first.color = gameData.currentTheme.downText[1];
                gameData.lowerText[index].second.color = gameData.currentTheme.downText[1];
            }
        }
    }
    #endregion
}
