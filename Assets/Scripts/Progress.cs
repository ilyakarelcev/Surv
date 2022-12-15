#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    [SerializeField] private int _healthLevel;
    [SerializeField] private int _damageLevel;
    [SerializeField] private int _lootLevel;

    [SerializeField] private int _chapter;
    [SerializeField] private int _coins;

    public int HealthLevel
    {
        get => _healthLevel; set
        {
            _healthLevel = value;
            SaveSystem.Save(this);
        }
    }
    public int DamageLevel
    {
        get => _damageLevel; set
        {
            _damageLevel = value;
            SaveSystem.Save(this);
        }
    }
    public int LootLevel
    {
        get => _lootLevel; set
        {
            _lootLevel = value;
            SaveSystem.Save(this);
        }
    }
    public int Chapter
    {
        get => _chapter; set
        {
            _chapter = value;
            SaveSystem.Save(this);
        }
    }
    public int Coins
    {
        get => _coins; set
        {
            _coins = value;
            SaveSystem.Save(this);
        }
    }
}


public class Progress : MonoBehaviour
{
    public ProgressData ProgressData;

    public void Init()
    {
        ProgressData = SaveSystem.Load();
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Progress))]
public class ProgressEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Progress progress = target as Progress;

        if (GUILayout.Button("SAVE"))
        {
            SaveSystem.Save(progress.ProgressData);
        }

        if (GUILayout.Button("LOAD"))
        {
            progress.ProgressData = SaveSystem.Load();
        }
        if (GUILayout.Button("RESET PROGRESS"))
        {
            progress.ProgressData = new ProgressData();
            SaveSystem.Save(progress.ProgressData);
        }
    }
}
#endif
