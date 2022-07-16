using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameUnitsSpawner : MonoBehaviour
{
    [SerializeField] private GameUnitsContainer _fruitsContainer;

    [Space(10)]
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    [Space(10)]
    [SerializeField] private Transform _pointFruitsFlyTo;

    [Space(10)]
    [SerializeField] private SpawnerData _spawnerData;

    private float _timeBetweenWaves;

    private void Start()
    {
        _timeBetweenWaves = _spawnerData.MaxTimeBetweenWaves;
    }

    private void Update()
    {
        if (_timeBetweenWaves <= 0)
        {
            SpawnFruitsWave();
            _timeBetweenWaves = _spawnerData.MaxTimeBetweenWaves;
        }
        else
        {
            _timeBetweenWaves -= Time.deltaTime;
        }
    }

    private void SpawnFruitsWave()
    {
        int gameUnitsMaxCount = Random.Range(_spawnerData.MinGameUnitsInWave, _spawnerData.MaxGameUnitsInWave + 1);

        List<GameObject> gameUnitPrefabs = new List<GameObject>(gameUnitsMaxCount);

        int fruitCount = Random.Range(_spawnerData.MinFruitsInWave, gameUnitsMaxCount);
        for(int i = 0; i < fruitCount; i++)
        {
            gameUnitPrefabs.Add(_fruitsContainer.GetRandomFruitPrefab());
        }

        AddUnitToList(gameUnitPrefabs, _spawnerData.BombSpawnProcent, _fruitsContainer.GetBombPrefab());
        AddUnitToList(gameUnitPrefabs, _spawnerData.BonusSpawnProcent, _fruitsContainer.GetRandomBonusPrefab());

        SpawnGameUnits(gameUnitPrefabs);
    }

    private void AddUnitToList(List<GameObject> units, int procent, GameObject unitPrefab)
    {
        if (CheckWillTheEvent(procent) == true)
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

    private void SpawnGameUnits(List<GameObject> gameUnits)
    {
        for(int i = 0; i < gameUnits.Count; i++)
        {
            var prefab = gameUnits[i];
            Vector2 spawnPosition = GetPointAtSegment(_firstPoint, _secondPoint, Random.Range(0f, 1f));

            var gameUnit = Instantiate(prefab, spawnPosition, Quaternion.identity);

            gameUnit.GetComponent<EarthGravity>().SetPointWhereToFly(_pointFruitsFlyTo.position);
            _fruitsContainer.AddUnit(gameUnit.GetComponent<GameUnit>());
        }
    }

    private bool CheckWillTheEvent(int procent)
    {
        if (procent == 0)
            return false;

        return Random.Range(0, 100 / procent) == 0;
    }

    private Vector2 GetPointAtSegment(Transform firstPoint, Transform secondPoint, float length)
    {
        Vector2 pointPosition = (1 - length) * firstPoint.position + length * secondPoint.position;

        return pointPosition;
    }
}
