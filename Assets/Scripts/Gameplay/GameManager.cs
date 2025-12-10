using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Tabs")]
    public GameObject gameplayTabsParent;
    public GameObject gameOverTabsParent;
    [Header("Screens")]
    public GameObject gameOverScreen;
    public GameObject[] otherScreens;

    public static GameManager Instance;

    #region Initialization / Destruction
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

    [ContextMenu("Game Over")]
    public void GameOver()
    {
        // Stop gameplay by setting game speed slider to 0 and making it uninteractable
        if (GameSpeedSlider.Instance != null)
        {
            GameSpeedSlider.Instance.slider.value = 0;
            GameSpeedSlider.Instance.slider.interactable = false;
        }

        // Switch to death screen (and tabs)
        SwitchToDeathScreen();
    }

    public void ReturnToMenu()
    {
        if (SceneManager.GetSceneByName("Main Menu").IsValid())
            SceneManager.LoadScene("Main Menu");
        else
            Debug.Log("No \"Main Menu\" scene found.");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SwitchToDeathScreen()
    {
        // Switch tabs
        gameplayTabsParent.SetActive(false);
        gameOverTabsParent.SetActive(true);

        // Switch screens
        foreach (var screen in otherScreens)
            screen.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}