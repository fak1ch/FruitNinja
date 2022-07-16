using System.Collections;
using UnityEngine;

public class Fruit : GameUnit
{
    protected override void CutResult()
    {
        Debug.Log("Add score");
    }
}
