using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : UnitCanCut
{
    [SerializeField] private float _bombRadius = 3f;
    [SerializeField] private float _explosivePower = 0.1f;
    [SerializeField] private int _healthCount = 1;
    [SerializeField] private GameObject _boomLabelEffect;

    [Space(10)]
    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private float _shakeStrength = 1f;
    [SerializeField] private int _shakeVibrato = 10;
    [SerializeField] private float _shakeRandomless = 1f;

    protected override void CutResult()
    {
        _mainObjects.HealthHandler.RemoveHealth(_healthCount);

        float x = transform.position.x;
        float y = transform.position.y;
        var units = _mainObjects.UnitsContainer.GetUnitsLocatedInCircle(x, y, _bombRadius);

        for (int i = 0; i < units.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, units[i].transform.position);
            Vector2 moveVector = units[i].transform.position - transform.position;
            moveVector *= _explosivePower / distance;
            units[i].PhysicalMovement.AddToVelocityVector(moveVector);
        }

        Instantiate(_boomLabelEffect, transform.position, Quaternion.identity);

        _mainObjects.MainCamera.ShakeCamera(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomless);

        Destroy(gameObject);
    }

    protected override void CutSprite(Vector2 mousePosition)
    {

    }
}
