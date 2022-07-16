using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnitsContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] _fruitPrefabs;
    [SerializeField] private GameObject[] _bonusPrefabs;
    [SerializeField] private GameObject _bombPrefab;

    [SerializeField] private List<GameUnit> _currentUnits = new List<GameUnit>();

    public void AddUnit(GameUnit gameUnit)
    {
        _currentUnits.Add(gameUnit);
    }

    public void RemoveUnit(GameUnit gameUnit)
    {
        _currentUnits.Remove(gameUnit);
    }

    public GameObject GetRandomFruitPrefab()
    {
        GameObject fruit = _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)];

        return fruit;
    }

    public GameObject GetRandomBonusPrefab()
    {
        GameObject bonus = _bonusPrefabs[Random.Range(0, _bonusPrefabs.Length)];

        return bonus;
    }

    public GameObject GetBombPrefab()
    {
        return _bombPrefab;
    }

    public List<GameUnit> GetUnitsWhichLocatedInCircle(float x, float y, float radius)
    {
        List<GameUnit> gameUnits = new List<GameUnit>();

        for(int i = 0; i < _currentUnits.Count; i++)
        {
            float fruitX = _currentUnits[i].transform.position.x;
            float fruitY = _currentUnits[i].transform.position.y;

            if (Mathf.Pow(x - fruitX, 2) + Mathf.Pow(y - fruitY, 2) <= Mathf.Pow(radius, 2))
            {
                gameUnits.Add(_currentUnits[i]);
            }
        }

        return gameUnits;
    }
}
