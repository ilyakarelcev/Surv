using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopIcon : MonoBehaviour
{

    public Effect CurrentEffect;
    [SerializeField] private Image _image;
    [SerializeField] private Image _background;
    [SerializeField] private Image _lockImage;

    public virtual void SetEffect(Effect effect)
    {
        CurrentEffect = effect;
        _image.sprite = effect.Sprite;
        _lockImage.gameObject.SetActive(false);
        _background.gameObject.SetActive(true);
        _image.gameObject.SetActive(true);
    }

}
