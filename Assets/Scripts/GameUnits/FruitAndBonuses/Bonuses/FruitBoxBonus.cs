using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBoxBonus : UnitCanCut
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _explosivePower = 0.02f;
    [SerializeField] private int _minFruitsCount = 3;
    [SerializeField] private int _maxFruitsCount = 5;
    [SerializeField] private Sprite _emptyBoxSprite;

    protected override void CutResult()
    {
        _spriteRenderer.sprite = _emptyBoxSprite;

        int count = Random.Range(_minFruitsCount, _maxFruitsCount + 1);

        List<UnitCanCut> fruits = new List<UnitCanCut>();

        for (int i = 0; i < count; i++)
        {
            Vector2 position = transform.position;
            float offsetX = Random.Range(-_offsetX, _offsetX);
            float offsetY = Random.Range(-_offsetY, _offsetY);
            position.x += offsetX;
            position.y += offsetY;

            var prefab = _mainObjects.UnitsContainer.GetRandomFruitPrefab();
            var fruit = Instantiate(prefab, position, Quaternion.identity);
            fruit.SetMainObjects(_mainObjects);
            fruits.Add(fruit);
            _mainObjects.UnitsContainer.AddUnit(fruit);
        }

        for (int i = 0; i < fruits.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, fruits[i].transform.position);
            Vector2 moveVector = fruits[i].transform.position - transform.position;
            moveVector *= _explosivePower / distance;
            fruits[i].PhysicalMovement.AddToVelocityVector(moveVector);
        }
    }

    protected override void CutSprite(Vector2 mousePosition)
    {

    }
}
