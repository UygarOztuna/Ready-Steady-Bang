using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.OnShoot();
    }
}
