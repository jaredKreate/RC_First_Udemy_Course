using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour {

    public Text valueText;
    public enum ValueType { Health, Ammo, Clips };
    public ValueType valueType;
    public enum ValueStyle { Percentage, Fraction };
    public ValueStyle valueStyle;

    Image bar;

    void Start()
    {
        bar = GetComponent<Image>();

        switch (valueType)
        {
            case ValueType.Health: UpdateHealth(); break;
            case ValueType.Ammo: UpdateAmmo(); break;
            case ValueType.Clips: UpdateClips(); break;
        }
    }

    void Update()
    {
        switch (valueType)
        {
            case ValueType.Health: UpdateHealth(); break;
            case ValueType.Ammo: UpdateAmmo(); break;
            case ValueType.Clips: UpdateClips(); break;
        }
    }

    void UpdateHealth()
    {
        ProgressBar.SetFill(ref bar, PlayerData.Instance.health, PlayerData.Instance.maxHealth);
        switch(valueStyle)
        {
            case ValueStyle.Fraction:
                valueText.text = PlayerData.Instance.health.ToString() + "/" + PlayerData.Instance.maxHealth.ToString();
                break;
            case ValueStyle.Percentage:
                valueText.text = string.Format("{0:00}", ((PlayerData.Instance.health / (float)PlayerData.Instance.maxHealth) * 100).ToString() + "%");
                break;
        }
    }

    void UpdateAmmo()
    {
        ProgressBar.SetFill(ref bar, PlayerData.Instance.ammo, PlayerData.Instance.clipSize);
        switch (valueStyle)
        {
            case ValueStyle.Fraction:
                valueText.text = PlayerData.Instance.ammo.ToString() + "/" + PlayerData.Instance.clipSize.ToString();
                break;
            case ValueStyle.Percentage:
                valueText.text = string.Format("{0:00}", ((PlayerData.Instance.ammo / (float)PlayerData.Instance.clipSize) * 100).ToString() + "%");
                break;
        }
    }

    void UpdateClips()
    {
        ProgressBar.SetFill(ref bar, PlayerData.Instance.clips, PlayerData.Instance.maxClips);
        switch (valueStyle)
        {
            case ValueStyle.Fraction:
                valueText.text = PlayerData.Instance.clips.ToString() + "/" + PlayerData.Instance.maxClips.ToString();
                break;
            case ValueStyle.Percentage:
                valueText.text = string.Format("{0:00}", ((PlayerData.Instance.clips / (float)PlayerData.Instance.maxClips) * 100).ToString() + "%");
                break;
        }
    }
}
