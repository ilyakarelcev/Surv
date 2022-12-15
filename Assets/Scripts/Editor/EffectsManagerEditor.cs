using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectsManager))]
public class EffectsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //EffectsManager effectsManager = target as EffectsManager;
        //if (GUILayout.Button(nameof(effectsManager.ShowCards))) {
        //    effectsManager.ShowCards();
        //}
    }
}
