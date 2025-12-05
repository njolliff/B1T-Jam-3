using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        GameSpeedChanged();
    }

    public void GameSpeedChanged()
    {
        Time.timeScale = SliderValueToScale(slider.value);
    }

    private float SliderValueToScale(float value) => value switch
    {
        0 => 0f,
        1 => 0.5f,
        2 => 1f,
        3 => 1.5f,
        4 => 2f,
        5 => 2.5f,
        6 => 3f,

        _ => 1f
    };
}