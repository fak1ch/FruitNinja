using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitsSpawner : MonoBehaviour
{
    [SerializeField] private FruitsContainer _fruitsContainer;

    [Space(10)]
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    [Space(10)]
    [SerializeField] private Transform _pointFruitsFlyTo;

    private void Start()
    {
        if (_fruitsContainer == null)
            _fruitsContainer = FindObjectOfType<FruitsContainer>();

        StartCoroutine(SpawnFruitsCorutine());
    }

    private void Update()
    {
        GetPointAtSegment(_firstPoint, _secondPoint, 0.1f);
    }

    private IEnumerator SpawnFruitsCorutine()
    {
        yield return new WaitForSeconds(2f);
        SpawnFruits();
        StartCoroutine(SpawnFruitsCorutine());
    }

    private void SpawnFruits()
    {
        GameObject fruitPrefab = _fruitsContainer.GetRandomFruitPrefab();
        Vector2 spawnPosition = GetPointAtSegment(_firstPoint, _secondPoint, Random.Range(0f, 1f));

        var fruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);

        fruit.GetComponent<EarthGravity>().SetPointWhereToFly(_pointFruitsFlyTo.position);
        _fruitsContainer.AddFruit(fruit.GetComponent<Fruit>());
    }

    private Vector2 GetPointAtSegment(Transform firstPoint, Transform secondPoint, float length)
    {
        Vector2 pointPosition = (1 - length) * firstPoint.position + length * secondPoint.position;

        return pointPosition;
    }
}
