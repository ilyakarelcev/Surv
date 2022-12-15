using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameState
{

    //[SerializeField] private GameObject _startMenuObject;
    [SerializeField] private StartMenu _startMenu;
    [SerializeField] private ChapterDisplay _chapterDisplay;
    [SerializeField] private Progress _progress;

    protected override void EnterFirstTime()
    {
        //base.EnterFirstTime();
        Debug.Log(_gameStateManager);
        _startMenu.Init(_gameStateManager);
        _chapterDisplay.Set(_progress.ProgressData.Chapter);
    }



    public override void Enter()
    {
        base.Enter();
        _startMenu.Show();
    }

    public override void Exit()
    {
        _startMenu.Hide();
    }

}
