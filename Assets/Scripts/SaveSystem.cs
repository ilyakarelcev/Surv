using UnityEngine;

public static class SaveSystem
{

    private const string _stringName = "Progress";

    public static void Save(ProgressData progressData)
    {

        string dataString = JsonUtility.ToJson(progressData);
        PlayerPrefs.SetString(_stringName, dataString);
    }

    public static ProgressData Load()
    {
        if (PlayerPrefs.HasKey(_stringName))
        {
            string dataString = PlayerPrefs.GetString(_stringName);
            return JsonUtility.FromJson<ProgressData>(dataString);
        }
        else { 
            return new ProgressData();
        }
    }

}
