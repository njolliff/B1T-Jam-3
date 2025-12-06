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
        if (updatedResource == resource && ResourceManager.Instance != null && text != null)
        {
            text.text = $"{ResourceManager.Instance.GetNumResource(resource):D3}";
        }
    }
}