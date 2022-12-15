using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchVariant {
    Horizontal,
    Vertical
}

public class RelativeSize : MonoBehaviour {

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _canvasRectTransform;

    [SerializeField] private MatchVariant _matchVariant;
    [Range(0, 1)] [SerializeField] private float _size;

    private void OnValidate() {
        Vector2 backgroundSize;
        if (_matchVariant == MatchVariant.Horizontal) {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.x;
        } else {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.y;
        }
        _rectTransform.sizeDelta = backgroundSize;
    }

}
