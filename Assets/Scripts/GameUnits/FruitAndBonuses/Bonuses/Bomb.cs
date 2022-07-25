using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : UnitCanCut
{
    [SerializeField] private float _bombRadius = 3f;
    [SerializeField] private float _explosivePower = 0.1f;
    [SerializeField] private int _healthCount = 1;

    public override void CutThisGameUnit(Vector2 mousePosition)
    {
        _mainObjects.HealthHandler.RemoveHealth(_healthCount);

        float x = transform.position.x;
        float y = transform.position.y;
        var units = _mainObjects.UnitsContainer.GetUnitsLocatedInCircle(x, y, _bombRadius);

        for(int i = 0; i < units.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, units[i].transform.position);
            Vector2 moveVector = units[i].transform.position - transform.position;
            moveVector *= _explosivePower / distance;
            units[i].PhysicalMovement.AddToVelocityVector(moveVector);
        }

        Destroy(gameObject);
    }
}
