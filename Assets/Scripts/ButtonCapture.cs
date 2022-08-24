using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonCapture : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent onClick;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
