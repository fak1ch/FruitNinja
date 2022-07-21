using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShadow : MonoBehaviour
{
    [SerializeField] private float _minShadowScale;
    [SerializeField] private Vector2 _shadowMaxOffset;

    [Space(10)]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PhysicalRotation _physicalRotation;

    private float _parentStartScale;
    private float _parentEndScale;
    private Vector2 _currentOffset;

    public Vector2 Offset
    {
        get { return _currentOffset; }
        set { _currentOffset = value; }
    }

    public void SetShadowSprite(Sprite sprite, Color color)
    {
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.color = color;
    }

    public void SetShadowRotation(PhysicalRotation parentRotation)
    {
        _physicalRotation.Multiplier = parentRotation.Multiplier;
        _physicalRotation.RotateSpeed = parentRotation.RotateSpeed;
    }

    public void SetShadowScaling(float parentStartScale, float parentEndScale)
    {
        _parentStartScale = parentStartScale;
        _parentEndScale = parentEndScale;
    }

    public void SetShadowPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void ChangeShadowScale(Vector2 currentScale)
    {
        float procent = GetProcent(_parentEndScale, _parentStartScale, currentScale.x);
        float newScale = (_parentEndScale - _minShadowScale) * (1 - procent);
        newScale += _minShadowScale;

        transform.localScale = new Vector2(newScale, newScale);
    }

    public void ChangeShadowPosition(Vector2 parentCurrentPosition, Vector2 currentScale)
    {
        float procent = GetProcent(_parentEndScale, _parentStartScale, currentScale.x);

        Vector2 newPosition = parentCurrentPosition;
        _currentOffset = _shadowMaxOffset * procent;
        newPosition += _currentOffset;

        transform.position = newPosition;
    }

    private float GetProcent(float a, float b, float value)
    {
        if (b - a == 0)
            return 0;

        return (value - a) / (b - a);
    }
}
