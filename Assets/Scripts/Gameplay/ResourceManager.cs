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
    public void IncreaseResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Food && _food < 999)
        {
            if (_food + amount > 999)
                _food = 999;
            else
                _food += amount;

            EventManager.OnResourceNumberChanged(ResourceType.Food);
        }
        else if (resourceType == ResourceType.Water && _water < 999)
        {
            if (_water + amount > 999)
                _water = 999;
            else
                _water += amount;

            EventManager.OnResourceNumberChanged(ResourceType.Water);
        }
        else if (resourceType == ResourceType.Wood && _wood < 999)
        {
            if (_wood + amount > 999)
                _wood = 999;
            else
                _wood += amount;

            EventManager.OnResourceNumberChanged(ResourceType.Wood);
        }
        else if (resourceType == ResourceType.Ore && _ore < 999)
        {
            if (_ore + amount > 999)
                _ore = 999;
            else
                _ore += amount;

            EventManager.OnResourceNumberChanged(ResourceType.Ore);
        }
        else if (resourceType == ResourceType.Population && _population < 999)
        {
            if (_population + amount > 999)
                _population = 999;
            else
                _population += amount;

            EventManager.OnResourceNumberChanged(ResourceType.Population);
        }
        else if (resourceType == ResourceType.Money && _money < 999)
        {
            if (_money + amount > 999)
                _money = 999;
            else
                _money += amount;

            EventManager.OnResourceNumberChanged(ResourceType.Money);
        }
    }
    public void DecreaseResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Food && _food > 0)
        {
            if (_food - amount < 0)
                _food = 0;
            else
                _food -= amount;
            EventManager.OnResourceNumberChanged(ResourceType.Food);
        }
        else if (resourceType == ResourceType.Water && _water > 0)
        {
            if (_water - amount < 0)
                _water = 0;
            else
                _water -= amount;
            EventManager.OnResourceNumberChanged(ResourceType.Water);
        }
        else if (resourceType == ResourceType.Wood && _wood > 0)
        {
            if (_wood - amount < 0)
                _wood = 0;
            else
                _wood -= amount;
            EventManager.OnResourceNumberChanged(ResourceType.Wood);
        }
        else if (resourceType == ResourceType.Ore && _ore > 0)
        {
            if (_ore - amount < 0)
                _ore = 0;
            else
                _ore -= amount;
            EventManager.OnResourceNumberChanged(ResourceType.Ore);
        }
        else if (resourceType == ResourceType.Population && _population > 0)
        {
            if (_population - amount < 0)
                _population = 0;
            else
                _population -= amount;
            EventManager.OnResourceNumberChanged(ResourceType.Population);
        }
        else if (resourceType == ResourceType.Money && _money > 0)
        {
            if (_money - amount < 0)
                _money = 0;
            else
                _money -= amount;
            EventManager.OnResourceNumberChanged(ResourceType.Population);
        }
    }
    #endregion
}