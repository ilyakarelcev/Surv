using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{

    [SerializeField] private float _distanceToCollect = 2f;
    //[SerializeField] private int _collectedExperience;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Image _experienceScale;

    private GameManager _gameManager;

    private float _experience = 0;
    private float _nextLevelExperience = 5;
    private Collider[] _colliders = new Collider[10];

    // кривая показывает сколько надо набрать опыта для повышения уровня в каждом уровне
    // дефолтное значение для первого уровня
    [SerializeField] private AnimationCurve _experienceCurve = AnimationCurve.Linear(0, 0, 50, 300);
    private CoinCounter _coinCounter;

    public void Init(GameManager gameManager, CoinCounter coinCounter)
    {
        _gameManager = gameManager;
        _coinCounter = coinCounter;
        _gameManager.OnUpLevel += SetNextLevelExperience;
    }

    private void FixedUpdate()
    {
        float radius = _distanceToCollect * (1 + _player.CollectionDistanceBoost);
        int numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, radius, _colliders, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < numberOfColliders; i++)
        {
            if (_colliders[i].GetComponent<Loot>() is Loot loot)
            {
                if (loot.LootType == LootType.Experience)
                {
                    loot.Collect(this);
                }
                else
                {
                    float distance = Vector3.Distance(transform.position, loot.transform.position);
                    if (distance < 2f)
                    {
                        loot.Collect(this);
                    }
                }

            }
        }
    }

    public void CollectExperienceLoot()
    {
        //_collectedExperience++;
        _experience++;
        if (_experience >= _nextLevelExperience)
        {
            _gameManager.UpLevelDelayed();
        }
        DisplayExperience();
    }

    public void CollectCoin(int number)
    {
        _coinCounter.AddCoins(number);
    }

    public void SetNextLevelExperience(int level)
    {
        _experience = 0;
        _nextLevelExperience = 5 + _experienceCurve.Evaluate(level);
        DisplayExperience();
    }

    private void DisplayExperience()
    {
        _experienceScale.fillAmount = (int)_experience / _nextLevelExperience;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToCollect * (1 + _player.CollectionDistanceBoost));
    }
#endif

}
