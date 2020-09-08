using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    GameController gameController;
    public Text levelText;
    int selectedLevel = 0;
    private enum Level
    {
        Easy = 55,
        Medium = 40,
        Hard = 25
    }
    int[] levelArr = (int[]) System.Enum.GetValues(typeof(Level));
    void Start()
    {
        System.Array.Reverse(levelArr);
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        levelText.text = System.Enum.GetName(typeof(Level), levelArr[selectedLevel]);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void ChangeLevel(int delta)
    {
        selectedLevel += delta;
        if (selectedLevel < 0) selectedLevel = 0;
        else if (selectedLevel == System.Enum.GetValues(typeof(Level)).Length) selectedLevel--;
        levelText.text = System.Enum.GetName(typeof(Level), levelArr[selectedLevel]);
    }

    public void StartGame(){
        gameController.StartGame(levelArr[selectedLevel]);
    }
}
/*
81 - 24(57) beginner
81 - 37(44) easy
81 - 46(35) medium
81 - 55(24) hard
81 - 56(25) extreme
*/
