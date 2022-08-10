using UnityEngine;
using UnityEngine.EventSystems;

public class TapInput : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private VoidEvent tapEvent;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        tapEvent.Raise();    
    }
}