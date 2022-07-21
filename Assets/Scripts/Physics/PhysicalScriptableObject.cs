using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PhysicalMovement Settings", menuName = "PhysicalMovementSettings")]
public class PhysicalScriptableObject : ScriptableObject
{
    public float GravityScale;
    public float AirDrug;
    public float MinImpulseX;
    public float MaxImpulseX;
    public float MinImpulseY;
    public float MaxImpulseY;
}
