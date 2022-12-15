using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SafeArea : MonoBehaviour
{

    [SerializeField] private RectTransform _rectTransform;
    Rect _safeArea;
    Vector2 _minAncor;
    Vector2 _maxAncor;

    private void Update()
    {
        _safeArea = Screen.safeArea;
        _minAncor = _safeArea.position;
        _maxAncor = _minAncor + _safeArea.size;

        _minAncor.x /= Screen.width;
        _minAncor.y /= Screen.height;
        _maxAncor.x /= Screen.width;
        _maxAncor.y /= Screen.height;

        _rectTransform.anchorMin = _minAncor;
        _rectTransform.anchorMax = _maxAncor;

        

        transform.localScale = Vector3.one;
    }



}
