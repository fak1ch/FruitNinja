using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;

    [Space(10)]
    [SerializeField] private Transform[] _pointsFlyTo;

    [Space(10)]
    [SerializeField] private MainObjects _mainObjects;

    public UnitCanCut SpawnUnit(UnitCanCut prefab)
    {
        Vector2 spawnPosition = GetPointAtSegment(_firstPoint, _secondPoint, Random.Range(0f, 1f));
        UnitCanCut gameUnit = Instantiate(prefab, spawnPosition, Quaternion.identity);
        gameUnit.PhysicalMovement.SetPointWhereToFly(GetRandomPointFlyTo());
        gameUnit.SetMainObjects(_mainObjects);

        return gameUnit;
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
