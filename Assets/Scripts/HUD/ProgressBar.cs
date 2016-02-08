using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public static void SetFill(ref Image bar, int current, int max)
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, current / (float)max, 5 * Time.deltaTime);
    }
    public static void SetFillInstant(ref Image bar, int current, int max)
    {
        bar.fillAmount = current / (float)max;
    }
}
