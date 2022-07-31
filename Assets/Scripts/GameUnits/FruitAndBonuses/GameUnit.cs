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
    [SerializeField] protected float _startScale;
    [SerializeField] protected float _endScale;
    [SerializeField] protected float _scaleSpeed;

    protected GameObject _spriteObject;

    protected bool _canChangeGameUnitScale = true;
    protected bool _canChangeShadowScaleAndPosition = true;

    private float _timeScale;

    public PhysicalMovement PhysicalMovement => _physicalMovement;

    protected virtual void Start()
    {
        RefreshTimeScale();
        _spriteObject = _physicalRotation.gameObject;
        _spriteObject.transform.localScale = new Vector2(_startScale, _startScale);

        _unitShadow.SetShadowSprite(_spriteRenderer.sprite, _shadowColor);
        _unitShadow.SetShadowScaling(_startScale, _endScale);
    }

    protected virtual void Update()
    {
        if (_canChangeGameUnitScale)
            ChangeUnitScale();

        ChangeShadow();
    }

    private void ChangeShadow()
    {
        if (_canChangeShadowScaleAndPosition == true)
        {
            _unitShadow.ChangeShadowScale(_spriteObject.transform.localScale);
            _unitShadow.ChangeShadowPosition(_spriteObject.transform.position, _spriteObject.transform.localScale);
        }
        _unitShadow.ChangeShadowRotation(_spriteObject.transform.rotation);
    }

    private void ChangeUnitScale()
    {
        Vector2 currentScale = _spriteObject.transform.localScale;

        currentScale.x -= Time.deltaTime * _scaleSpeed * _timeScale;
        currentScale.y -= Time.deltaTime * _scaleSpeed * _timeScale;

        currentScale.x = Mathf.Clamp(currentScale.x, _endScale, currentScale.x);
        currentScale.y = Mathf.Clamp(currentScale.y, _endScale, currentScale.y);

        _spriteObject.transform.localScale = currentScale;
    }

    public void RefreshTimeScale()
    {
        float timeScale = Utils.TimeScale;

        _timeScale = timeScale;
        _physicalMovement.TimeScale = timeScale;
        _physicalRotation.TimeScale = timeScale;
    }
}
