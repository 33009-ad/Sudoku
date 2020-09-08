using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThemeHandler : MonoBehaviour
{
    public RectTransform themePanelRT, themePanelBGRT;
    private bool themeButtonPressed = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = new Vector3(4.5f + 10 * Screen.width / Screen.height, 0, -0.2f);
        themePanelBGRT.position = new Vector3(0, 0, -0.2f);
        themePanelBGRT.sizeDelta = new Vector2(9, 1.5f);
        themePanelBGRT.transform.localScale = new Vector2(1, 0);
        themePanelRT.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        themeButtonPressed = !themeButtonPressed;
        if (themeButtonPressed)
        {
            themePanelBGRT.DOScaleY(1, 0.3f).OnComplete(() => {
                themePanelRT.gameObject.SetActive(true);
                themePanelRT.transform.position = startPosition;
            });
        }
        else
        {
            themePanelRT.DOMoveX(-startPosition.x, 0.5f).OnComplete(() =>
                {
                    themePanelRT.gameObject.SetActive(false);
                    themePanelBGRT.DOScaleY(0, 0.3f);
                });
        }
    }
}
