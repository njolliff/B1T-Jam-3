using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float selectedScale;
    public float scaleTime;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(new Vector3(selectedScale, selectedScale, 1)));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(Vector3.one));
    }

    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0;

        while (elapsed < scaleTime)
        {
            elapsed += Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / scaleTime);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}