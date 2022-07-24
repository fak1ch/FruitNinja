using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBonus : UnitCanCut
{
    [SerializeField] private float _timeScale = 0.2f;
    [SerializeField] private float _timeFreeze = 2f;
    [SerializeField] private float _timeUntilDestroy = 10f;

    private void Awake()
    {
        StartCoroutine(DestroyBonus());
    }

    protected override void CutResult()
    {
        StartCoroutine(FreezeTime());
    }

    private IEnumerator FreezeTime()
    {
        Time.timeScale = _timeScale;
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(_timeFreeze);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

    private IEnumerator DestroyBonus()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        _mainObjects.UnitsContainer.RemoveUnit(this);
        Destroy(gameObject);
    }
}
