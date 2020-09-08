using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private GameData gameData;
    private GameController gameController;
    private ThemeHandler themeHandler;

    private bool isThemeComponent;
    private int themeIndex;

    void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();

        if (gameObject.name.Contains("Theme"))
        {
            gameData = GameData.GetObject();
            themeHandler = GameObject.Find("Theme Icon Panel").GetComponent<ThemeHandler>();
            themeIndex = int.Parse(name.Substring(5));
            isThemeComponent = true;

            transform.Find("Primary Color").GetComponent<SpriteRenderer>().color = gameData.theme[themeIndex].background;
            transform.Find("Secondary Color").GetComponent<SpriteRenderer>().color = gameData.theme[themeIndex].circleBg[3];
        }
        else isThemeComponent = false;
    }

    private void OnMouseDown()
    {
        if (isThemeComponent)
        {
            gameData.currentTheme = gameData.theme[themeIndex];
            gameController.SetTheme(true);
        }
        else gameController.ButtonClicked(name);
    }
}
