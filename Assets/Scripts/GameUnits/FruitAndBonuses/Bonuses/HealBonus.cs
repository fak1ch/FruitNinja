using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : UnitCanCut
{
    [SerializeField] private int _addHealCount;
    [SerializeField] private float _timeUntilDestroy = 10f;

    private void Awake()
    {
        StartCoroutine(DestroyBonus());
    }

    protected override void CutResult()
    {
        _mainObjects.HealthHandler.AddHealth(_addHealCount);
        Destroy(gameObject);
    }

    private IEnumerator DestroyBonus()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        _mainObjects.UnitsContainer.RemoveUnit(this);
        Destroy(gameObject);
    }
}
