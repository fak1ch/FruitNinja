using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexSpawnHandler : MonoBehaviour
{
    [SerializeField] private SpawnerData[] _spawnersData;
    [SerializeField] private UnitsContainer _unitsContainer;

    [SerializeField] private UnitsWaveData _waveData;

    private Utils _utils;
    private float _timeBetweenWaves;

    public bool PauseSpawnUnits { get; set; }

    private void Start()
    {
        _timeBetweenWaves = _waveData.MaxTimeBetweenWaves;
        _utils = new Utils();
    }

    private void Update()
    {
        if (PauseSpawnUnits == false)
        {
            if (_timeBetweenWaves <= 0)
            {
                SpawnWave();
                _timeBetweenWaves = _waveData.MaxTimeBetweenWaves;
            }
            else
            {
                _timeBetweenWaves -= Time.deltaTime;
            }
        }
    }

    private void SpawnWave()
    {
        int gameUnitsMaxCount = Random.Range(_waveData.MinGameUnitsInWave, _waveData.MaxGameUnitsInWave + 1);

        List<UnitCanCut> gameUnitPrefabs = new List<UnitCanCut>(gameUnitsMaxCount);

        int unitsCount = Random.Range(_waveData.MinFruitsInWave, gameUnitsMaxCount);

        for (int i = 0; i < unitsCount; i++)
        {
            gameUnitPrefabs.Add(_unitsContainer.GetRandomFruitPrefab());
        }

        AddUnitToList(gameUnitPrefabs, _waveData.BombSpawnProcent, _unitsContainer.GetBombPrefab());
        AddUnitToList(gameUnitPrefabs, _waveData.BonusSpawnProcent, _unitsContainer.GetRandomBonusPrefab());

        StartCoroutine(SpawnGameUnits(gameUnitPrefabs));
    }

    private void AddUnitToList(List<UnitCanCut> units, int procent, UnitCanCut unitPrefab)
    {
        if (_utils.CheckRandomless(procent) == true)
        {
            if (units.Count < units.Capacity)
            {
                units.Add(unitPrefab);
            }
            else
            {
                units[Random.Range(0, units.Count)] = unitPrefab;
            }
        }
    }

    private IEnumerator SpawnGameUnits(List<UnitCanCut> gameUnits)
    {
        float timeBetweenUnitSpawns = Random.Range(_waveData.MinTimeBetweenUnitSpawns, _waveData.MaxTimeBetweenUnitSpawns);

        for (int i = 0; i < gameUnits.Count; i++)
        {
            var prefab = gameUnits[i];
            var spawner = GetRandomUnitSpawner();

            var newUnit = spawner.SpawnUnit(prefab);
            _unitsContainer.AddUnit(newUnit);

            yield return new WaitForSeconds(timeBetweenUnitSpawns);
        }
    }

    private UnitSpawner GetRandomUnitSpawner()
    {
        UnitSpawner unitSpawner = _spawnersData[_spawnersData.Length - 1].UnitsSpawner;

        for(int i = 0; i < _spawnersData.Length; i++)
        {
            if (_utils.CheckRandomless(_spawnersData[i].SpawnProcent) == true)
            {
                unitSpawner = _spawnersData[i].UnitsSpawner;
                break;
            }
        }

        return unitSpawner;
    }
}
