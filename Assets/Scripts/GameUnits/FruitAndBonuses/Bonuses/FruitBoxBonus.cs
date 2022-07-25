using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBoxBonus : UnitCanCut
{
    [SerializeField] private float _offsetX;  
    [SerializeField] private int _minFruitsCount = 3;
    [SerializeField] private int _maxFruitsCount = 5;
    [SerializeField] private Sprite _emptyBoxSprite;

    protected override void CutResult()
    {
        _spriteRenderer.sprite = _emptyBoxSprite;

        int count = Random.Range(_minFruitsCount, _maxFruitsCount + 1);

        for (int i = 0; i < count; i++)
        {
            Vector2 position = transform.position;
            float offsetX = Random.Range(-_offsetX, _offsetX);
            position.x += offsetX;

            var prefab = _mainObjects.UnitsContainer.GetRandomFruitPrefab();
            var fruit = Instantiate(prefab, position, Quaternion.identity);
            fruit.SetMainObjects(_mainObjects);
            _mainObjects.UnitsContainer.AddUnit(fruit);
        }
    }

    protected override void CutSprite(Vector2 mousePosition)
    {

    }
}
