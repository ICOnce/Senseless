using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreePan : MonoBehaviour, IDragHandler
{
    public Transform skillTree;

    public void OnDrag(PointerEventData eventData)
    {
        skillTree.position += (Vector3)eventData.delta;
    }
}
