using UnityEngine;
using UnityEngine.UI;

public class HappinessSlider : MonoBehaviour
{
    public Slider slider;
    public Image sliderHandleSprite;
    public Sprite unhappySprite, neutralSprite, happySprite;

    public enum SliderRange {Lower, Middle, Upper }

    #region Event Subscription
    void OnEnable()
    {
        EventManager.onUpgradePurchased += SliderValueChanged;
    }
    void OnDisable()
    {
        EventManager.onUpgradePurchased -= SliderValueChanged;
    }
    #endregion

    public void SliderValueChanged(UpgradeType upgradeType)
    {
        float normalizedValue = slider.value / slider.maxValue;

        if (normalizedValue < 0.333f)
            sliderHandleSprite.sprite = unhappySprite;
        else if (normalizedValue < 0.667f)
            sliderHandleSprite.sprite = neutralSprite;
        else
            sliderHandleSprite.sprite = happySprite;
    }
}