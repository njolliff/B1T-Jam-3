using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public int birthPopulationGain = 1;
    public int immigrationPopulationGain = 1;
    public int foodRequirement;
    public int waterRequirement;
    public int woodRequirement;
    public int oreRequirement;

    public static PopulationManager Instance;
    private int _foodGenerated, _waterGenerated, _woodGenerated, _oreGenerated;

    #region Initialization / Destruction
    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
    }
    void OnDestroy()
    {
        // Singleton
        if (Instance == this)
            Instance = null;
    }
    #endregion

    public void PopulationResourceGenerated(ResourceType resource, int amount)
    {
        if (resource == ResourceType.Food)
        {
            _foodGenerated += amount;
            CheckBirth();
        }
        else if (resource == ResourceType.Water)
        {
            _waterGenerated += amount;
            CheckBirth();
        }
        else if (resource == ResourceType.Wood)
        {
            _woodGenerated += amount;
            CheckImmigration();
        }
        else if (resource == ResourceType.Ore)
        {
            _oreGenerated += amount;
            CheckImmigration();
        }
    }

    #region Birth / Immigration
    private void CheckBirth()
    {
        if (_foodGenerated >= foodRequirement && _waterGenerated >= waterRequirement)
        {
            ResourceManager.Instance.IncreaseResource(ResourceType.Population, birthPopulationGain);
            _foodGenerated = 0;
            _waterGenerated = 0;
        }
    }
    private void CheckImmigration()
    {
        if (_woodGenerated >= woodRequirement && _oreGenerated >= oreRequirement)
        {
            ResourceManager.Instance.IncreaseResource(ResourceType.Population, immigrationPopulationGain);
            _woodGenerated = 0;
            _oreGenerated = 0;
        }
    }
    #endregion
}