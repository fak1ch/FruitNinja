using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : UnitCanCut
{
    public override void CutThisGameUnit(Vector2 mousePosition)
    {
        Debug.Log("Caboooom!");
    }
}
