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
    public int daysUntilOffering;
    public float demandScaleFactor;
    public float demandUpgradeFactor = 1;
    private Slider _offerringSlider;

    [Header("Happiness")]
    public float happiness = 50;
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
        daysUntilOffering = demandInterval;
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

    public void CheckOfferingDay()
    {

        // Update offering progress bar
        if (_offerringSlider != null)
            _offerringSlider.value = demandInterval - daysUntilOffering;

        // Make offering if it is offering day
        if (daysUntilOffering == 0)
        {
            DemandOffering();
            daysUntilOffering = demandInterval;
        }

        // Otherwise, decrement days until offering
        else
            daysUntilOffering--;
    }

    private void DemandOffering()
    {
        // Calculate offering amount and make demand
        int offeringAmount = Mathf.RoundToInt((float)(baseResourcesDemanded * Mathf.Pow(demandScaleFactor, _numDemandsMade) * demandUpgradeFactor));

        // Gain happiness if able to receive full offering, lose happiness if not
        if (ResourceManager.Instance.TakeOffering(resourceType, offeringAmount))
            GainHappiness();
        else
            LoseHappiness();

        // Increment number of demands made
        _numDemandsMade++;
    }

    #region Gain/Lose Happiness
    private void GainHappiness()
    {
        // Increase happiness up to 100
        if (happiness + happinessGain > 100)
            happiness = 100;
        else
            happiness += happinessGain;

        // Update happiness slider
        if (_happinessSlider != null)
            _happinessSlider.value = happiness;
    }
    private void LoseHappiness()
    {
        // Decrease happiness down to 0
        if (happiness - happinessLoss < 0)
            happiness = 0;
        else
            happiness -= happinessLoss;

        // Update happiness slider
        if (_happinessSlider != null)
            _happinessSlider.value = happiness;
    }
    #endregion
}