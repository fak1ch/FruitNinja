using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [Space(10)]
    [SerializeField] private Transform _healthPanel;
    [SerializeField] private GameObject _heartPrefab;

    private List<GameObject> _health = new List<GameObject>();

    private void Start()
    {
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        AddHealth(_currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AddHealth(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RemoveHealth(1);
        }
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

    private void CheckZeroHealth()
    {
        if (_currentHealth == 0)
        {
            Debug.Log("GameOver");
        }
    }
}
