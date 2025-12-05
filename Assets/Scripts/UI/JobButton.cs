using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JobButton : MonoBehaviour, IPointerClickHandler
{
    public ResourceType resource;
    public JobButtonType buttonType;

    public Image sprite;
    public Button button;

    public enum JobButtonType { Plus, Minus }
    private bool _isEnabled;

    #region Event Subscription
    void OnEnable()
    {
        EventManager.onWorkerNumberChanged += WorkerNumberChanged;
    }
    void OnDisable()
    {
        EventManager.onWorkerNumberChanged -= WorkerNumberChanged;
    }
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if (JobManager.Instance != null)
        {
            if (buttonType == JobButtonType.Plus)
                JobManager.Instance.AssignWorker(resource);
            else if (buttonType == JobButtonType.Minus)
                JobManager.Instance.UnassignWorker(resource);
        }
    }

    private void WorkerNumberChanged()
    {
        // If button is a plus button, enable button if there are workers available to be assigned
        // Disable if there are no workers to be assigned
        if (buttonType == JobButtonType.Plus && GameManager.Instance != null)
        {
            if (GameManager.Instance.unassignedWorkers > 0 && !_isEnabled)
                EnableButton();
            else if (GameManager.Instance.unassignedWorkers <= 0 && _isEnabled)
                DisableButton();
        }
        // If button is a minus button, enable button if resource work site has workers assigned
        // Disable if work site has no workers
        else if (buttonType == JobButtonType.Minus && JobManager.Instance != null)
        {
            if (JobManager.Instance.GetNumWorkers(resource) > 0 && !_isEnabled)
                EnableButton();
            else if (JobManager.Instance.GetNumWorkers(resource) <= 0 && _isEnabled)
                DisableButton();
        }
    }

    private void EnableButton()
    {
        // Enable sprite and make button interactable
        sprite.enabled = true;
        button.interactable = true;

        _isEnabled = true;
    }
    private void DisableButton()
    {
        // Disable sprite and make button uninteractable
        sprite.enabled = false;
        button.interactable = false;

        _isEnabled = false;
    }
}