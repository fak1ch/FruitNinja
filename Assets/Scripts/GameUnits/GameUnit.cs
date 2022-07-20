using System;
using System.Collections;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    [Space(10)]
    [SerializeField] protected Color _shadowColor;
    [SerializeField] protected UnitShadow _unitShadow;
    [SerializeField] protected PhysicalMovement _physicalMovement;
    [SerializeField] protected PhysicalRotation _physicalRotation;
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    [Space(10)]
    [SerializeField] private float _startScale;
    [SerializeField] private float _endScale;
    [SerializeField] private float _scaleSpeed;

    protected Utils _utils;

    private GameObject _spriteObject;

    public PhysicalMovement GetPhysicalMovement => _physicalMovement;

    private void Start()
    {
        _utils = new Utils();

        _spriteObject = _physicalRotation.gameObject;
        _spriteObject.transform.localScale = new Vector2(_startScale, _startScale);

        _unitShadow.SetShadowSprite(_spriteRenderer.sprite, _shadowColor);
        _unitShadow.SetShadowRotation(_physicalRotation);
        _unitShadow.SetShadowScaling(_startScale, _endScale);
    }

    private void Update()
    {
        ChangeUnitScale();
    }

    private void ChangeUnitScale()
    {
        Vector2 currentScale = _spriteObject.transform.localScale;

        currentScale.x -= Time.deltaTime * _scaleSpeed;
        currentScale.y -= Time.deltaTime * _scaleSpeed;

        currentScale.x = Mathf.Clamp(currentScale.x, _endScale, currentScale.x);
        currentScale.y = Mathf.Clamp(currentScale.y, _endScale, currentScale.y);

        _spriteObject.transform.localScale = currentScale;

        _unitShadow.ChangeShadowScale(_spriteObject.transform.localScale);
        _unitShadow.ChangeShadowPosition(_spriteObject.transform.position, _spriteObject.transform.localScale);
    }
}
