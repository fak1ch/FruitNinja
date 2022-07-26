using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBonus : UnitCanCut
{
    [SerializeField] private float _timeScale = 0.2f;
    [SerializeField] private float _timeFreeze = 2f;

    protected override void CutResult()
    {
        StartCoroutine(FreezeTime());
    }

    private IEnumerator FreezeTime()
    {
        Time.timeScale = _timeScale;
        _spriteRenderer.enabled = false;
        _unitShadow.gameObject.SetActive(false);
        yield return new WaitForSeconds(_timeFreeze);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
