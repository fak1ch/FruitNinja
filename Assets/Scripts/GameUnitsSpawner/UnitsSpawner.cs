using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitsSpawner : MonoBehaviour
{
    [SerializeField] private UnitsContainer _unitsContainer;

    [Space(10)]
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    [Space(10)]
    [SerializeField] private Transform[] _pointsFlyTo;

    [Space(10)]
    [SerializeField] private SpawnerData _spawnerData;

    private Utils _utils;
    private float _timeBetweenWaves;

    public bool PauseSpawnUnits { get; set; }

    private void Start()
    {
        _timeBetweenWaves = _spawnerData.MaxTimeBetweenWaves;
        _utils = new Utils();
    }

    private void Update()
    {
        if (PauseSpawnUnits == false)
        {
            if (_timeBetweenWaves <= 0)
            {
                SpawnWave();
                _timeBetweenWaves = _spawnerData.MaxTimeBetweenWaves;
            }
            else
            {
                _timeBetweenWaves -= Time.deltaTime;
            }
        }
    }

    private void SpawnWave()
    {
        int gameUnitsMaxCount = Random.Range(_spawnerData.MinGameUnitsInWave, _spawnerData.MaxGameUnitsInWave + 1);

        List<UnitCanCut> gameUnitPrefabs = new List<UnitCanCut>(gameUnitsMaxCount);

        int unitsCount = Random.Range(_spawnerData.MinFruitsInWave, gameUnitsMaxCount);
        for(int i = 0; i < unitsCount; i++)
        {
            gameUnitPrefabs.Add(_unitsContainer.GetRandomFruitPrefab());
        }

        AddUnitToList(gameUnitPrefabs, _spawnerData.BombSpawnProcent, _unitsContainer.GetBombPrefab());
        AddUnitToList(gameUnitPrefabs, _spawnerData.BonusSpawnProcent, _unitsContainer.GetRandomBonusPrefab());

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
        float timeBetweenUnitSpawns = Random.Range(_spawnerData.MinTimeBetweenUnitSpawns, _spawnerData.MaxTimeBetweenUnitSpawns);

        for (int i = 0; i < gameUnits.Count; i++)
        {
            var prefab = gameUnits[i];
            Vector2 spawnPosition = GetPointAtSegment(_firstPoint, _secondPoint, Random.Range(0f, 1f));

            UnitCanCut gameUnit = Instantiate(prefab, spawnPosition, Quaternion.identity);

            gameUnit.GetPhysicalMovement.SetPointWhereToFly(GetRandomPointFlyTo());
            _unitsContainer.AddUnit(gameUnit);

            yield return new WaitForSeconds(timeBetweenUnitSpawns);
        }
    }

    private Vector2 GetPointAtSegment(Transform firstPoint, Transform secondPoint, float length)
    {
        Vector2 pointPosition = (1 - length) * firstPoint.position + length * secondPoint.position;

        return pointPosition;
    }

    private Vector2 GetRandomPointFlyTo()
    {
        return _pointsFlyTo[Random.Range(0, _pointsFlyTo.Length)].position;
    }
}
