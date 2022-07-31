using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBoxBonus : UnitCanCut
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _plusVelocityVectorY = 2;
    [SerializeField] private float _fruitsImmortalityTime = 0.25f;
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
            position.x += offsetX;
            position.y = transform.position.y;

            var prefab = _mainObjects.UnitsContainer.GetRandomFruitPrefab();
            var fruit = Instantiate(prefab, position, Quaternion.identity);
            fruit.SetImmortality(_fruitsImmortalityTime);
            fruit.SetMainObjects(_mainObjects);
            fruits.Add(fruit);
            _mainObjects.UnitsContainer.AddUnit(fruit);
        }

        for (int i = 0; i < fruits.Count; i++)
        {
            Vector2 moveVector = _physicalMovement.GetVelocityVector();
            moveVector.y += _plusVelocityVectorY;
            fruits[i].PhysicalMovement.AddToVelocityVector(moveVector);
        }
    }

    protected override void CutSprite(Vector2 mousePosition)
    {

    }
}
