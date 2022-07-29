using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBonus : UnitCanCut
{
    [SerializeField] private float _timeScale = 0.2f;
    [SerializeField] private float _normalTimeScale = 1f;
    [SerializeField] private float _timeFreezeDuration = 1f;

    protected override void CutResult()
    {
        StartCoroutine(FreezeTime());
    }

    private IEnumerator FreezeTime()
    {
        Utils.TimeScale = _timeScale;
        _mainObjects.UnitsContainer.RefreshTimeScaleForUnits();

        _spriteRenderer.enabled = false;
        _unitShadow.gameObject.SetActive(false);
        yield return new WaitForSeconds(_timeFreezeDuration);

        Utils.TimeScale = _normalTimeScale;
        _mainObjects.UnitsContainer.RefreshTimeScaleForUnits();

        Destroy(gameObject);
    }
}
