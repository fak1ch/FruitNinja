using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCanCut : GameUnit
{
    [Space(10)]
    [SerializeField] private HalfUnit[] _halfPrefabsPool;
    [SerializeField] private int _minCutLineProcent = 15;
    [SerializeField] private int _maxCutLineProcent = 85;

    [Space(10)]
    [SerializeField] private Color _bladeColorCut;

    protected MainObjects _mainObjects;
    public Color BladeColorCut => _bladeColorCut;

    protected virtual void CutResult()
    {

        Destroy(gameObject);
    }

    public virtual void CutThisGameUnit(Vector2 mousePosition)
    {
        float distance = Mathf.Abs(transform.position.x - mousePosition.x);

        int cutLineProcent = Random.Range(_minCutLineProcent, _maxCutLineProcent);

        var sprites = _utils.GetTwoSeparatedSprites(_spriteRenderer.sprite.texture, cutLineProcent);

        Vector2 velocity = _physicalMovement.GetVelocityVector();
        Vector2 newShadowPosition = (Vector2)transform.position + _unitShadow.Offset;

        float scale = _unitShadow.transform.lossyScale.x;

        _halfPrefabsPool[0].SpawnHalfUnit(sprites[0], velocity, newShadowPosition, scale, true);
        _halfPrefabsPool[1].SpawnHalfUnit(sprites[1], velocity, newShadowPosition, scale, false);

        CutResult();
    }

    public void SetFruitData(FruitData fruitData)
    {
        _bladeColorCut = fruitData.BladeEffect;
        _spriteRenderer.sprite = fruitData.FruitSprite;
    }

    public void SetMainObjects(MainObjects mainObjects)
    {
        _mainObjects = mainObjects;
    }
}
