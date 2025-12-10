using TMPro;
using UnityEngine;

public class SiteUpgradeText : MonoBehaviour
{
    public ResourceType resource;
    public bool isResourceText;
    public TextMeshProUGUI text;

    void OnEnable()
    {
        // Subscribe to events
        EventManager.onUpgradePurchased += UpdateText;

        // Update text
        UpdateText();
    }
    void OnDisable()
    {
        // Unsubscribe from events
        EventManager.onUpgradePurchased -= UpdateText;
    }

    private void UpdateText()
    {
        if (text != null && JobManager.Instance != null)
            text.text = JobManager.Instance.GetSiteUpgradeText(resource, isResourceText);
    }
}