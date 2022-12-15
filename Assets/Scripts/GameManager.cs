using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private Collector _coinCollector;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private PermanentProgress _permanentProgress;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private Transform _playerTransform;

    public Action<int> OnUpLevel;
    [SerializeField] private TextMeshProUGUI _levelText;
    private int _level = -1;
    [SerializeField] private LevelUpEffect _levelUpEffect;
    [SerializeField] private PauseButton _pauseButton;
    

    public void Init(GameStateManager gameStateManager)
    {
        _player.Init(_permanentProgress);
        _playerHealth.Init(gameStateManager);
        _coinCollector.Init(this, _coinCounter);
        _enemyManager.Init(this, gameStateManager);
        _effectsManager.OnHideCards += WhenHideCards;
        _pauseButton.Init(gameStateManager);
    }

    public void SetFirstLevel()
    {
        UpLevel();
        ShowCards();
    }

    public void UpLevelDelayed() {
        _levelUpEffect.StartEffect(_playerTransform);
        UpLevel();
        Invoke(nameof(ShowCards), 2f);
    }

    private void ShowCards() {
        Time.timeScale = 0f;
        _effectsManager.ShowCards(_level);
    }

    private void UpLevel() {
        _level++;
        // начальный уровень нулевой, но показывать пользователю надо цифру 1
        _levelText.text = (_level + 1).ToString();
        OnUpLevel.Invoke(_level);
    }

    private void WhenHideCards() {
        StartCoroutine(WaitForClick());
    }

    private IEnumerator WaitForClick() {
        //yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Restart();
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
