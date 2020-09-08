using UnityEngine;

public class GarbageScript
{
 /*   public bool IsBlocksOk(int index)
    {
        int currentNumber, i;
        currentNumber = sudokuInputGrid[index];
        for (i = index - 9; i >= 0; i -= 9) //to check previous rows in same column
            if (sudokuInputGrid[i] == currentNumber)
                return false;
        for (i = index - 1; i >= 0 && (index / 9) == (i / 9); i--)
            if (sudokuInputGrid[i] == currentNumber)
                return false;
        for (i = index - 1; ; i--)
        {
            if (i % 3 == 2)
                i -= 6;
            if (i < 0 || index / 27 != i / 27) break;
            if (sudokuInputGrid[i] == currentNumber)
                return false;
        }
        return true;
    }*/

  /*  private IEnumerator ScaleChangeBackground(SpriteRenderer sr, Vector2 circleBackgroundSize, float wait, float duration)
    {
        if (wait > 0) yield return new WaitForSeconds(wait);
        float startTime = Time.time, processed;
        while (true)
        {
            processed = (Time.time - startTime) / duration;
            sr.size = Vector2.Lerp(Vector2.zero, circleBackgroundSize, processed);
            if (processed >= 1) break;
            yield return null;
        }
        StartCoroutine(VibrateScale(sr, circleBackgroundSize, duration / 10));
    }*/

 /*   private IEnumerator VibrateScale(SpriteRenderer sr, Vector2 circleBackgroundSize, float duration)
    {
        float startTime = Time.time, processed;
        Vector2 startSize = circleBackgroundSize, endSize = circleBackgroundSize * 1.1f;
        while (true)
        {
            processed = (Time.time - startTime) / duration;
            sr.size = Vector2.Lerp(startSize, endSize, processed);
            if (processed >= 1)
            {
                if (startSize == circleBackgroundSize)
                {
                    startTime = Time.time;
                    (startSize, endSize) = (endSize, startSize);
                }
                else break;
            }
            yield return null;
        }
    }*/
}
