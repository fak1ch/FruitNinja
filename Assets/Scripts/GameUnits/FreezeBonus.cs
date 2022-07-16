using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBonus : GameUnit
{
    protected override void CutResult()
    {
        Debug.Log("Freeze scene");
    }
}
