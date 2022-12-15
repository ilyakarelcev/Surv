using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterDisplay : MonoBehaviour
{

    [SerializeField] private string _prefix = "CHAPTER ";
    [SerializeField] private TextMeshProUGUI _chapterText;

    public void Set(int chapter) {
        _chapterText.text = _prefix + (chapter + 1);
    }

}
