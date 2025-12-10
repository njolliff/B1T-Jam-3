using UnityEngine;
using TMPro;

public class DeityUpgradeCostText : MonoBehaviour
{
    public ResourceType resource;
    public bool isQuantityCost;
    public TextMeshProUGUI text;

    #region Event Subscription
    void OnEnable()
    {
        EventManager.onUpgradePurchased += UpdateText;

        UpdateText(UpgradeType.Gold);
    }
    void OnDisable()
    {
        EventManager.onUpgradePurchased -= UpdateText;
    }
    #endregion

    private void UpdateText(UpgradeType upgradeType)
    {
        if (text != null && DeityManager.Instance != null)
        {
            if (isQuantityCost)
                text.text = DeityManager.Instance.GetQuantityUpgradeCost(resource).ToString();
            else
                text.text = DeityManager.Instance.GetFrequencyUpgradeCost(resource).ToString();
        }
            
    }
}