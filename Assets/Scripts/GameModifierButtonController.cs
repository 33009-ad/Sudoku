using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameModifierButtonController : MonoBehaviour
{
    private static GameData gameData;
    GameController gameController;
    public GameObject retryButtonObject, editButtonObject, undoButtonObject, checkoutButtonObject;

    private void Start()
    {
        gameData = GameData.GetObject();
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    public void EditPressed()
    {
        gameController.inEditMode = !gameController.inEditMode;
        if (gameController.inEditMode)
            editButtonObject.GetComponent<Image>().color = gameData.currentTheme.circleBg[3];
        else editButtonObject.GetComponent<Image>().color = Color.clear;
    }

    public void RestartGame() => gameController.StartGame(-1);

    public void Undo()
    {
        if (gameController.activityTrackerStack.Count == 0) return;
        GameController.ActivityTracker activityTracker;
        activityTracker = (GameController.ActivityTracker) gameController.activityTrackerStack.Pop();
        if(activityTracker.type == 1)
        {
            gameData.upperText[activityTracker.index].first.text = activityTracker.data;
            gameData.upperText[activityTracker.index].second.text = "";
        }
        else
        {
            gameData.upperText[activityTracker.index].second.text = activityTracker.data;
            gameData.upperText[activityTracker.index].first.text = "";
        }
    }

    public void Checkout()
    {
        int i, j;
        string str;
        bool[] isWrong = new bool[81];
        for(i = 0; i < 81; i++)
            isWrong[i] = false;

        //-------------------------------------------Inline function started
        void CustomIterator(int delta)
        {
            for (j = i + delta; j >= 0 && j / 9 == i / 9; j += delta)
            {
                if (!isWrong[j] && str == gameData.upperText[j].first.text && (delta < 0 || delta > 0 && gameController.isFixed[j]))
                {
                    print(i + " is wrong1 for " + j*delta + " and " + str);
                    isWrong[i] = true;
                    return;
                }
            }

            for (j = i + 9 * delta; j >= 0 && j < 81; j += 9 * delta)
            {
                if (!isWrong[j] && str == gameData.upperText[j].first.text && (delta < 0 || delta > 0 && gameController.isFixed[j]))
                {
                    print(i + " is wrong2 for " + j * delta + " and " + str);
                    isWrong[i] = true;
                    return;
                }
            }

            for (j = i + delta;; j += delta)
            {
                if ((j % 3 == 2 && delta < 0) || (j % 3 == 0 && delta > 0))
                    j += 6 * delta;
                if (j < 0 || i / 27 != j / 27) break;
                if (!isWrong[j] && gameData.upperText[j].first.text == str && (delta < 0 || delta > 0 && gameController.isFixed[j]))
                {
                    print(i + " is wrong3 for " + j * delta + " and " + str);
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
            CustomIterator(1);
            CustomIterator(-1);
            if (isWrong[i])
            {
                gameData.upperSpriteRenderer[i].color = Color.red;
                gameData.upperBackground[i].transform.localScale = Vector2.zero;
                gameData.upperBackground[i].transform.DOScale(Vector2.one, 0.2f);
                numberOfWrongPlacement++;
            }
        }
    }

    public void Back()
    {
        gameController.GoForward(false);
    }
}