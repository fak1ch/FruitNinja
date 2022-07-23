using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsContainer : MonoBehaviour
{
    [SerializeField] private UnitCanCut[] _fruitPrefabs;
    [SerializeField] private UnitCanCut[] _bonusPrefabs;
    [SerializeField] private UnitCanCut _bombPrefab;

    [SerializeField] private List<UnitCanCut> _currentUnits = new List<UnitCanCut>();

    public List<UnitCanCut> GetCurrentUnits => _currentUnits;
    public int GetCurrentUnitsCount => _currentUnits.Count;

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
        UnitCanCut fruit = _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)];

        return fruit;
    }

    public UnitCanCut GetRandomBonusPrefab()
    {
        UnitCanCut bonus = _bonusPrefabs[Random.Range(0, _bonusPrefabs.Length)];

        return bonus;
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
}
