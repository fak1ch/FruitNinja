using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : UnitCanCut
{
    [SerializeField] private int _addHealCount;

    protected override void CutResult()
    {
        _mainObjects.HealthHandler.AddHealth(_addHealCount);
        Destroy(gameObject);
    }
}
