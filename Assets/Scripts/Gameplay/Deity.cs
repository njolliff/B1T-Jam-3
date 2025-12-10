using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Deity
{
    #region Variables
    // Serialized
    [Header("Offering")]
    public ResourceType resourceType;
    public int baseResourcesDemanded;
    public int demandInterval;
    public int daysSinceLastDemand = 0;
    public float demandScaleFactor;
    public float demandUpgradeFactor;
    private Slider _offerringSlider;

    [Header("Happiness")]
    public float happiness;
    public float happinessGain, happinessLoss;
    private Slider _happinessSlider;

    // Non-Serialized
    private int _numDemandsMade = 0;
    #endregion

    #region Constructor
    public Deity(ResourceType resource, int baseResourcesDemanded, int baseDemandInterval, float demandScaleFactor, float happinessGain, float happinessLoss, Slider offerringSlider, Slider happinessSlider)
    {
        // Set resource type
        resourceType = resource;

        // Set initial offering values
        this.baseResourcesDemanded = baseResourcesDemanded;
        demandInterval = baseDemandInterval;
        this.demandScaleFactor = demandScaleFactor;

        // Set happiness gained/lost on offering made/missed
        this.happinessGain = happinessGain;
        this.happinessLoss = happinessLoss;

        // Set slider references
        _offerringSlider = offerringSlider;
        _happinessSlider = happinessSlider;

        // Set offering slider max value to demand interval
        if (_offerringSlider != null)
            _offerringSlider.maxValue = demandInterval;

        // Set default upgrade factor and happiness
        demandUpgradeFactor = 1;
        happiness = 50;
    }
    #endregion

    public void DayPassed()
    {
        // Increment days since last demand
        daysSinceLastDemand++;

        // Update offering progress bar
        if (_offerringSlider != null)
            _offerringSlider.value = daysSinceLastDemand;

        // Make offering if it is offering day
        if (daysSinceLastDemand >= demandInterval)
        {
            DemandOffering();
            daysSinceLastDemand = 0;
        }
    }

    private void DemandOffering()
    {
        // Calculate offering amount and make demand of up to 999 resources
        int offeringAmount = Mathf.Min(
            999,
            Mathf.RoundToInt((float)(baseResourcesDemanded * Mathf.Pow(demandScaleFactor, _numDemandsMade) * demandUpgradeFactor))
        );

        if (ResourceManager.Instance != null)
        {
            // Increment number of demands made
            _numDemandsMade++;

            // Gain happiness if able to receive full offering, lose happiness if not
            if (ResourceManager.Instance.TakeOffering(resourceType, offeringAmount))
                GainHappiness();
            else
                LoseHappiness();
        }
    }

    #region Gain/Lose Happiness
    private void GainHappiness()
    {
        // Increase happiness up to 100
        happiness = Mathf.Min(100, happiness + happinessGain);

        // Update happiness slider
        if (_happinessSlider != null)
            _happinessSlider.value = happiness;
    }
    private void LoseHappiness()
    {
        // Decrease happiness down to 0
        happiness = Mathf.Max(0, happiness - happinessLoss);

        // Update happiness slider
        if (_happinessSlider != null)
            _happinessSlider.value = happiness;

        // If happiness is 0, game over
        if (happiness == 0 && GameManager.Instance != null)
            GameManager.Instance.GameOver();
    }
    #endregion
}