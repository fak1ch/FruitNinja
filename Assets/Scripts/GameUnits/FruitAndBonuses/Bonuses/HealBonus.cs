using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : UnitCanCut
{
    [SerializeField] private int _addHealCount;
    [SerializeField] private HearthUI _hearthUI;

    protected override void CutResult()
    {
        var hearth = Instantiate(_hearthUI, _mainObjects.UITransform);
        hearth.transform.position = transform.position;
        hearth.MoveToPosition(_mainObjects.HealthHandler, _addHealCount);
        Destroy(gameObject);
    }
}
