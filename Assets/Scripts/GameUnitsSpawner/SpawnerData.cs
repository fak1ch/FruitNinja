using System;

[Serializable]
public class SpawnerData
{
    public float MaxTimeBetweenUnitSpawns = 0.2f;
    public float MinTimeBetweenUnitSpawns = 0.05f;
    public float MaxTimeBetweenWaves = 3f;
    public int MinGameUnitsInWave = 1;
    public int MaxGameUnitsInWave = 6;
    public int MinFruitsInWave = 0;
    public int BombSpawnProcent = 20;
    public int BonusSpawnProcent = 10;
}
