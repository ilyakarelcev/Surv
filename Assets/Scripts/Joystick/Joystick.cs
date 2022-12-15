using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum InputType
{
    Mouse,
    Touch
}

[ExecuteAlways]
public class Joystick : MonoBehaviour
{

    [SerializeField] private InputType _inputType;

    [SerializeField] private RectTransform _backgroundTransform;
    [SerializeField] private RectTransform _stickTransform;

    [Range(0, 1)][SerializeField] private float _size;
    [Range(0, 1)][SerializeField] private float _stickSize;

    public Vector2 Value { get; private set; }
    public bool IsPressed { get; private set; }

    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private MatchVariant _matchVariant;

    [HideInInspector] public UnityEvent<Vector2> EventOnDown;
    [HideInInspector] public UnityEvent<Vector2> EventOnPressed;
    [HideInInspector] public UnityEvent<Vector2> EventOnUp;

    [SerializeField] private bool _backgroundFollowPointer;

    [SerializeField] private JoystickArea _joystickArea;

    private void OnValidate()
    {
        UpdateSize();
    }

    private void Awake()
    {
        _joystickArea.Init(this);
    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void UpdateSize()
    {
        Vector2 backgroundSize = GetBackgroundSize();
        _backgroundTransform.sizeDelta = backgroundSize;
        _stickTransform.sizeDelta = backgroundSize * _stickSize;
    }

    Vector2 GetBackgroundSize()
    {
        Vector2 backgroundSize;
        if (_matchVariant == MatchVariant.Horizontal)
        {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.x;
        }
        else
        {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.y;
        }
        return backgroundSize;
    }

    void Start()
    {
#if UNITY_ANDRIOD
        _inputType = InputType.Touch;
#endif
#if UNITY_IOS
        _inputType = InputType.Touch;
#endif
        UpdateSize();
        if (Application.isPlaying)
            Hide();
    }

    private int _fingerId = -1;

    void Update()
    {
        UpdateSize();
        if (!Application.isPlaying) {
            
            return;
        }
        
        if (!IsPressed) return;

        if (_inputType == InputType.Touch)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == _fingerId)
                {
                    OnPressed(touch.position);
                }
            }
        }
        else if (_inputType == InputType.Mouse)
        {
            OnPressed(Input.mousePosition);
        }
    }

    public void OnDown(PointerEventData eventData)
    {
        IsPressed = true;
        Show();
        
        _fingerId = eventData.pointerId;
        _backgroundTransform.position = eventData.position;
        EventOnDown.Invoke(eventData.position);
        // Чтобы не было кадра в котором джойстик на старом месте
        OnPressed(eventData.position);
    }

    void OnPressed(Vector2 touchPosition)
    {
        if (IsPressed == false) return;
        Vector2 toMouse = touchPosition - (Vector2)_backgroundTransform.position;
        float distance = toMouse.magnitude;
        float pixelSize = GetBackgroundSize().x;
        float radius = pixelSize * 0.5f;

        if (_backgroundFollowPointer)
        {
            if (distance > radius)
            {
                Vector2 offset = toMouse - toMouse.normalized * radius;
                _backgroundTransform.position += (Vector3)offset;
            }
        }

        float toMouseClamped = Mathf.Clamp(distance, 0, radius);
        Vector2 stickPosition = toMouse.normalized * toMouseClamped;
        Value = stickPosition / radius;
        _stickTransform.localPosition = stickPosition;
        EventOnPressed.Invoke(touchPosition);
    }

    public void OnUp(PointerEventData eventData)
    {
        if (!IsPressed) return;
        IsPressed = false;
        Hide();
        Value = Vector2.zero;
        _fingerId = -1;
        EventOnUp.Invoke(eventData.position);

    }

    private void Show()
    {
        _backgroundTransform.gameObject.SetActive(true);
        _stickTransform.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _backgroundTransform.gameObject.SetActive(false);
        _stickTransform.gameObject.SetActive(false);
    }

}
