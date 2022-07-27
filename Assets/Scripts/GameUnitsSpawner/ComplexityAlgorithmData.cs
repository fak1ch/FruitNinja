using System;
using UnityEngine;

[Serializable]
class ComplexityAlgorithmData
{
    public float SecondsUntilDifficultUp = 5;

    [Space(10)]
    public float TimeBetweenUnitSpawnsJump = 0.1f;
    public float TimeBetweenWavesJump = 0.5f;
    public int MaxGameUnitsJump = 1;
}
