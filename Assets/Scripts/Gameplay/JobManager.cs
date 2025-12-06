using UnityEngine;

public class JobManager : MonoBehaviour
{
    public WorkSite foodWorkSite, waterWorkSite, woodWorkSite, oreWorkSite;

    public static JobManager Instance;

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

    #region Day Passed
    public void DayPassed()
    {
        // Generate resources
        ResourceManager.Instance.IncreaseResource(ResourceType.Food, foodWorkSite.GenerateResources());
        ResourceManager.Instance.IncreaseResource(ResourceType.Water, waterWorkSite.GenerateResources());
        ResourceManager.Instance.IncreaseResource(ResourceType.Wood, woodWorkSite.GenerateResources());
        ResourceManager.Instance.IncreaseResource(ResourceType.Ore, oreWorkSite.GenerateResources());
        

        // Generate money
        ResourceManager.Instance.IncreaseResource(ResourceType.Money, Mathf.RoundToInt(
            foodWorkSite.GenerateMoney() +
            waterWorkSite.GenerateMoney() +
            woodWorkSite.GenerateMoney() +
            oreWorkSite.GenerateMoney()
        ));
        
    }
    #endregion

    #region Assignment / Unassignment
    public void AssignWorker(ResourceType resourceType)
    {
        if (ResourceManager.Instance.unassignedWorkers > 0)
        {
            // Assign worker to appropriate work site
            switch (resourceType)
            {
                case ResourceType.Food:
                    foodWorkSite.workersAssigned++;
                    break;
                case ResourceType.Water:
                    waterWorkSite.workersAssigned++;
                    break;
                case ResourceType.Wood:
                    woodWorkSite.workersAssigned++;
                    break;
                case ResourceType.Ore:
                    oreWorkSite.workersAssigned++;
                    break;
            }

            // Decrement unassigned workers and call worker number changed event
            ResourceManager.Instance.unassignedWorkers--;
            EventManager.OnWorkerNumberChanged();
        }
    }
    public void UnassignWorker(ResourceType resourceType)
    {
        // If site has a worker to be removed,
        // Remove worker from appropriate work site, increment unassigned worker, and call worker number changed event
        switch (resourceType)
        {
            case ResourceType.Food:
                if (foodWorkSite.workersAssigned > 0)
                {
                    foodWorkSite.workersAssigned--;
                    ResourceManager.Instance.unassignedWorkers++;
                    EventManager.OnWorkerNumberChanged();
                }
                break;
            case ResourceType.Water:
                if (waterWorkSite.workersAssigned > 0)
                {
                    waterWorkSite.workersAssigned--;
                    ResourceManager.Instance.unassignedWorkers++;
                    EventManager.OnWorkerNumberChanged();
                }
                break;
            case ResourceType.Wood:
                if (woodWorkSite.workersAssigned > 0)
                {
                    woodWorkSite.workersAssigned--;
                    ResourceManager.Instance.unassignedWorkers++;
                    EventManager.OnWorkerNumberChanged();
                }
                break;
            case ResourceType.Ore:
                if (oreWorkSite.workersAssigned > 0)
                {
                    oreWorkSite.workersAssigned--;
                    ResourceManager.Instance.unassignedWorkers++;
                    EventManager.OnWorkerNumberChanged();
                }
                break;
        }
    }
    #endregion

    #region Accessors
    public int GetNumWorkers(ResourceType resourceType) => resourceType switch
    {
        ResourceType.Food => foodWorkSite.workersAssigned,
        ResourceType.Water => waterWorkSite.workersAssigned,
        ResourceType.Wood => woodWorkSite.workersAssigned,
        ResourceType.Ore => oreWorkSite.workersAssigned,
        
        _ => throw new System.NotImplementedException()
    };
    public string GetResourceGenerationRange(ResourceType resourceType) => resourceType switch
    {
        ResourceType.Food => $"{foodWorkSite.minResourcesPerWorker * foodWorkSite.workersAssigned}-{foodWorkSite.maxResourcesPerWorker * foodWorkSite.workersAssigned}",
        ResourceType.Water => $"{waterWorkSite.minResourcesPerWorker * waterWorkSite.workersAssigned}-{waterWorkSite.maxResourcesPerWorker * waterWorkSite.workersAssigned}",
        ResourceType.Wood => $"{woodWorkSite.minResourcesPerWorker * woodWorkSite.workersAssigned}-{woodWorkSite.maxResourcesPerWorker * woodWorkSite.workersAssigned}",
        ResourceType.Ore => $"{oreWorkSite.minResourcesPerWorker * oreWorkSite.workersAssigned}-{oreWorkSite.maxResourcesPerWorker * oreWorkSite.workersAssigned}",

        _ => throw new System.NotImplementedException()
    };
    public string GetMoneyGenerationRange(ResourceType resourceType) => resourceType switch
    {
        ResourceType.Food => $"{foodWorkSite.minMoneyPerWorker * foodWorkSite.workersAssigned}-{foodWorkSite.maxMoneyPerWorker * foodWorkSite.workersAssigned}",
        ResourceType.Water => $"{waterWorkSite.minMoneyPerWorker * waterWorkSite.workersAssigned}-{waterWorkSite.maxMoneyPerWorker * waterWorkSite.workersAssigned}",
        ResourceType.Wood => $"{woodWorkSite.minMoneyPerWorker * woodWorkSite.workersAssigned}-{woodWorkSite.maxMoneyPerWorker * woodWorkSite.workersAssigned}",
        ResourceType.Ore => $"{oreWorkSite.minMoneyPerWorker * oreWorkSite.workersAssigned}-{oreWorkSite.maxMoneyPerWorker * oreWorkSite.workersAssigned}",

        _ => throw new System.NotImplementedException()
    };
    #endregion
}