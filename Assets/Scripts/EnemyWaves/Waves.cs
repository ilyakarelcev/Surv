using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[System.Serializable]
public struct EnemyWaves
{
    public Enemy Enemy;
    public float[] NumberPerSecond;
}

public class Waves : MonoBehaviour
{

    public float ColumnWidth = 0.2f;
    public float CollumnOffset = 1f;

    [SerializeField] private List<WavePoint> _wavePointsList = new List<WavePoint>();
    [SerializeField] private WavePoint _wavePointPrefab;
    [SerializeField] private TextMeshProUGUI[] _levelTexts;
    [SerializeField] private TextMeshProUGUI _levelTextPrefab;
    [SerializeField] private Transform _textParent;

    [SerializeField] private ChapterSettings _levelSettings;

    // Вызывается при нажатии на кнопку. Берет значения из SO и обновляет графики
    public void CreateAllPoint()
    {
        RecreateTexts();

        // Удаляем все старые точки
        for (int i = 0; i < _wavePointsList.Count; i++)
        {
            if(_wavePointsList[i])
                DestroyImmediate(_wavePointsList[i].gameObject);
        }
        _wavePointsList.Clear();

        // Создаем все новые точки исходя из SO
        for (int i = 0; i < _levelSettings.EnemyWavesArray.Length; i++)
        {
            CreatePointsForEnemy(_levelSettings.EnemyWavesArray[i].Enemy, _levelSettings.EnemyWavesArray[i].NumberPerSecond, i * ColumnWidth * 1f);
        }
    }

    void RecreateTexts() {
        for (int i = 0; i < _levelTexts.Length; i++)
        {
            DestroyImmediate(_levelTexts[i].gameObject);
        }
        _levelTexts = new TextMeshProUGUI[50];
        for (int i = 0; i < 50; i++)
        {
            TextMeshProUGUI newText = Instantiate(_levelTextPrefab, _textParent);
            newText.transform.position = new Vector3(i * CollumnOffset, 0f, 0f);
            newText.text = i.ToString();
            _levelTexts[i] = newText;
        }
    }

    public void CreatePointsForEnemy(Enemy enemy, float[] numberPerSecond, float offset)
    {
        for (int i = 0; i < numberPerSecond.Length; i++)
        {
            WavePoint newWavePoint = Instantiate(_wavePointPrefab, transform);
            newWavePoint.name = enemy.name + " " + i;
            float yValue = numberPerSecond[i];
            newWavePoint.Init(this, enemy, i, i * CollumnOffset + offset, yValue);
#if UNITY_EDITOR
            EditorUtility.SetDirty(newWavePoint.gameObject);
#endif
            _wavePointsList.Add(newWavePoint);
            //newWavePoint.transform.position = new Vector3(i + offset, yValue, 0);
        }
    }

    // Записываем в SO значение с графика
    public void SetValue(Enemy enemy, int levelIndex, float value)
    {
        for (int i = 0; i < _levelSettings.EnemyWavesArray.Length; i++)
        {
            if (_levelSettings.EnemyWavesArray[i].Enemy == enemy)
            {
                _levelSettings.EnemyWavesArray[i].NumberPerSecond[levelIndex] = value;
                break;
            }
        }
    }


}

#if UNITY_EDITOR
[CustomEditor(typeof(Waves)), CanEditMultipleObjects]
public class WavesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Waves waves = (Waves)target;

        if (GUILayout.Button("CreatePoints"))
        {
            waves.CreateAllPoint();
        }
    }
}
#endif