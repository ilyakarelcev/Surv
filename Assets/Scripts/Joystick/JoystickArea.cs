using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private Joystick _joystick;

    public void Init(Joystick joystick) {
        _joystick = joystick;
    }

    public void OnPointerDown(PointerEventData eventData) {
        
        _joystick.OnDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData) {
        
        _joystick.OnUp(eventData);
    }
}
