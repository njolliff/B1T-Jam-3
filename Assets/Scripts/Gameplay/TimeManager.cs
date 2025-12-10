using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float dayLength;
    public int day, month, year;

    private float _dayTimer = 0;

    void Update()
    {
        HandleTime();
    }

    private void HandleTime()
    {
        // Increment day timer by delta time until day passes
        _dayTimer += Time.deltaTime;
        if (_dayTimer >= dayLength)
        {
            day++;
            
            // Check if month passed
            if (day >= 32)
            {
                day = 0;
                month++;

                // Check if year passed
                if (month >= 13)
                {
                    month = 0;
                    year++;
                }
            }

            DayPassed();
            _dayTimer = 0f;
        }
    }

    private void DayPassed()
    {
        // Update birth / immigration cooldowns
        if (PopulationManager.Instance != null)
            PopulationManager.Instance.DayPassed();

        // Do daily work
        if (JobManager.Instance != null)
            JobManager.Instance.DayPassed();

        // Check for deity demands
        if (DeityManager.Instance != null)
            DeityManager.Instance.DayPassed();
        
        // Update UI
        EventManager.OnDayPassed(day, month, year);
    }
}