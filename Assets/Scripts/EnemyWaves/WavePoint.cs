using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
public class WavePoint : MonoBehaviour
{

    [HideInInspector] [SerializeField] private Waves _waves;
    [HideInInspector] [SerializeField] private Enemy _enemy;
    [HideInInspector] [SerializeField] private int _index;
    [HideInInspector] [SerializeField] private float _xPosition;

    public void Init(Waves waves, Enemy enemy, int index, float xPosition, float yPosition)
    {
        _waves = waves;
        _enemy = enemy;
        _index = index;
        _xPosition = xPosition + _waves.ColumnWidth * 0.5f;
        transform.position = new Vector3(_xPosition, yPosition, 0f);
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            if (_enemy == null) return;
            float y = Mathf.Max(0, transform.position.y);
            transform.position = new Vector3(_xPosition, y, 0f);
            _waves.SetValue(_enemy, _index, transform.position.y);
            transform.hasChanged = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_waves == null) return;
        float sizeY = transform.position.y;
        Vector3 size = new Vector3(_waves.ColumnWidth, sizeY, 0f);
        Gizmos.color = _enemy.ColumnColor;
        Gizmos.DrawCube(transform.position - Vector3.up * sizeY * 0.5f, size);
        Gizmos.DrawSphere(transform.position, _waves.ColumnWidth * 0.5f);
    }

}
