using System;
using UnityEngine;

[Serializable]
class PhysicalMovementData
{
    [SerializeField] private PhysicalScriptableObject _physicalScriptableObject;

    [Space(10)]
    public float GravityScale;
    public float AirDrug;
    public float MinImpulseX;
    public float MaxImpulseX;
    public float MinImpulseY;
    public float MaxImpulseY;

    public void SetValuesFromSctiptableObject()
    {
        GravityScale = _physicalScriptableObject.GravityScale;
        AirDrug = _physicalScriptableObject.AirDrug;
        MinImpulseX = _physicalScriptableObject.MinImpulseX;
        MaxImpulseX = _physicalScriptableObject.MaxImpulseX;
        MinImpulseY = _physicalScriptableObject.MinImpulseY;
        MaxImpulseY = _physicalScriptableObject.MaxImpulseY;
    }
}
