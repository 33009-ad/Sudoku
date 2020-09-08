using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : GameControllerHelper
{
    public GameObject worldSpace, staticWorldSpace, gamePanel, startPanel;
    public bool inEditMode = false;
    public int selectedMode = 0, selectedIndex, buttonIndex;
    private int[] cnt = new int[10];
    private int remainUnfilled;
    public Text gamePanelTime;
    private Coroutine coroutineTime;
    public struct ActivityTracker
    {
        public int type, index;
        public string data;
    }
    private ActivityTracker activityTracker;
    public Stack activityTrackerStack = new Stack();


    void Start()
    {
        float worldHeightScale = Camera.main.orthographicSize * 2;
        RectTransform rt = worldSpace.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Screen.width / (float)Screen.height * worldHeightScale, worldHeightScale);
        staticWorldSpace.GetComponent<RectTransform>().sizeDelta = rt.sizeDelta;
        Start1();

        gameData.upperSquareSize = Mathf.Min(rt.sizeDelta.x * 0.98f, rt.sizeDelta.y * 0.62f) / 9;
        gameData.lowerSquareSize = Mathf.Min(rt.sizeDelta.x * 0.95f / 5, rt.sizeDelta.y * 0.22f / 2);
        gameData.upperSquareScale = new Vector3(gameData.upperSquareSize, gameData.upperSquareSize);
        gameData.lowerSquareScale = new Vector2(gameData.lowerSquareSize, gameData.lowerSquareSize);

        CreateGrid();
        GoForward(false);
        SetTheme(false);
    }

    public void StartGame(int n)
    {
        selectedMode = 0;
        if(coroutineTime != null) StopCoroutine(coroutineTime);
        coroutineTime = StartCoroutine(TimeCounter(gamePanelTime));
        GoForward(true);
        activityTrackerStack.Clear();
        LoadNewGame(n);
        SetTheme(false);
        if (n > 1)
        {
            HighlightBlocks(0, -1, false);
            AnimateLine(0.5f);
        }
        else
        {
            HighlightBlocks(0, -1, false);
            AnimateUpperGround(0.5f);
        }
    }

    public void GoForward(bool flag)
    {
        if (!flag && coroutineTime != null) StopCoroutine(coroutineTime);
        startPanel.SetActive(!flag);
        worldSpace.SetActive(flag);
        gamePanel.SetActive(flag);
    }

    #region SetTheme
    [Header("For Theme")]
    public Image backButtonImg;
    public Image retryButtonImg;
    public Image checkoutButtonImg;
    public Image editButtonImg;
    public Image editButtonBgImg;
    public Image undoButtonImg;
    public Image smallLineImg;
    public Image themeSelectorPanelBG;
    public Image startButtonBG;
    public Image themeIcon;
    public Text sudokuFront;
    public Text timeText;
    public Text startButtonText;
    public Text levelText;

    public void SetTheme(bool animate)
    {
        selectedMode = 0;
        int i, j;
        float duration = 0.5f;
        if (animate)
        {
            Camera.main.DOColor(gameData.currentTheme.background, duration);
            sudokuFront.DOColor(gameData.currentTheme.upText[0], duration);
            timeText.DOColor(gameData.currentTheme.upText[0], duration);
            backButtonImg.DOColor(gameData.currentTheme.upText[0], duration);
            retryButtonImg.DOColor(gameData.currentTheme.upText[0], duration);
            checkoutButtonImg.DOColor(gameData.currentTheme.upText[0], duration);
            editButtonImg.DOColor(gameData.currentTheme.upText[0], duration);
            if (inEditMode) editButtonBgImg.DOColor(gameData.currentTheme.circleBg[3], duration);
            themeSelectorPanelBG.DOColor(gameData.currentTheme.circleBg[1], duration);
            undoButtonImg.DOColor(gameData.currentTheme.upText[0], duration);
            lineMat.DOColor(gameData.currentTheme.circleBg[3], duration);
            startButtonBG.DOColor(gameData.currentTheme.circleBg[2], duration);
            startButtonText.DOColor(gameData.currentTheme.upText[3], duration);
            themeIcon.DOColor(gameData.currentTheme.upText[0], duration);
            levelText.DOColor(gameData.currentTheme.upText[0], duration);

            HighlightBlocks(0, -1, true);
            for (i = 0; i < 10; i++)
            {
                HighlightHelper(i, false, 0);
            }
        }
        else
        {
            Camera.main.backgroundColor = gameData.currentTheme.background;
            sudokuFront.color = gameData.currentTheme.upText[0];
            timeText.color = gameData.currentTheme.upText[0];
            backButtonImg.color = gameData.currentTheme.upText[0];
            retryButtonImg.color = gameData.currentTheme.upText[0];
            checkoutButtonImg.color = gameData.currentTheme.upText[0];
            editButtonImg.color = gameData.currentTheme.upText[0];
            if(inEditMode) editButtonBgImg.color = gameData.currentTheme.circleBg[3];
            themeSelectorPanelBG.color = gameData.currentTheme.circleBg[1];
            undoButtonImg.color = gameData.currentTheme.upText[0];
            lineMat.color = gameData.currentTheme.circleBg[3];
            startButtonBG.color = gameData.currentTheme.circleBg[2];
            startButtonText.color = gameData.currentTheme.upText[3];
            themeIcon.color = gameData.currentTheme.upText[0];
            levelText.color = gameData.currentTheme.upText[0];

            HighlightBlocks(0, -1, false);
            for (i = 0; i < 10; i++)
            {
                HighlightHelper(i, false, 0);
            }
        }

        for(i = 0; i < 2; i++)
            for (j = 0; j < 54; j++)
                gameData.smallLineSpriteRenderer[i, j].color = gameData.currentTheme.circleBg[1];
    }
    #endregion

    public void ButtonClicked(string buttonName)
    {
        buttonIndex = int.Parse(buttonName.Substring(2));
        switch (buttonName[0])
        {
            case 'e':
                if(selectedMode == 2)
                {
                    if (isFixed[buttonIndex]) return;
                    bool contains = false;
                    if (inEditMode)
                    {
                        string str = gameData.upperText[buttonIndex].first.text;
                        activityTracker.data = str;
                        str = AddData(str, selectedIndex, ref contains);
                        gameData.upperText[buttonIndex].second.text = str;
                        gameData.upperText[buttonIndex].first.text = "";
                        if(str != activityTracker.data)
                        {
                            activityTracker.type = 2;
                            activityTracker.index = buttonIndex;
                            activityTrackerStack.Push(activityTracker);
                        }
                    }
                    else
                    {
                        if(gameData.upperText[buttonIndex].first.text == "")
                        {
                            activityTracker.data = gameData.upperText[buttonIndex].second.text;
                            activityTracker.type = 2;
                        }
                        else
                        {
                            activityTracker.data = gameData.upperText[buttonIndex].first.text;
                            activityTracker.type = 1;

                            Text aText = gameData.lowerText[int.Parse(activityTracker.data)].second;
                            aText.text = (aText.text == "-1" ? "" : ((aText.text == "" ? 0 : int.Parse(aText.text)) + 1).ToString());
                        }
                        activityTracker.index = buttonIndex;
                        activityTrackerStack.Push(activityTracker);

                        if (gameData.upperText[buttonIndex].first.text == selectedIndex.ToString())
                        {
                            gameData.upperText[buttonIndex].first.text = "";
                            contains = false;
                        }
                        else
                        {
                            gameData.upperText[buttonIndex].first.text = selectedIndex.ToString();
                            contains = true;
                        }
                        gameData.upperText[buttonIndex].second.text = "";

                        if (gameData.upperText[buttonIndex].first.text != "")
                        {
                            Text aText = gameData.lowerText[selectedIndex].second;
                            aText.text = (aText.text == "1" ? "" : ((aText.text == "" ? 0 : int.Parse(aText.text)) - 1).ToString());
                        }
                    }
                    if (contains) HighlightHelper(buttonIndex, true, 1, 0);
                    else HighlightHelper(buttonIndex, true, 0, 0);
                }
                else
                {
                    if((selectedMode != 0 && gameData.upperText[buttonIndex].first.text != "" && gameData.upperText[buttonIndex].first.text == gameData.upperText[selectedIndex].first.text) ||
                        (selectedMode == 1 && selectedIndex == buttonIndex))
                    {
                        selectedMode = 0;
                        HighlightBlocks(0, -1, true);
                    }
                    else
                    {
                        if (gameData.upperText[buttonIndex].first.text != "")
                        {
                            if (isFixed[buttonIndex])
                            {
                                selectedMode = 3;
                                HighlightHelper(buttonIndex, true, 2, 0);
                            }
                            else
                            {
                                selectedMode = 1;
                                HighlightHelper(buttonIndex, true, 3, 0);
                            }
                            selectedIndex = buttonIndex;
                            HighlightBlocks(int.Parse(gameData.upperText[buttonIndex].first.text), buttonIndex, true);
                        }
                        else
                        {
                            selectedMode = 1;
                            selectedIndex = buttonIndex;
                            HighlightHelper(buttonIndex, true, 3, 0);
                            HighlightBlocks(0, buttonIndex, true);
                        }
                    }
                }
                break;

            case 'i':
                if(buttonIndex == 0)
                {
                    if (selectedMode != 1) return;
                    if (gameData.upperText[selectedIndex].first.text == "")
                    {
                        activityTracker.data = gameData.upperText[selectedIndex].second.text;
                        activityTracker.type = 2;
                    }
                    else
                    {
                        activityTracker.data = gameData.upperText[selectedIndex].first.text;
                        activityTracker.type = 1;
                        int ind = int.Parse(gameData.upperText[selectedIndex].first.text);
                        string str = gameData.lowerText[ind].second.text;
                        int newValue = (str == "" ? 0 : int.Parse(str));
                        gameData.lowerText[ind].second.text = "" + (newValue == -1 ? "" : (newValue + 1).ToString()) ;
                    }
                    activityTracker.index = selectedIndex;
                    activityTrackerStack.Push(activityTracker);

                    gameData.upperText[selectedIndex].first.text = "";
                    gameData.upperText[selectedIndex].second.text = "";
                }
                else //---------------- for all lower ground buttons
                {
                    if(selectedMode == 1)
                    {
                        gameData.upperSpriteRenderer[selectedIndex].transform.DOScale(Vector2.one * 1.2f, 0.1f).OnComplete(() =>
                            gameData.upperSpriteRenderer[selectedIndex].transform.DOScale(Vector2.one, 0.1f));
                        bool contains = false;
                        if (inEditMode)
                        {
                            string str = gameData.upperText[selectedIndex].second.text;
                            activityTracker.data = str;
                            str = AddData(str, buttonIndex, ref contains);
                            gameData.upperText[selectedIndex].second.text = str;
                            gameData.upperText[selectedIndex].first.text = "";
                            if(str != activityTracker.data)
                            {
                                activityTracker.type = 2;
                                activityTracker.index = selectedIndex;
                                activityTrackerStack.Push(activityTracker);
                            }
                        }
                        else
                        {
                            if (gameData.upperText[selectedIndex].first.text == "")
                            {
                                activityTracker.data = gameData.upperText[selectedIndex].second.text;
                                activityTracker.type = 2;
                            }
                            else
                            {
                                activityTracker.data = gameData.upperText[selectedIndex].first.text;
                                activityTracker.type = 1;
                            }
                            activityTracker.index = selectedIndex;
                            activityTrackerStack.Push(activityTracker);

                            if (gameData.upperText[selectedIndex].first.text == buttonIndex.ToString())
                            {
                                gameData.upperText[selectedIndex].first.text = "";
                                contains = false;
                            }
                            else
                            {
                                gameData.upperText[selectedIndex].first.text = buttonIndex.ToString();
                                contains = true;
                            }
                            gameData.upperText[selectedIndex].second.text = "";
                        }
                        if (contains) HighlightBlocks(buttonIndex, selectedIndex, true);
                        else HighlightBlocks(0, selectedIndex, true);
                    }
                    else if (selectedMode == 2)
                    {
                        HighlightHelper(selectedIndex, false, 0);
                        if (selectedIndex != buttonIndex)
                        {
                            selectedIndex = buttonIndex;
                            HighlightHelper(buttonIndex, false, 1);
                            HighlightBlocks(buttonIndex, -1, true);
                        }
                        else
                        {
                            selectedMode = 0;
                            HighlightBlocks(0, -1, true);
                        }
                    }
                    else
                    {
                        selectedMode = 2;
                        selectedIndex = buttonIndex;
                        HighlightHelper(buttonIndex, false, 1);
                        HighlightBlocks(buttonIndex, -1, true);
                    }
                }
                break;
        }
    }

    private void HighlightBlocks(int n, int index, bool animate)
    {
        char ch = n.ToString().ElementAt(0);
        int i, j, l;
        string str1, str2;
        for (i = 0; i < 10; i++) cnt[i] = 9;
        for (i = 0; i < 81; i++)
        {
            str1 = gameData.upperText[i].first.text;
            if(str1 != "") cnt[int.Parse(str1)]--;
            if (i == index) continue;
            if (n == 0)
            {
                if (animate) HighlightHelper(i, true, 0, i % 7 / 10f);
                else HighlightHelper(i, true, 0);
                continue;
            }
            str2 = gameData.upperText[i].second.text;
            l = str2.Length;
            if(str1 == n.ToString())
            {
                if (animate) HighlightHelper(i, true, 1, i % 7 / 10f);
                else HighlightHelper(i, true, 1);
                continue;
            }
            for(j = 0; j < l; j++)
            {
                if(str2[j] == ch)
                {
                    if (animate) HighlightHelper(i, true, 1, i % 7 / 10f);
                    else HighlightHelper(i, true, 1);
                    break;
                }
            }
            if (j == l)
            {
                if (animate) HighlightHelper(i, true, 0, i % 7 / 10f);
                else HighlightHelper(i, true, 0);
            }
        }
        j = 0; //using as a counter
        for (i = 1; i < 10; i++)
        {
            if (cnt[i] == 0) gameData.lowerText[i].second.text = "";
            else
            {
                j++;
                gameData.lowerText[i].second.text = cnt[i].ToString();
            }
        }
        if(j == 0 && CheckoutModed() == 0)
        {
            StopAllCoroutines();
            gamePanelTime.text = "Congratulations!";
        }
    }

    public int CheckoutModed()
    {
        int i, j;
        string str;
        bool[] isWrong = new bool[81];
        for (i = 0; i < 81; i++)
            isWrong[i] = false;

        //-------------------------------------------Inline function started
        void CustomIteratorModed(int delta)
        {
            for (j = i + delta; j >= 0 && j / 9 == i / 9; j += delta)
            {
                if (!isWrong[j] && str == gameData.upperText[j].first.text && (delta < 0 || delta > 0 && isFixed[j]))
                {
                    isWrong[i] = true;
                    return;
                }
            }

            for (j = i + 9 * delta; j >= 0 && j < 81; j += 9 * delta)
            {
                if (!isWrong[j] && str == gameData.upperText[j].first.text && (delta < 0 || delta > 0 && isFixed[j]))
                {
                    isWrong[i] = true;
                    return;
                }
            }

            for (j = i + delta; ; j += delta)
            {
                if ((j % 3 == 2 && delta < 0) || (j % 3 == 0 && delta > 0))
                    j += 6 * delta;
                if (j < 0 || i / 27 != j / 27) break;
                if (!isWrong[j] && gameData.upperText[j].first.text == str && (delta < 0 || delta > 0 && isFixed[j]))
                {
                    isWrong[i] = true;
                    return;
                }
            }
        }//-------------------------------------------Inline function ended

        int numberOfWrongPlacement = 0;
        for (i = 0; i < 81; i++)
        {
            str = gameData.upperText[i].first.text;
            if (str == "") continue;
            CustomIteratorModed(1);
            CustomIteratorModed(-1);
            if (isWrong[i])
                numberOfWrongPlacement++;
        }
        return numberOfWrongPlacement;
    }
}