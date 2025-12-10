using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSlider : MonoBehaviour
{
    public Slider slider;

    #region Singleton
    public static GameSpeedSlider Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    #endregion

    void Start()
    {
        GameSpeedChanged();
    }

    public void GameSpeedChanged()
    {
        if (slider != null)
            Time.timeScale = SliderValueToScale(slider.value);
    }

    private float SliderValueToScale(float value) => value switch
    {
        0 => 0f,
        1 => 1f,
        2 => 2f,
        3 => 3f,
        4 => 4f,
        5 => 5f,
        6 => 6f,

        _ => 1f
    };
}