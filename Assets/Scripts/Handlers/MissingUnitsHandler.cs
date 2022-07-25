using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingUnitsHandler : MonoBehaviour
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _timeBetweenCheck = 0.2f;

    [Space(10)]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private UnitsContainer _unitsContainer;
    [SerializeField] private HealthHandler _healthHandler;

    private float _leftCameraPositionX;
    private float _rightCameraPositionX;
    private float _bottomCameraPositionY;

    private Coroutine _checkingMissingNumbers;

    private void Start()
    {
        Vector2 position = _mainCamera.ViewportToWorldPoint(new Vector2(0, 0));

        _leftCameraPositionX = position.x;
        _bottomCameraPositionY = position.y;

        position = _mainCamera.ViewportToWorldPoint(new Vector2(1, 0));

        _rightCameraPositionX = position.x;

        _checkingMissingNumbers = StartCoroutine(CheckingMissingNumbers());
    }

    private IEnumerator CheckingMissingNumbers()
    {
        while (true)
        {
            CheckMissingFruits();
            yield return new WaitForSeconds(_timeBetweenCheck);
        }
    }

    private void CheckMissingFruits()
    {
        var fruits = _unitsContainer.CurrentUnits;

        for(int i = 0; i < fruits.Count; i++)
        {
            if (fruits[i].TryGetComponent(out Fruit fruit))
            {
                if (isOutOfBounds(fruit) == true)
                {
                    _unitsContainer.RemoveUnit(fruit);
                    _healthHandler.RemoveHealth(1);
                    Destroy(fruit.gameObject);
                }
            }
        }
    }

    private bool isOutOfBounds(Fruit fruit)
    {
        float x = fruit.transform.position.x;
        float y = fruit.transform.position.y;

        float leftBoundX = _leftCameraPositionX - _offsetX;
        float rightBoundX = _rightCameraPositionX + _offsetX;
        float bottomBoundY = _bottomCameraPositionY - _offsetY;

        if (x < leftBoundX || x > rightBoundX || y < bottomBoundY)
            return true;

        return false;
    }
}
