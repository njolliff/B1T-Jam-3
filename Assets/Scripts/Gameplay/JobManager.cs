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
    public void AssignWorker(ResourceType resource)
    {
        if (ResourceManager.Instance.unassignedWorkers > 0)
        {
            // Assign worker to appropriate work site
            switch (resource)
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
    public void UnassignWorker(ResourceType resource)
    {
        // If site has a worker to be removed,
        // Remove worker from appropriate work site, increment unassigned worker, and call worker number changed event
        switch (resource)
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

    #region Upgrading
    public void UpgradeResourceGeneration(ResourceType resource)
    {
        if (resource == ResourceType.Food)
            foodWorkSite.UpgradeResourceGeneration();
        else if (resource == ResourceType.Water)
            waterWorkSite.UpgradeResourceGeneration();
        else if (resource == ResourceType.Wood)
            woodWorkSite.UpgradeResourceGeneration();
        else if (resource == ResourceType.Ore)
            oreWorkSite.UpgradeResourceGeneration();
    }
    public void UpgradeMoneyGeneration(ResourceType resource)
    {
        if (resource == ResourceType.Food)
            foodWorkSite.UpgradeMoneyGeneration();
        else if (resource == ResourceType.Water)
            waterWorkSite.UpgradeMoneyGeneration();
        else if (resource == ResourceType.Wood)
            woodWorkSite.UpgradeMoneyGeneration();
        else if (resource == ResourceType.Ore)
            oreWorkSite.UpgradeMoneyGeneration();
    }
    public bool CanAffordResourceUpgrade(ResourceType resource)
    {
        if (ResourceManager.Instance != null)
        {
            if (resource == ResourceType.Food)
                return foodWorkSite.CanAffordResourceUpgrade();
            else if (resource == ResourceType.Water)
                return waterWorkSite.CanAffordResourceUpgrade();
            else if (resource == ResourceType.Wood)
                return woodWorkSite.CanAffordResourceUpgrade();
            else if (resource == ResourceType.Ore)
                return oreWorkSite.CanAffordResourceUpgrade();
            else
                throw new System.NotImplementedException("Invalid resource type for CanAffordResourceUpgrade().");
        }
        else
            throw new System.NotImplementedException("Resource Manager is null.");
    }
    public bool CanAffordMoneyUpgrade(ResourceType resource)
    {
        if (ResourceManager.Instance != null)
        {
            if (resource == ResourceType.Food)
                return foodWorkSite.CanAffordMoneyUpgrade();
            else if (resource == ResourceType.Water)
                return waterWorkSite.CanAffordMoneyUpgrade();
            else if (resource == ResourceType.Wood)
                return woodWorkSite.CanAffordMoneyUpgrade();
            else if (resource == ResourceType.Ore)
                return oreWorkSite.CanAffordMoneyUpgrade();
            else
                throw new System.NotImplementedException("Invalid resource type for CanAffordMoneyUpgrade().");
        }
        else
            throw new System.NotImplementedException("Resource Manager is null.");
    }
    #endregion

    #region Accessors
    public int GetNumWorkers(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodWorkSite.workersAssigned,
        ResourceType.Water => waterWorkSite.workersAssigned,
        ResourceType.Wood => woodWorkSite.workersAssigned,
        ResourceType.Ore => oreWorkSite.workersAssigned,
        
        _ => throw new System.NotImplementedException("Invalid resource type for GetNumWorkers().")
    };
    public string GetResourceGenerationRange(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodWorkSite.GetResourceGenerationRange(),
        ResourceType.Water => waterWorkSite.GetResourceGenerationRange(),
        ResourceType.Wood => woodWorkSite.GetResourceGenerationRange(),
        ResourceType.Ore => oreWorkSite.GetResourceGenerationRange(),

        _ => throw new System.NotImplementedException("Invalid resource type for GetResourceGenerationRange().")
    };
    public string GetMoneyGenerationRange(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodWorkSite.GetMoneyGenerationRange(),
        ResourceType.Water => waterWorkSite.GetMoneyGenerationRange(),
        ResourceType.Wood => woodWorkSite.GetMoneyGenerationRange(),
        ResourceType.Ore => oreWorkSite.GetMoneyGenerationRange(),

        _ => throw new System.NotImplementedException("Invalid resource type for GetMoneyGenerationRange().")
    };
    public string GetSiteUpgradeText(ResourceType resource, bool getResource)
    {
        if (resource == ResourceType.Food)
        {
            if (getResource)
                return foodWorkSite.GetResourceUpgradeText();
            else
                return foodWorkSite.GetMoneyUpgradeText();
        }
        else if (resource == ResourceType.Water)
        {
            if (getResource)
                return waterWorkSite.GetResourceUpgradeText();
            else
                return waterWorkSite.GetMoneyUpgradeText();
        }
        else if (resource == ResourceType.Wood)
        {
            if (getResource)
                return woodWorkSite.GetResourceUpgradeText();
            else
                return woodWorkSite.GetMoneyUpgradeText();
        }
        else if (resource == ResourceType.Ore)
        {
            if (getResource)
                return oreWorkSite.GetResourceUpgradeText();
            else
                return oreWorkSite.GetMoneyUpgradeText();
        }

        throw new System.NotImplementedException("Invalid resource type for GetSiteUpgradeText().");
    }
    public int GetSiteResourceUpgradeCost(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodWorkSite.resourceUpgradeCost,
        ResourceType.Water => waterWorkSite.resourceUpgradeCost,
        ResourceType.Wood => woodWorkSite.resourceUpgradeCost,
        ResourceType.Ore => oreWorkSite.resourceUpgradeCost,

        _ => throw new System.NotImplementedException("Invalid resource type for GetSiteResourceUpgradeCost().")
    };
    public int GetSiteMoneyUpgradeCost(ResourceType resource) => resource switch
    {
        ResourceType.Food => foodWorkSite.moneyUpgradeCost,
        ResourceType.Water => waterWorkSite.moneyUpgradeCost,
        ResourceType.Wood => woodWorkSite.moneyUpgradeCost,
        ResourceType.Ore => oreWorkSite.moneyUpgradeCost,

        _ => throw new System.NotImplementedException("Invalid resource type for GetSiteMoneyUpgradeCost().")
    };
    #endregion
}