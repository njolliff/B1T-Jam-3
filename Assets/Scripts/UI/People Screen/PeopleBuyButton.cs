using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PeopleBuyButton : MonoBehaviour
{
    public ResourceType resource;
    public bool isQuantityButton;
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
        CheckIfPurchasable(UpgradeType.Gold);
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
        if (DeityManager.Instance != null)
        {
            if (isQuantityButton)
                DeityManager.Instance.UpgradeQuantity(resource);
            else
                DeityManager.Instance.UpgradeFrequency(resource);
        }
        else
            throw new System.NotImplementedException("Deity Manager is null.");
    }
    #endregion

    #region Check if Purchasable
    // Overload for onResourceNumberChanged event
    private void CheckIfPurchasable(ResourceType resourceType)
    {
        if (DeityManager.Instance == null)
            throw new System.NotImplementedException("Deity Manager is null.");

        if (resourceType == ResourceType.Population)
        {
            if (isQuantityButton && DeityManager.Instance.CanAffordQuantityUpgrade(resource))
                ShowButton();
            else if (!isQuantityButton && DeityManager.Instance.CanAffordFrequencyUpgrade(resource))
                ShowButton();
            else
                HideButton();
        }
    }
    // Overload for onUpgradePurchased event
    private void CheckIfPurchasable(UpgradeType upgradeType)
    {
        if (DeityManager.Instance != null)
        {
            if (isQuantityButton && DeityManager.Instance.CanAffordQuantityUpgrade(resource))
                ShowButton();
            else if (!isQuantityButton && DeityManager.Instance.CanAffordFrequencyUpgrade(resource))
                ShowButton();
            else
                HideButton();
        }
        else
            throw new System.NotImplementedException("Deity Manager is null.");
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