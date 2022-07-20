using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBonus : UnitCanCut
{
    protected override void CutResult()
    {
        Debug.Log("Freeze scene");
    }
}
