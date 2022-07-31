using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Effect : MonoBehaviour
{
    [SerializeField] private float _offsetValueY = 1f;
    [SerializeField] private float _animationMoveSpeed = 3;
    [SerializeField] private float _animationFadeSpeed = 3;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _timeUntilDestroy;

    public Color Color
    {
        set
        {
            _spriteRenderer.color = value;
        }
    }

    private void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Default).SetCapacity(100,200);

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMoveY(transform.position.y - _offsetValueY, _animationMoveSpeed).OnComplete(DestroyGameObject));
        mySequence.Insert(0f, _spriteRenderer.DOFade(0, _animationFadeSpeed));
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
