using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeZoom : MonoBehaviour, IScrollHandler
{
    public float zoomSpeed = 1f;
    public float minScale = 0.5f;
    public float maxScale = 2f;

    private RectTransform rect;


    public Transform skillTree;
    public Canvas canvas;


    public void OnScroll(PointerEventData eventData)
    {
        rect = skillTree.GetComponent<RectTransform>();

        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect,
            eventData.position,
            canvas.worldCamera,
            out localMousePos
        );

        float oldScale = rect.localScale.x;

        float newScale = Mathf.Clamp(oldScale + eventData.scrollDelta.y * zoomSpeed, minScale, maxScale);
        float scaleFactor = newScale / oldScale;

        rect.localScale = Vector3.one * newScale;

        Vector3 offset = (Vector3)localMousePos * (scaleFactor - 1f);
        rect.localPosition -= offset;
    }
}
