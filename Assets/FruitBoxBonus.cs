using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBoxBonus : UnitCanCut
{
    protected override void CutResult()
    {
        Debug.Log("SpawnManyFruits");
    }
}
