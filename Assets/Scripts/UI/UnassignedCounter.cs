using TMPro;
using UnityEngine;

public class UnassignedCounter : MonoBehaviour
{
    public TextMeshProUGUI text;

    #region Event Subscription
    void OnEnable()
    {
        EventManager.onWorkerNumberChanged += UpdateText;
    }
    void OnDisable()
    {
        EventManager.onWorkerNumberChanged -= UpdateText;
    }
    #endregion

    void Start()
    {
        // Initialize text
        UpdateText();
    }

    private void UpdateText()
    {
        // Update counter
        if (GameManager.Instance != null)
        {
            text.text = GameManager.Instance.unassignedWorkers.ToString();
        }
    }
}