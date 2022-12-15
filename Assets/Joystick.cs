using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eccentric {
    public class Joystick : MonoBehaviour
    {

        [SerializeField] RectTransform _joystickRect;
        [SerializeField] Transform _stickTransform;
        [SerializeField] private RectTransform _zone;
        private Vector2 _startClickPosition;

        public Vector2 Value;

        void Update()
        {
            Vector3 mousePosition = Input.mousePosition;

            Debug.Log(_zone.rect);
            if (_zone.rect.Contains(mousePosition)) {

                if (Input.GetMouseButtonDown(0))
                {
                    _startClickPosition = mousePosition;
                    transform.position = _startClickPosition;
                }

                if (Input.GetMouseButton(0))
                {
                    Vector2 delta = (Vector2)mousePosition - _startClickPosition;
                    float radius = (_joystickRect.sizeDelta.x / 2f) * _joystickRect.lossyScale.x;
                    Vector2 clamed = Vector2.ClampMagnitude(delta, radius);
                    _stickTransform.position = _startClickPosition + clamed;
                    Value = clamed / radius;
                }

            }
            

            if (Input.GetMouseButtonUp(0))
            {
                transform.position = new Vector3(Screen.width / 2, Screen.height / 5);
                _stickTransform.localPosition = Vector3.zero;
                Value = Vector2.zero;
            }
        }
    }
}

