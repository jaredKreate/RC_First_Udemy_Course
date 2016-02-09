using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitIndicators : MonoBehaviour {

    public float fadeSmooth = 15;

    Image indicator;
    Color currColor;
    float alpha;

    bool hit = false;

    void Start()
    {
        indicator = GetComponent<Image>();
        currColor = indicator.color;
        currColor.a = 0;
        indicator.color = currColor;
    }

    IEnumerator FadeOut()
    {
        while (hit)
        {
            currColor.a -= fadeSmooth * Time.deltaTime;
            if (currColor.a <= 0)
                hit = false;
            indicator.color = currColor;
            yield return new WaitForSeconds(0);
        }
    }

    void SetIndicators()
    {
        if (!hit)
        {
            currColor.a = 1;
            indicator.color = currColor;
            hit = true;
            StopCoroutine(FadeOut());
            StartCoroutine(FadeOut());
        }
    }

    void OnEnable()
    {
        Bullet.SetIndicators += SetIndicators;
    }

    void OnDisable()
    {
        Bullet.SetIndicators -= SetIndicators;
    }
}
