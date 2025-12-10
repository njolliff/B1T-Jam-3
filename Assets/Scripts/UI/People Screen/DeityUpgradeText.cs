using TMPro;
using UnityEngine;

public class DeityUpgradeText : MonoBehaviour
{
    public ResourceType resource;
    public bool isQuantityText;
    public TextMeshProUGUI text;

    void OnEnable()
    {
        // Subscribe to events
        EventManager.onUpgradePurchased += UpdateText;

        // Update text
        UpdateText(UpgradeType.Gold);
    }
    void OnDisable()
    {
        // Unsubscribe from events
        EventManager.onUpgradePurchased -= UpdateText;
    }

    private void UpdateText(UpgradeType upgradeType)
    {
        if (text != null && JobManager.Instance != null)
        {
            if (isQuantityText)
                text.text = DeityManager.Instance.GetQuantityUpgradeText(resource);
            else
                text.text = DeityManager.Instance.GetFrequencyUpgradeText(resource);
        }
    }
}