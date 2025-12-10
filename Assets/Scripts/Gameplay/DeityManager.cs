using UnityEngine;
using UnityEngine.UI;

public class DeityManager : MonoBehaviour
{
    #region Variables
    [Header("Deity Settings")]
    public int baseResourcesDemanded;
    public int baseDemandInterval;
    public float demandScaleFactor;
    public float happinessGain, happinessLoss;
    public int baseQuantityUpgradeCost, baseFrequencyUpgradeCost;

    [Header("Food Deity Sliders")]
    public Slider foodHappinessSlider;
    public Slider foodOfferingSlider;

    [Header("Water Deity Sliders")]
    public Slider waterHappinessSlider;
    public Slider waterOfferingSlider;

    [Header("Wood Deity Sliders")]
    public Slider woodHappinessSlider;
    public Slider woodOfferingSlider;

    [Header("Ore Deity Sliders")]
    public Slider oreHappinessSlider;
    public Slider oreOfferingSlider;

    [Header("Deities")]
    public Deity foodDeity;
    public Deity waterDeity;
    public Deity woodDeity;
    public Deity oreDeity;

    public static DeityManager Instance;
    #endregion

    #region Initialization / Destruction
    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;

        // Initialize deities with deity settings
        InitializeDeities();
    }
    void OnDestroy()
    {
        // Singletons
        if (Instance == this)
            Instance = null;
    }
    private void InitializeDeities()
    {
        // Initialize deity values
        foodDeity = new(ResourceType.Food, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, baseQuantityUpgradeCost, baseFrequencyUpgradeCost, foodOfferingSlider, foodHappinessSlider);
        waterDeity = new(ResourceType.Water, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, baseQuantityUpgradeCost, baseFrequencyUpgradeCost, waterOfferingSlider, waterHappinessSlider);
        woodDeity = new(ResourceType.Wood, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, baseQuantityUpgradeCost, baseFrequencyUpgradeCost, woodOfferingSlider, woodHappinessSlider);
        oreDeity = new(ResourceType.Ore, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, baseQuantityUpgradeCost, baseFrequencyUpgradeCost, oreOfferingSlider, oreHappinessSlider);
    }
    #endregion

    #region Day Passed
    public void DayPassed()
    {
        // Notify dieties of day passing
        foodDeity.DayPassed();
        waterDeity.DayPassed();
        woodDeity.DayPassed();
        oreDeity.DayPassed();
    }
    #endregion

    #region Upgrades
    public void UpgradeQuantity(ResourceType resource)
    {
        if (resource == ResourceType.Food)
            foodDeity.UpgradeQuantity();
        else if (resource == ResourceType.Water)
            waterDeity.UpgradeQuantity();
        else if (resource == ResourceType.Wood)
            woodDeity.UpgradeQuantity();
        else if (resource == ResourceType.Ore)
            oreDeity.UpgradeQuantity();
    }
    public void UpgradeFrequency(ResourceType resource)
    {
        if (resource == ResourceType.Food)
            foodDeity.UpgradeFrequency();
        else if (resource == ResourceType.Water)
            waterDeity.UpgradeFrequency();
        else if (resource == ResourceType.Wood)
            woodDeity.UpgradeFrequency();
        else if (resource == ResourceType.Ore)
            oreDeity.UpgradeFrequency();
    }
    #endregion

    #region Accessors
    public bool CanAffordQuantityUpgrade(ResourceType resource)
    {
        if (ResourceManager.Instance != null)
        {
            if (resource == ResourceType.Food)
                return foodDeity.CanAffordQuantityUpgrade();
            else if (resource == ResourceType.Water)
                return waterDeity.CanAffordQuantityUpgrade();
            else if (resource == ResourceType.Wood)
                return woodDeity.CanAffordQuantityUpgrade();
            else if (resource == ResourceType.Ore)
                return oreDeity.CanAffordQuantityUpgrade();
            
            else
                throw new System.NotImplementedException("Invalid resource type for CanAffordQuantityUpgrade().");
        }
        else
            throw new System.NotImplementedException("Resource Manager is null.");
    }
    public bool CanAffordFrequencyUpgrade(ResourceType resource)
    {
        if (ResourceManager.Instance != null)
        {
            if (resource == ResourceType.Food)
                return foodDeity.CanAffordFrequencyUpgrade();
            else if (resource == ResourceType.Water)
                return waterDeity.CanAffordFrequencyUpgrade();
            else if (resource == ResourceType.Wood)
                return woodDeity.CanAffordFrequencyUpgrade();
            else if (resource == ResourceType.Ore)
                return oreDeity.CanAffordFrequencyUpgrade();
            
            else
                throw new System.NotImplementedException("Invalid resource type for CanAffordFrequencyUpgrade().");
        }
        else
            throw new System.NotImplementedException("Resource Manager is null.");
    }
    public int GetQuantityUpgradeCost(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodDeity.quantityUpgradeCost,
        ResourceType.Water => waterDeity.quantityUpgradeCost,
        ResourceType.Wood => woodDeity.quantityUpgradeCost,
        ResourceType.Ore => oreDeity.quantityUpgradeCost,

        _ => throw new System.NotImplementedException("Invalid resource type for GetQuantityUpgradeCost().")
    };
    public int GetFrequencyUpgradeCost(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodDeity.frequencyUpgradeCost,
        ResourceType.Water => waterDeity.frequencyUpgradeCost,
        ResourceType.Wood => woodDeity.frequencyUpgradeCost,
        ResourceType.Ore => oreDeity.frequencyUpgradeCost,

        _ => throw new System.NotImplementedException("Invalid resource type for GetFrequencyUpgradeCost().")
    };
    public string GetQuantityUpgradeText(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodDeity.GetQuantityUpgradeText(),
        ResourceType.Water => waterDeity.GetQuantityUpgradeText(),
        ResourceType.Wood => woodDeity.GetQuantityUpgradeText(),
        ResourceType.Ore => oreDeity.GetQuantityUpgradeText(),

        _ => throw new System.NotImplementedException("Invalid resource type for GetQuantityUpgradeText().")
    };
    public string GetFrequencyUpgradeText(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodDeity.GetFrequencyUpgradeText(),
        ResourceType.Water => waterDeity.GetFrequencyUpgradeText(),
        ResourceType.Wood => woodDeity.GetFrequencyUpgradeText(),
        ResourceType.Ore => oreDeity.GetFrequencyUpgradeText(),

        _ => throw new System.NotImplementedException("Invalid resource type for GetFrequencyUpgradeText().")
    };
    #endregion
}