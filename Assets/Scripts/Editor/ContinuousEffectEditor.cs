using System;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

[CustomEditor(typeof(ContinuousEffect), true)]
public class ContinuousEffectEditor : Editor
{

    const float width = 100f;

    GUIStyle styleChanged;
    GUIStyle styleDefault;
    GUIStyle summLabel;

    bool showBools = true;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        styleChanged = new GUIStyle(GUI.skin.textField);
        styleChanged.normal.textColor = Color.green;
        styleDefault = new GUIStyle(GUI.skin.textField);
        styleDefault.normal.textColor = Color.white * 0.38f;
        summLabel = new GUIStyle(GUI.skin.label);
        summLabel.normal.textColor = Color.white * 0.42f;

        ContinuousEffect continuousEffect = target as ContinuousEffect;

        showBools = EditorGUILayout.Foldout(showBools, "Active Skills", true);
        if (showBools)
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < ContinuousEffect.GetTotalNumberOfSkills(); i++)
            {
                if (i != 0 && i % 4 == 0)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }

                string name = ((Skill)i).ToString();
                continuousEffect.ActiveSkills[i] = GUILayout.Toggle(continuousEffect.ActiveSkills[i], name, "Button", GUILayout.Width(90f));
            }
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();

        // Колонка с названиями уровней
        EditorGUILayout.BeginVertical(GUILayout.Width(60));
        GUILayout.Label("");
        for (int i = 0; i < 10; i++)
        {
            GUILayout.Label("LVL " + (i + 1));
        }
        EditorGUILayout.EndVertical();

        for (int i = 0; i < ContinuousEffect.GetTotalNumberOfSkills(); i++)
        {
            DrawColumn(continuousEffect, i);
        }

        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(continuousEffect);

    }

    void DrawColumn(ContinuousEffect continuousEffect, int skillIndex)
    {
        if (continuousEffect.ActiveSkills[skillIndex] == false) return;

        string lable = ((Skill)skillIndex).ToString();

        EditorGUILayout.BeginVertical(GUILayout.Width(width));
        float summ = 0;
        GUILayout.Label(lable, GUILayout.Width(width));
        for (int l = 0; l < 10; l++)
        {
            EditorGUILayout.BeginHorizontal();
            float fieldValue = continuousEffect.SkillLevelsAdditions[skillIndex].Values[l];
            GUIStyle style = fieldValue == 0 ? styleDefault : styleChanged;
            float value = EditorGUILayout.FloatField(fieldValue, style, GUILayout.Width(width - 50));
            continuousEffect.SkillLevelsAdditions[skillIndex].Values[l] = value;
            summ += value;
            continuousEffect.SkillLevels[skillIndex].Values[l] = summ;
            GUILayout.Label(summ.ToString(), summLabel);
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndVertical();


    }

}
