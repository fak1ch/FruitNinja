using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOMoveY(transform.position.y - _offsetValueY, _animationMoveSpeed));
        mySequence.Insert(0f, _spriteRenderer.DOFade(0, _animationFadeSpeed));

        StartCoroutine(DestroyGameObject(_timeUntilDestroy));
    }

    private IEnumerator DestroyGameObject(float time)
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        Destroy(gameObject);
    }
}
