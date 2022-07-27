using System.Collections;
using UnityEngine;

public class Fruit : UnitCanCut
{
    [SerializeField] private int _scorePrice = 10;
    [SerializeField] private FruitScoreEffect _fruitScoreEffect;

    public int GetScorePrice()
    {
        return _scorePrice;
    }

    protected override void CutResult()
    {
        var fruitScoreEffect = Instantiate(_fruitScoreEffect, transform.position, Quaternion.identity);
        fruitScoreEffect.SetScoreText(_scorePrice);
        base.CutResult();
    }

    public void SetFruitData(FruitData fruitData)
    {
        _currentUnitData = fruitData;

        _bladeColorCut = _currentUnitData.BladeEffect;
        _spriteRenderer.sprite = _currentUnitData.FruitSprite;
        _startScale = _currentUnitData.StartScale;
        _endScale = _currentUnitData.EndScale;
        _scaleSpeed = _currentUnitData.ScaleSpeed;
        _scorePrice = fruitData.ScorePrice;
    }
}
