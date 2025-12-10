using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldBuyButton : MonoBehaviour
{
    public ResourceType resource;
    public bool isResourceButton;
    public Image sprite;
    public Button button;
    public TextMeshProUGUI text;

    #region Enable / Disable
    void OnEnable()
    {
        // Subscribe to events
        EventManager.onResourceNumberChanged += CheckIfPurchasable;
        EventManager.onUpgradePurchased += CheckIfPurchasable;

        // Show/Hide button when screen is enabled
        CheckIfPurchasable();
    }

    void OnDisable()
    {
        // Unsubscribe from event
        EventManager.onResourceNumberChanged -= CheckIfPurchasable;
        EventManager.onUpgradePurchased -= CheckIfPurchasable;
    }
    #endregion

    #region Purchase
    public void PurchaseUpgrade()
    {
        if (JobManager.Instance != null)
        {
            if (isResourceButton)
                JobManager.Instance.UpgradeResourceGeneration(resource);
            else
                JobManager.Instance.UpgradeMoneyGeneration(resource);
        }
        else
            Debug.Log("Job Manager is null.");
    }
    #endregion

    #region Check if Purchasable
    // Overload for onResourceNumberChanged event
    private void CheckIfPurchasable(ResourceType resourceType)
    {
        if (JobManager.Instance != null && resourceType == ResourceType.Money)
        {
            if (isResourceButton && JobManager.Instance.CanAffordResourceUpgrade(resource))
                ShowButton();
            else if (!isResourceButton && JobManager.Instance.CanAffordMoneyUpgrade(resource))
                ShowButton();
            else
                HideButton();
        }
    }
    // Overload for onUpgradePurchased event
    private void CheckIfPurchasable()
    {
        if (JobManager.Instance != null)
        {
            if (isResourceButton && JobManager.Instance.CanAffordResourceUpgrade(resource))
                ShowButton();
            else if (!isResourceButton && JobManager.Instance.CanAffordMoneyUpgrade(resource))
                ShowButton();
            else
                HideButton();
        }
    }
    #endregion

    #region Show / Hide Button
    private void ShowButton()
    {
        // Enable sprite and text and make button interactable
        if (sprite != null)
            sprite.enabled = true;
        if (text != null)
            text.enabled = true;
        if (button != null)
            button.interactable = true;
    }
    private void HideButton()
    {
        // Disable sprite and text and make button uninteractable
        if (sprite != null)
            sprite.enabled = false;
        if (text != null)
            text.enabled = false;
        if (button != null)
            button.interactable = false;
    }
    #endregion
}