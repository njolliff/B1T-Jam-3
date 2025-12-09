using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Variables
    // Serialized
    public bool isDefaultTab = false;

    [Header("Components")]
    public Image sprite;
    public TextMeshProUGUI text;

    [Header("Screens")]
    public GameObject screen;
    public GameObject[] otherScreens;

    [Header("Sprites")]
    public Sprite unselectedSprite;
    public Sprite selectedSprite;

    // Non-Serialized
    public static Tab activeTab;
    #endregion

    #region Initialization / Destruction
    void OnEnable()
    {
        // Set the default active tab
        if (isDefaultTab)
        {
            activeTab = this;
            SetSpriteEnabled();
        }
    }
    void OnDisable()
    {
        if (activeTab == this)
            activeTab = null;
    }
    #endregion

    #region Pointer Methods
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (activeTab != this)
            SetSpriteEnabled();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // If not the active tab, 
        if (activeTab != this)
            SetSpriteDisabled();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (activeTab != this)
        {
            // Set active tab sprite to deselected and become active tab
            activeTab.SetSpriteDisabled();
            activeTab = this;

            // Disable other screens, then enable instance screen
            foreach (var otherScreen in otherScreens)
                otherScreen.SetActive(false);
            if (screen != null)
                screen.SetActive(true);
        }
    }
    #endregion

    #region Sprite Toggle
    private void SetSpriteEnabled()
    {
        if (sprite != null && text != null)
        {
            // Change sprite to selected and move text +2 px
            sprite.sprite = selectedSprite;
            text.rectTransform.localPosition = text.rectTransform.localPosition + Vector3.up * 2;
        }
    }
    public void SetSpriteDisabled()
    {
        if (sprite != null && text != null)
        {
            // Change sprite to unselected and move text -2 px
            sprite.sprite = unselectedSprite;
            text.rectTransform.localPosition = text.rectTransform.localPosition + Vector3.down * 2;
        }
    }
    #endregion
}