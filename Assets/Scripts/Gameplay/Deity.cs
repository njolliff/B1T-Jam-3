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
    public float demandUpgradeFactor = 1f;
    private Slider _offerringSlider;

    [Header("Happiness")]
    public float happiness = 50f;
    public float happinessGain, happinessLoss;
    private Slider _happinessSlider;

    [Header("Upgrades")]
    public int baseQuantityUpgradeCost, baseFrequencyUpgradeCost;
    public int quantityUpgradeCost, frequencyUpgradeCost;
    public int numQuantityUpgrades = 1, numFrequencyUpgrades = 1;

    // Non-Serialized
    private int _numDemandsMade = 0;
    #endregion

    #region Constructor
    public Deity(ResourceType resource, int baseResourcesDemanded, int baseDemandInterval, float demandScaleFactor, float happinessGain, float happinessLoss, int baseQuantityUpgradeCost, int baseFrequencyUpgradeCost, Slider offerringSlider, Slider happinessSlider)
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

        // Set upgrade costs
        this.baseQuantityUpgradeCost = baseQuantityUpgradeCost;
        quantityUpgradeCost = baseQuantityUpgradeCost;
        this.baseFrequencyUpgradeCost = baseFrequencyUpgradeCost;
        frequencyUpgradeCost = baseFrequencyUpgradeCost;

        // Set slider references
        _offerringSlider = offerringSlider;
        _happinessSlider = happinessSlider;

        // Set offering slider max value to demand interval
        if (_offerringSlider != null)
            _offerringSlider.maxValue = demandInterval;
    }
    #endregion

    #region Day Passed + Demand Offering
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
    #endregion

    #region Upgrades
    public void UpgradeQuantity()
    {
        if (ResourceManager.Instance != null && ResourceManager.Instance.unassignedWorkers >= quantityUpgradeCost)
        {
            // Lose unassigned workers + population equal to upgrade cost
            ResourceManager.Instance.unassignedWorkers -= quantityUpgradeCost;
            ResourceManager.Instance.DecreaseResource(ResourceType.Population, quantityUpgradeCost);

            // Decrement upgrade factor and increment number of upgrades purchased
            demandUpgradeFactor -= 0.05f;
            numQuantityUpgrades++;

            // Calculate cost of next upgrade up to 999 and call upgrade purchased event
            quantityUpgradeCost = Mathf.Min(999, (int)(baseQuantityUpgradeCost * Mathf.Pow(2, numQuantityUpgrades)));
            EventManager.OnUpgradePurchased(UpgradeType.Person);
        }
    }
    public void UpgradeFrequency()
    {
        if (ResourceManager.Instance != null && ResourceManager.Instance.unassignedWorkers >= frequencyUpgradeCost)
        {
            // Lose unassigned workers + population equal to upgrade cost
            ResourceManager.Instance.unassignedWorkers -= quantityUpgradeCost;
            ResourceManager.Instance.DecreaseResource(ResourceType.Population, frequencyUpgradeCost);

            // Increment demand intervalt and update demand slider max value
            demandInterval++;
            if (_offerringSlider != null)
                _offerringSlider.maxValue = demandInterval;

            // Increment number of upgrades
            numQuantityUpgrades++;

            // Calculate cost of next upgrade up to 999 and call upgrade purchased event
            frequencyUpgradeCost = Mathf.Min(999, (int)(baseQuantityUpgradeCost * Mathf.Pow(2, numQuantityUpgrades)));
            EventManager.OnUpgradePurchased(UpgradeType.Person);
        }
    }
    #endregion

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

    #region Accessors
    public bool CanAffordQuantityUpgrade() => ResourceManager.Instance.unassignedWorkers >= quantityUpgradeCost;
    public bool CanAffordFrequencyUpgrade() => ResourceManager.Instance.unassignedWorkers >= frequencyUpgradeCost;
    public string GetQuantityUpgradeText() => $"{demandUpgradeFactor * 100}%\n↓\n{(demandUpgradeFactor - 0.05f) * 100}%";
    public string GetFrequencyUpgradeText() => $"{demandInterval} Days\n↓\n{demandInterval + 1} Days";
    #endregion
}