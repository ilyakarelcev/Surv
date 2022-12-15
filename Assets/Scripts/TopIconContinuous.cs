using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopIconContinuous : TopIcon
{

    [SerializeField] private Image _loadImage;

    public override void SetEffect(Effect effect)
    {
        base.SetEffect(effect);
        ContinuousEffect continuousEffect = effect as ContinuousEffect;
        continuousEffect.OnLoad.AddListener(UpdateLoad);
        _loadImage.gameObject.SetActive(true);
    }

    public void UpdateLoad(float percent) {
        _loadImage.fillAmount = (1 - percent);
    }


}
