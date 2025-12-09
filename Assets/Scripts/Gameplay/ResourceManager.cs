using UnityEngine;

public enum ResourceType
{
    Food,
    Water,
    Wood,
    Ore,
    Population,
    Money
}

public class ResourceManager : MonoBehaviour
{
    #region Variables
    // Serialized
    [Header("Population")]
    [SerializeField] private int _population;
    public int unassignedWorkers;
    [Header("Resources")]
    [SerializeField] private int _money;
    [SerializeField] private int _food, _water, _wood, _ore;

    // Non-Serialized
    public static ResourceManager Instance;
    #endregion

    #region Initialization / Destruction
    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;

        // Initialize unassigned workers
        unassignedWorkers = _population;
    }
    void OnDestroy()
    {
        // Singleton
        if (Instance == this)
            Instance = null;
    }
    #endregion

    public int GetNumResource(ResourceType resource) => resource switch
    {
        ResourceType.Food => _food,
        ResourceType.Water => _water,
        ResourceType.Wood => _wood,
        ResourceType.Ore => _ore,
        ResourceType.Population => _population,
        ResourceType.Money => _money,

        _ => throw new System.NotImplementedException()
    };

    public bool TakeOffering(ResourceType resource, int amount)
    {
        if (GetNumResource(resource) - amount < 0)
        {
            DecreaseResource(resource, amount);
            return false;
        }
        else
        {
            DecreaseResource(resource, amount);
            return true;
        }
    }

    #region Increase/Decrease Resource
    public void IncreaseResource(ResourceType resource, int amount)
    {
        if (resource == ResourceType.Food)
        {
            // Gain resource, capped at 999
            _food = Mathf.Min(999, _food + amount);

            // Notify population manager of resource gain
            PopulationManager.Instance.PopulationResourceGenerated(resource, amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Water)
        {
            // Gain resource, capped at 999
            _water = Mathf.Min(999, _water + amount);

            // Notify population manager of resource gain
            PopulationManager.Instance.PopulationResourceGenerated(resource, amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Wood)
        {
            // Gain resource, capped at 999
            _wood = Mathf.Min(999, _wood + amount);

            // Notify population manager of resource gain
            PopulationManager.Instance.PopulationResourceGenerated(resource, amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Ore)
        {
            // Gain resource, capped at 999
            _ore = Mathf.Min(999, _ore + amount);

            // Notify population manager of resource gain
            PopulationManager.Instance.PopulationResourceGenerated(resource, amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Population)
        {
            if (_population + amount > 999)
            {
                // Add actual number of population gained to unassigned workers
                int populationGained = Mathf.Max(0, 999 - (_population + amount));
                unassignedWorkers += populationGained;
                EventManager.OnWorkerNumberChanged();

                _population = 999;
            }
            else
            {
                // Add number of population gained to unassigned workers and population
                _population += amount;
                unassignedWorkers += amount;
                EventManager.OnWorkerNumberChanged();
            }

            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Money)
        {
            // Gain resource, capped at 999
            _money = Mathf.Min(999, _money + amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
    }
    public void DecreaseResource(ResourceType resource, int amount)
    {
        if (resource == ResourceType.Food)
        {
            // Lose resource, but not below 0
            _food = Mathf.Max(0, _food - amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Water)
        {
            // Lose resource, but not below 0
            _water = Mathf.Max(0, _water - amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Wood)
        {
            // Lose resource, but not below 0
            _wood = Mathf.Max(0, _wood - amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Ore)
        {
            // Lose resource, but not below 0
            _ore = Mathf.Max(0, _ore - amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Population)
        {
            // Lose resource, but not below 0
            _population = Mathf.Max(0, _population - amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
        else if (resource == ResourceType.Money)
        {
            // Lose resource, but not below 0
            _money = Mathf.Max(0, _money - amount);

            // Update UI
            EventManager.OnResourceNumberChanged(resource);
        }
    }
    #endregion
}