using TMPro;
using UnityEngine;

public class SiteUpgradeCostText : MonoBehaviour
{
    public ResourceType resource;
    public bool isResourceCost;
    public TextMeshProUGUI text;

    #region Event Subscription
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
    #endregion

    private void UpdateText(UpgradeType upgradeType)
    {
        if (text != null && JobManager.Instance != null)
        {
            if (isResourceCost)
                text.text = JobManager.Instance.GetSiteResourceUpgradeCost(resource).ToString();
            else
                text.text = JobManager.Instance.GetSiteMoneyUpgradeCost(resource).ToString();
        }
            
    }
}