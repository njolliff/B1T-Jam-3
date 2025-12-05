using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    public ResourceType resource;
    public TextMeshProUGUI text;

    void OnEnable()
    {
        EventManager.onResourceNumberChanged += UpdateText;
    }
    void OnDisable()
    {
        EventManager.onResourceNumberChanged -= UpdateText;
    }

    void Start()
    {
        UpdateText(resource);
    }

    private void UpdateText(ResourceType updatedResource)
    {
        if (updatedResource == resource && GameManager.Instance != null)
        {
            text.text = $"{GameManager.Instance.GetNumResource(resource):D3}";
        }
    }
}