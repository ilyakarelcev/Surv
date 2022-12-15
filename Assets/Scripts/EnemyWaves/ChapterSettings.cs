
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class ChapterSettings : ScriptableObject
{
    public EnemyWaves[] EnemyWavesArray;
}

#if UNITY_EDITOR
[CustomEditor(typeof(ChapterSettings))]
public class ChapterSettingsEditor : Editor
{

    //[SerializeField]
    private static string _sceneBefore;
    private const string _chapterSettingsLevelPath = "Assets/Scenes/ChapterSettings.unity";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (string.IsNullOrEmpty(_sceneBefore))
        {
            string currentPath = SceneManager.GetActiveScene().path;
            if (currentPath != _chapterSettingsLevelPath)
            {
                if (GUILayout.Button("Edit"))
                {
                    // Проверяем, что кнопка Edit нажата не в сцене для редактирования
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        _sceneBefore = currentPath;
                        EditorSceneManager.OpenScene(_chapterSettingsLevelPath);
                    }
                }

            }
        }
        else {
            // Возврат к сцене, из которой нажали кнопку Edit
            if (GUILayout.Button("Back"))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
                    EditorSceneManager.OpenScene(_sceneBefore);
                    _sceneBefore = "";
                }
            }
        }
    }
}
#endif
