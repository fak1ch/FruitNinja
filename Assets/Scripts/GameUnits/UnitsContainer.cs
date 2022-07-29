using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsContainer : MonoBehaviour
{
    [SerializeField] private FruitData[] _fruitsData;
    [SerializeField] private Fruit _fruitPrefab;

    [Space(10)]
    [SerializeField] private UnitCanCut _bonusHeartPrefab;
    [SerializeField] private UnitCanCut _bonusIceCubePrefab;
    [SerializeField] private UnitCanCut _bonusMagnitePrefab;
    [SerializeField] private UnitCanCut _bonusFruitBoxPrefab;
    [SerializeField] private UnitCanCut _bombPrefab;

    [SerializeField] private List<UnitCanCut> _currentUnits = new List<UnitCanCut>();

    public List<UnitCanCut> CurrentUnits => _currentUnits;
    public int CurrentUnitsCount => _currentUnits.Count;

    public void AddUnit(UnitCanCut gameUnit)
    {
        _currentUnits.Add(gameUnit);
    }

    public void RemoveUnit(UnitCanCut gameUnit)
    {
        _currentUnits.Remove(gameUnit);
    }

    public UnitCanCut GetRandomFruitPrefab()
    {
        int index = Random.Range(0, _fruitsData.Length);

        _fruitPrefab.SetFruitData(_fruitsData[index]);

        return _fruitPrefab;
    }

    public UnitCanCut GetBonusHeart()
    {
        return _bonusHeartPrefab;
    }

    public UnitCanCut GetBonusIceCube()
    {
        return _bonusIceCubePrefab;
    }

    public UnitCanCut GetBonusMagnite()
    {
        return _bonusMagnitePrefab;
    }

    public UnitCanCut GetBonusFruitBox()
    {
        return _bonusFruitBoxPrefab;
    }

    public UnitCanCut GetBombPrefab()
    {
        return _bombPrefab;
    }

    public List<UnitCanCut> GetUnitsLocatedInCircle(float x, float y, float radius)
    {
        List<UnitCanCut> gameUnits = new List<UnitCanCut>();

        for(int i = 0; i < _currentUnits.Count; i++)
        {
            float fruitX = _currentUnits[i].transform.position.x;
            float fruitY = _currentUnits[i].transform.position.y;

            float dx = x - fruitX;
            float dy = y - fruitY;

            if (dx * dx + dy * dy <= radius * radius)
            {
                gameUnits.Add(_currentUnits[i]);
            }
        }

        return gameUnits;
    }

    public void RefreshTimeScaleForUnits()
    {
        for(int i = 0; i < _currentUnits.Count; i++)
        {
            _currentUnits[i].RefreshTimeScale();
        }
    }
}
