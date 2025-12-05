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

public class GameManager : MonoBehaviour
{
    #region Variables
    // Serialized
    [Header("Deities")]
    //public Deity foodDeity = new();
    [Header("Population")]
    public int population;
    public int unassignedWorkers;
    [Header("Resources")]
    public int money;
    public int food, water, wood, ore;

    // Non-Serialized
    public static GameManager Instance;
    #endregion

    #region Initialization / Destruction
    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;

        // Initialize unassigned workers
        unassignedWorkers = population;
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
        ResourceType.Food => food,
        ResourceType.Water => water,
        ResourceType.Wood => wood,
        ResourceType.Ore => ore,
        ResourceType.Population => population,
        ResourceType.Money => money,

        _ => throw new System.NotImplementedException()
    };

    public void IncreaseResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Food && food < 999)
            food += amount;
        else if (resourceType == ResourceType.Water && water < 999)
            water += amount;
        else if (resourceType == ResourceType.Wood && wood < 999)
            wood += amount;
        else if (resourceType == ResourceType.Ore && ore < 999)
            ore += amount;
        else if (resourceType == ResourceType.Population && population < 999)
            population += amount;
        else if (resourceType == ResourceType.Money && money < 999)
            money += amount;
    }
    public void DecreaseResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Food && food > 0)
            food -= amount;
        else if (resourceType == ResourceType.Water && water > 0)
            water -= amount;
        else if (resourceType == ResourceType.Wood && wood > 0)
            wood -= amount;
        else if (resourceType == ResourceType.Ore && ore > 0)
            ore -= amount;
        else if (resourceType == ResourceType.Population && population > 0)
            population -= amount;
        else if (resourceType == ResourceType.Money && money > 0)
            money -= amount;
    }
}