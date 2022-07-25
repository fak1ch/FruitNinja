using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
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
        StartCoroutine(DestroyGameObject(_timeUntilDestroy));
    }

    private IEnumerator DestroyGameObject(float time)
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        Destroy(gameObject);
    }
}
