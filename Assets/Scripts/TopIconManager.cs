using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopIconManager : MonoBehaviour
{

    [SerializeField] private TopIcon[] _topIconsOneTime;
    [SerializeField] private TopIcon[] _topIconsContinuous;

    public void AddIcon(Effect effect)
    {
        if (effect is OneTimeEffect)
        {
            foreach (var item in _topIconsOneTime)
            {
                if (!item.CurrentEffect)
                {
                    item.SetEffect(effect);
                    break;
                }
            }
        }
        if (effect is ContinuousEffect)
        {
            foreach (var item in _topIconsContinuous)
            {
                if (!item.CurrentEffect)
                {
                    item.SetEffect(effect);
                    break;
                }
            }
        }
    }

}
