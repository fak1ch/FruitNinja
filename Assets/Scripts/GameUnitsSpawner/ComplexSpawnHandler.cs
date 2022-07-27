using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexSpawnHandler : MonoBehaviour
{
    [SerializeField] private SpawnerData[] _spawnersData;
    [SerializeField] private WaveData _waveData;
    [SerializeField] private ComplexityAlgorithmData _difficultUpData;

    [Space(10)]
    [SerializeField] private float _minusTimeIfContainerEmpty = 0.5f;
    [SerializeField] private int _chanceBetweenFruitAndBonus = 50;

    [Space(10)]
    [SerializeField] private UnitsContainer _unitsContainer;
    [SerializeField] private ScoreDrawingUI _scoreDrawingUI;
    [SerializeField] private HealthHandler _healthHandler;

    private WaveData _startWaveData;
    private float _timeBetweenWaves;
    private float _timeUntilDifficultUp;
    private bool _isReduced = false;

    public bool PauseSpawnUnits { get; set; }

    private void Start()
    {
        _startWaveData = (WaveData)_waveData.Clone();
        _timeBetweenWaves = 1f;
        _timeUntilDifficultUp = _difficultUpData.SecondsUntilDifficultUp;
    }

    private void Update()
    {
        if (PauseSpawnUnits == false)
        {
            if (_timeBetweenWaves <= 0)
            {
                SpawnWave();
                _timeBetweenWaves = _waveData.TimeBetweenWaves;
            }
            else
            {
                _timeBetweenWaves -= Time.deltaTime;
            }

            if (_timeUntilDifficultUp <= 0)
            {
                DifficultUp();
                _timeUntilDifficultUp = _difficultUpData.SecondsUntilDifficultUp;
            }
            else
            {
                _timeUntilDifficultUp -= Time.deltaTime;
            }
        }

        CheckingCurrentUnits();
    }

    private void SpawnWave()
    {
        int gameUnitsMaxCount = Random.Range(_waveData.MinGameUnitsInWave, _waveData.MaxGameUnitsInWave + 1);

        List<UnitCanCut> bonusPrefabs = new List<UnitCanCut>(gameUnitsMaxCount);

        int fruitsCount = Random.Range(_waveData.MinFruitsInWave, gameUnitsMaxCount + 1);

        AddUnitToList(bonusPrefabs, _waveData.BombSpawnProcent, _unitsContainer.GetBombPrefab());

        if (_healthHandler.IsMaxHealth == false)
            AddUnitToList(bonusPrefabs, _waveData.BonusHeartSpawnProcent, _unitsContainer.GetBonusHeart());

        AddUnitToList(bonusPrefabs, _waveData.BonusIceCubeSpawnProcent, _unitsContainer.GetBonusIceCube());
        AddUnitToList(bonusPrefabs, _waveData.BonusMagniteSpawnProcent, _unitsContainer.GetBonusMagnite());
        AddUnitToList(bonusPrefabs, _waveData.BonusFruitBoxSpawnProcent, _unitsContainer.GetBonusFruitBox());

        if (bonusPrefabs.Count > _waveData.maxBonusCountInWave)
        {
            int count = bonusPrefabs.Count - _waveData.maxBonusCountInWave;
            while (count > 0)
            {
                bonusPrefabs.RemoveAt(Random.Range(0, count));
                count--;
            }
        }

        StartCoroutine(SpawnGameUnits(bonusPrefabs, fruitsCount - bonusPrefabs.Count));
    }

    private void AddUnitToList(List<UnitCanCut> units, int procent, UnitCanCut unitPrefab)
    {
        if (Utils.CheckRandomless(procent) == true)
        {
            units.Add(unitPrefab);
        }
    }

    private IEnumerator SpawnGameUnits(List<UnitCanCut> bonusPrefabs, int fruitCount)
    {
        float timeBetweenUnitSpawns = Random.Range(_waveData.MinTimeBetweenUnitSpawns, _waveData.MaxTimeBetweenUnitSpawns);

        while (true)
        {
            bool spawnBonus = Utils.CheckRandomless(_chanceBetweenFruitAndBonus);
            var spawner = GetRandomUnitSpawner();
            UnitCanCut prefab = null;

            if (spawnBonus == true && bonusPrefabs.Count > 0)
            {
                int index = Random.Range(0, bonusPrefabs.Count);
                prefab = bonusPrefabs[index];
                bonusPrefabs.Remove(prefab);
            }
            else if (fruitCount > 0)
            {
                prefab = _unitsContainer.GetRandomFruitPrefab();
                fruitCount--;
            }

            if (prefab != null)
            {
                var newUnit = spawner.SpawnUnit(prefab);
                _unitsContainer.AddUnit(newUnit);
            }

            if (bonusPrefabs.Count == 0 && fruitCount == 0)
                break;

            yield return new WaitForSeconds(timeBetweenUnitSpawns);
        }
    }

    private void DifficultUp()
    {
        _waveData.MinTimeBetweenUnitSpawns -= _difficultUpData.TimeBetweenUnitSpawnsJump;
        _waveData.MaxTimeBetweenUnitSpawns -= _difficultUpData.TimeBetweenUnitSpawnsJump;
        _waveData.MinTimeBetweenUnitSpawns = Mathf.Clamp(_waveData.MinTimeBetweenUnitSpawns, _waveData.LastTimeMaxTimeBetweenUnitSpawns, _waveData.MinTimeBetweenUnitSpawns);
        _waveData.MaxTimeBetweenUnitSpawns = Mathf.Clamp(_waveData.MaxTimeBetweenUnitSpawns, _waveData.LastTimeMaxTimeBetweenUnitSpawns, _waveData.MaxTimeBetweenUnitSpawns);

        _waveData.TimeBetweenWaves -= _difficultUpData.TimeBetweenWavesJump;
        _waveData.TimeBetweenWaves = Mathf.Clamp(_waveData.TimeBetweenWaves, _waveData.MinTimeBetweenWaves, _waveData.TimeBetweenWaves);

        _waveData.MaxGameUnitsInWave += _difficultUpData.MaxGameUnitsJump;
    }

    private void CheckingCurrentUnits()
    {
        int unitsCount = _unitsContainer.CurrentUnitsCount;

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
            if (Utils.CheckRandomless(_spawnersData[i].SpawnProcent) == true)
            {
                unitSpawner = _spawnersData[i].UnitsSpawner;
                break;
            }
        }

        return unitSpawner;
    }

    public void SetCurrentWaveDataAsDefault()
    {
        _waveData = (WaveData)_startWaveData.Clone();
    }
}
