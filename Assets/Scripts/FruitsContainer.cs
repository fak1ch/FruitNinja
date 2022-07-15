using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] _fruitPrefabs;

    [SerializeField] private List<Fruit> _currentFruits = new List<Fruit>();

    public void AddFruit(Fruit fruit)
    {
        _currentFruits.Add(fruit);
    }

    public void RemoveFruit(Fruit fruit)
    {
        _currentFruits.Remove(fruit);
    }

    public GameObject GetRandomFruitPrefab()
    {
        GameObject fruit = _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)];

        return fruit;
    }

    public List<Fruit> GetFruitsWhichLocatedInCircle(float x, float y, float radius)
    {
        List<Fruit> fruits = new List<Fruit>();

        for(int i = 0; i < _currentFruits.Count; i++)
        {
            float fruitX = _currentFruits[i].transform.position.x;
            float fruitY = _currentFruits[i].transform.position.y;

            if (Mathf.Pow(x - fruitX, 2) + Mathf.Pow(y - fruitY, 2) <= Mathf.Pow(radius, 2))
            {
                fruits.Add(_currentFruits[i]);
            }
        }

        return fruits;
    }
}
