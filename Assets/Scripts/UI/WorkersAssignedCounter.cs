using TMPro;
using UnityEngine;

public class WorkersAssignedCounter : MonoBehaviour
{
    public ResourceType resourceType;
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

    private void UpdateText()
    {
        if (JobManager.Instance != null && text != null)
        {
            text.text = JobManager.Instance.GetNumWorkers(resourceType).ToString();
        }
    }
}