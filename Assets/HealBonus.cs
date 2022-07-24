using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : UnitCanCut
{
    [SerializeField] private int _healCount;

    protected override void CutResult()
    {
        Debug.Log("Healing");
    }
}
