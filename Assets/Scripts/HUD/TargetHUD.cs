using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetHUD : MonoBehaviour {

    public Text targetName;
    public Image targetHealthBar;
    public Text targetHealth;
    public float maxActiveTime = 3;
    public float fadeSmooth = 8;

    float activeTimer = 0;
    float canvasAlpha = 0;
    CanvasGroup canvas;
    EnemyData target;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = canvasAlpha;
    }

    bool ActiveTimeIsUp()
    {
        activeTimer += Time.deltaTime;
        if (activeTimer > maxActiveTime)
            return true;
        return false;
    }

    IEnumerator FadeTo(float alpha)
    {
        while (Mathf.Abs(canvasAlpha - alpha) > 0.01f)
        {
            canvasAlpha = Mathf.Lerp(canvasAlpha, alpha, fadeSmooth * Time.deltaTime);
            canvas.alpha = canvasAlpha;
            yield return new WaitForSeconds(0);
        }
    }

    void Update()
    {
        if (target && canvasAlpha > 0.01f)
        {
            ProgressBar.SetFill(ref targetHealthBar, target.health, target.maxHealth);
            targetHealth.text = string.Format("{0:00}", ((target.health / (float)target.maxHealth) * 100).ToString() + "%");

        }
        else if (!target)
        {
            ProgressBar.SetFill(ref targetHealthBar, 0, 100);
            targetHealth.text = string.Format("{0:00}", ((0 / 100) * 100).ToString() + "%");
            StopAllCoroutines();
            StartCoroutine(FadeTo(0));
        }
        if (ActiveTimeIsUp() && target)
        {
            StopAllCoroutines();
            StartCoroutine(FadeTo(0));
        }
    }

    void OnEnable()
    {
        Bullet.ActivateTarget += ActivateTarget;
    }

    void OnDisable()
    {
        Bullet.ActivateTarget -= ActivateTarget;
    }

    void ActivateTarget(EnemyData enemyData)
    {
        if (target != enemyData)
            ProgressBar.SetFillInstant(ref targetHealthBar, enemyData.health, enemyData.maxHealth);
        target = enemyData;
        targetName.text = target.name;
        StopAllCoroutines();
        StartCoroutine(FadeTo(1));
        activeTimer = 0;
        
    }

    
}
