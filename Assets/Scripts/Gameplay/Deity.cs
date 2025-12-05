using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Deity
{
    [Header("Resource")]
    public ResourceType resourceType;
    public int resourcesDemanded;
    public float offeringCooldown;
    public Slider offerringSlider;

    [Header("Happiness")]
    public float happiness;
    public Slider happinessSlider;

    //private float _maxHappiness = 100f;

    //private float _offeringTimer = 0f;

    private void UpdateOfferingTimer(int day, int month, int year)
    {
        
    }
}