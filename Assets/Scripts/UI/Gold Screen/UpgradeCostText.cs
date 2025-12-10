using TMPro;
using UnityEngine;

public class UpgradeCostText : MonoBehaviour
{
    public ResourceType resource;
    public bool isResourceCost;
    public TextMeshProUGUI text;

    #region Event Subscription
    void OnEnable()
    {
        EventManager.onUpgradePurchased += UpdateText;
    }
    void OnDisable()
    {
        EventManager.onUpgradePurchased -= UpdateText;
    }
    #endregion

    private void UpdateText()
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