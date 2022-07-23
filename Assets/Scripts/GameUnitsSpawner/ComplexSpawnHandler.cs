using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexSpawnHandler : MonoBehaviour
{
    [SerializeField] private SpawnerData[] _spawnersData;
    [SerializeField] private UnitsWaveData[] _wavesData;
    [SerializeField] private float _minusTimeIfContainerEmpty = 0.5f;

    [Space(10)]
    [SerializeField] private UnitsContainer _unitsContainer;
    [SerializeField] private ScoreDrawingUI _scoreDrawingUI;

    private Utils _utils;
    [SerializeField] private float _timeBetweenWaves;
    private UnitsWaveData _currentWaveData;
    private int _currentWaveDataIndex;
    private bool _isReduced = false;

    public bool PauseSpawnUnits { get; set; }

    private void Start()
    {
        _currentWaveData = _wavesData[0];
        _currentWaveDataIndex = 0;
        _timeBetweenWaves = _currentWaveData.TimeBetweenWaves;
        _utils = new Utils();
    }

    private void Update()
    {
        if (PauseSpawnUnits == false)
        {
            if (_timeBetweenWaves <= 0)
            {
                SpawnWave();
                _timeBetweenWaves = _currentWaveData.TimeBetweenWaves;
            }
            else
            {
                _timeBetweenWaves -= Time.deltaTime;
            }
        }

        CheckingGameComplication();
        CheckingCurrentUnits();
    }

    private void SpawnWave()
    {
        int gameUnitsMaxCount = Random.Range(_currentWaveData.MinGameUnitsInWave, _currentWaveData.MaxGameUnitsInWave + 1);

        List<UnitCanCut> gameUnitPrefabs = new List<UnitCanCut>(gameUnitsMaxCount);

        int fruitsCount = Random.Range(_currentWaveData.MinFruitsInWave, gameUnitsMaxCount + 1);

        for (int i = 0; i < fruitsCount; i++)
        {
            gameUnitPrefabs.Add(_unitsContainer.GetRandomFruitPrefab());
        }

        AddUnitToList(gameUnitPrefabs, _currentWaveData.BombSpawnProcent, _unitsContainer.GetBombPrefab());
        AddUnitToList(gameUnitPrefabs, _currentWaveData.BonusSpawnProcent, _unitsContainer.GetRandomBonusPrefab());

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
        float timeBetweenUnitSpawns = Random.Range(_currentWaveData.MinTimeBetweenUnitSpawns, _currentWaveData.MaxTimeBetweenUnitSpawns);

        for (int i = 0; i < gameUnits.Count; i++)
        {
            var prefab = gameUnits[i];
            var spawner = GetRandomUnitSpawner();

            var newUnit = spawner.SpawnUnit(prefab);
            _unitsContainer.AddUnit(newUnit);

            yield return new WaitForSeconds(timeBetweenUnitSpawns);
        }
    }

    private void CheckingGameComplication()
    {
        int currentScore = _scoreDrawingUI.GetCurrentScore;
        int i = _currentWaveDataIndex + 1;

        if (i < _wavesData.Length)
        {
            while(i < _wavesData.Length)
            {
                if (currentScore >= _wavesData[_currentWaveDataIndex].WorkLessScore
                    && currentScore <= _wavesData[i].WorkLessScore)
                {
                    _currentWaveData = _wavesData[i];
                    _currentWaveDataIndex = i;
                }

                i++;
            }
        }
    }

    private void CheckingCurrentUnits()
    {
        int unitsCount = _unitsContainer.GetCurrentUnitsCount;

        if (unitsCount == 0 && _isReduced == false)
        {
            _isReduced = true;
            _timeBetweenWaves -= _minusTimeIfContainerEmpty;
        }
        if (unitsCount > 0 && _isReduced == true)
        {
            _isReduced = false;
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
