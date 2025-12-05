using TMPro;
using UnityEngine;

public class GenerationCounter : MonoBehaviour
{
    public ResourceType resourceType;
    public GenerationCounterType counterType;
    public TextMeshProUGUI text;

    public enum GenerationCounterType { Resource, Money }

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
        if (JobManager.Instance != null)
        {
            if (counterType == GenerationCounterType.Resource)
                text.text = JobManager.Instance.GetResourceGenerationRange(resourceType);
            else if (counterType == GenerationCounterType.Money)
                text.text = JobManager.Instance.GetMoneyGenerationRange(resourceType);
        }
    }
}