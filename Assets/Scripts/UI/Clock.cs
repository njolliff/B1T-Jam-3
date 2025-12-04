using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI text;

    void OnEnable()
    {
        EventManager.onDayPassed += UpdateClockText;
    }
    void OnDisable()
    {
        EventManager.onDayPassed -= UpdateClockText;
    }

    private void UpdateClockText(int day, int month, int year)
    {
        text.text = $"{year:D2}:{month:D2}:{day:D2}";
    }
}