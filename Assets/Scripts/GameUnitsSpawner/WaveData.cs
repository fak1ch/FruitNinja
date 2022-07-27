using System;
using UnityEngine;

[Serializable]
public class WaveData : ICloneable
{
    public float MaxTimeBetweenUnitSpawns = 0.2f;
    public float MinTimeBetweenUnitSpawns = 0.05f;
    public float LastTimeMaxTimeBetweenUnitSpawns = 0.1f;

    [Space(10)]
    public float TimeBetweenWaves = 3f;
    public float MinTimeBetweenWaves = 1f;

    [Space(10)]
    public int MinGameUnitsInWave = 1;
    public int MaxGameUnitsInWave = 6;
    public int MinFruitsInWave = 0;

    [Space(10)]
    public int BombSpawnProcent = 20;
    public int BonusHeartSpawnProcent = 10;
    public int BonusIceCubeSpawnProcent = 10;
    public int BonusMagniteSpawnProcent = 10;
    public int BonusFruitBoxSpawnProcent = 10;
    public int maxBonusCountInWave = 1;

    public object Clone() => MemberwiseClone();
}
