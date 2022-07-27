using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [Space(10)]
    [SerializeField] private Transform _healthPanel;
    [SerializeField] private GameObject _heartPrefab;

    [Space(10)]
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private UnitsContainer _unityContainer;
    [SerializeField] private GridLayoutGroup _grid;

    [SerializeField] private List<GameObject> _health = new List<GameObject>();

    private float _offset = 0;

    public bool IsMaxHealth => _currentHealth == _maxHealth;
    public bool IsGameOver = false;

    private void Start()
    {
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        AddHealth(_currentHealth);
    }
    

    public void AddHealth(int healthCount)
    {
        if (healthCount <= 0)
            return;

        if (_currentHealth + healthCount > _maxHealth)
            _currentHealth = _maxHealth;
        else
            _currentHealth += healthCount;

        int count = _currentHealth - _health.Count;

        for (int i = 0; i < count; i++)
        {
            var heartPrefab = Instantiate(_heartPrefab, _healthPanel);
            _health.Add(heartPrefab);
        }
    }

    public void RemoveHealth(int healthCount)
    {
        if (healthCount <= 0)
            return;

        if (_currentHealth - healthCount >= 0)
            _currentHealth -= healthCount;
        else
            _currentHealth = 0;

        int count = _health.Count - _currentHealth;

        for (int i = 0; i < count; i++)
        {
            Destroy(_health[i]);
            _health.RemoveAt(i);
        }

        CheckZeroHealth();
    }

    public void SetMaxHealth()
    {
        AddHealth(_maxHealth);
    }

    public Vector2 GetPositionToNextHeart()
    {
        if (_offset == 0)
        {
            _offset = _health[0].transform.position.x - _health[1].transform.position.x;
        }

        Vector2 lastHeartPosition = _health[0].transform.position;
        lastHeartPosition.x += _offset;

        return lastHeartPosition;
    }

    private void CheckZeroHealth()
    {
        if (_currentHealth == 0 && IsGameOver == false)
        {
            IsGameOver = true;
            _gameOverPanel.PauseTheGame();
        }
    }

    private void Update()
    {
        if (_unityContainer.CurrentUnitsCount == 0 && IsGameOver == true)
        {
            IsGameOver = false;
            _gameOverPanel.ShowGameOverPanel();
        }
    }
}
