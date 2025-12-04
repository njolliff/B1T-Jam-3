using UnityEngine;

public class Deity
{
    public ResourceType resourceType;
    public float happiness;
    public float offeringCooldown;

    private float _maxHappiness = 100f;

    private float _offeringTimer = 0f;

    public Deity(ResourceType resourceType)
    {
        
    }
}