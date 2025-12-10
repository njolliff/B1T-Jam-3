using UnityEngine;
using UnityEngine.UI;

public class DeityManager : MonoBehaviour
{
    [Header("Deity Settings")]
    public int baseResourcesDemanded;
    public int baseDemandInterval;
    public float demandScaleFactor;
    public float happinessGain, happinessLoss;

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
    #endregion

    public void DayPassed()
    {
        // Notify dieties of day passing
        foodDeity.DayPassed();
        waterDeity.DayPassed();
        woodDeity.DayPassed();
        oreDeity.DayPassed();
    }

    private void InitializeDeities()
    {
        // Initialize deity values
        foodDeity = new(ResourceType.Food, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, foodOfferingSlider, foodHappinessSlider);
        waterDeity = new(ResourceType.Water, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, waterOfferingSlider, waterHappinessSlider);
        woodDeity = new(ResourceType.Wood, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, woodOfferingSlider, woodHappinessSlider);
        oreDeity = new(ResourceType.Ore, baseResourcesDemanded, baseDemandInterval, demandScaleFactor, happinessGain, happinessLoss, oreOfferingSlider, oreHappinessSlider);
    }
}