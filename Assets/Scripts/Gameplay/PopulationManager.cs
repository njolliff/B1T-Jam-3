using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    #region Variables
    // Serialized
    public float requirementScaling;
    [Header("Birth")]
    public int birthCooldown;
    public int baseBirthRequirement;
    public float birthUpgradeFactor = 1f;
    [Header("Immigration")]
    public int immigrationCooldown;
    public int baseImmigrationRequirement;
    public float immigrationUpgradeFactor = 1f;

    // Non-Serialized
    public static PopulationManager Instance;
    private int _foodGenerated = 0, _waterGenerated = 0, _woodGenerated = 0, _oreGenerated = 0;
    private int _birthTimer = 0, _immigrationTimer = 0;
    private bool _canDoBirth = true, _canDoImmigration = true;
    #endregion

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

    public void DayPassed()
    {
        // Update birth coolcown
        if (!_canDoBirth)
        {
            _birthTimer++;
            if (_birthTimer >= birthCooldown)
            {
                _canDoBirth = true;
                _birthTimer = 0;
                CheckBirth();
            }
        }
        
        // Update immigration cooldown
        if (!_canDoImmigration)
        {
            _immigrationTimer++;
            if (_immigrationTimer >= immigrationCooldown)
            {
                _canDoImmigration = true;
                _immigrationTimer = 0;
                CheckImmigration();
            }
        }
    }
    public void PopulationResourceGenerated(ResourceType resource, int amount)
    {
        // Birth
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

        // Immigration
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
        // Calculate the birth requirement then check if a birth can be done
        int birthRequirement = GetScaledBirthRequirement();
        if (_canDoBirth && _foodGenerated >= birthRequirement && _waterGenerated >= birthRequirement)
        {
            // Increase population
            ResourceManager.Instance.IncreaseResource(ResourceType.Population, 1);

            // Reset resource counters
            _foodGenerated = 0;
            _waterGenerated = 0;

            // Put birth on cooldown
            _canDoBirth = false;
        }
    }
    private void CheckImmigration()
    {
        // Calculate the immigration requirement then check if an immigration can be done
        int immigrationRequirement = GetScaledImmigrationRequirement();
        if (_canDoBirth && _woodGenerated >= immigrationRequirement && _oreGenerated >= immigrationRequirement)
        {
            // Increase population
            ResourceManager.Instance.IncreaseResource(ResourceType.Population, 1);

            // Reset resource counters
            _woodGenerated = 0;
            _oreGenerated = 0;

            // Put immigration on cooldown
            _canDoImmigration = false;
        }
    }
    #endregion

    #region Helper Methods
    // Calculate scaled birth requirement based on current population
    private int GetScaledBirthRequirement()
    {
        if (ResourceManager.Instance != null)
        {
            int currentPopulation = ResourceManager.Instance.GetNumResource(ResourceType.Population);
            return Mathf.RoundToInt(baseBirthRequirement * Mathf.Pow(requirementScaling, currentPopulation / 10) * birthUpgradeFactor);
        }
        else
        {
            Debug.Log("Resource Manager instance was found null while getting scaled birth requirement.");
            throw new System.NotImplementedException();
        }
    }
    // Calculate scaled immigration requirement based on current population
    private int GetScaledImmigrationRequirement()
    {
        if (ResourceManager.Instance != null)
        {
            int currentPopulation = ResourceManager.Instance.GetNumResource(ResourceType.Population);
            return Mathf.RoundToInt(baseImmigrationRequirement * Mathf.Pow(requirementScaling, currentPopulation / 10) * immigrationUpgradeFactor);
        }
        else
        {
            Debug.Log("Resource Manager instance was found null while getting scaled immigration requirement.");
            throw new System.NotImplementedException();
        }
    }
    #endregion
}