using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    [SerializeField] private Blade _blade;
    [SerializeField] private ComboDrawingUI _comboDrawingUI;
    [SerializeField] private float _timeBetweenCuts = 0.3f;

    private int _cutsCount = 0;

    private Coroutine _checkTimeBetweenCuts;

    private void OnEnable()
    {
        _blade.OnFruitsCutten += CheckComboCut;
    }

    private void OnDisable()
    {
        _blade.OnFruitsCutten -= CheckComboCut;
    }

    private void CheckComboCut(int cuttenFruitsCount)
    {
        if (_checkTimeBetweenCuts != null)
            StopCoroutine(_checkTimeBetweenCuts);

        _cutsCount += cuttenFruitsCount;
        _checkTimeBetweenCuts = StartCoroutine(CheckTimeBetweenCuts());
    }

    private IEnumerator CheckTimeBetweenCuts()
    {
        yield return new WaitForSeconds(_timeBetweenCuts);
        HappenedComboCuts();
    }

    private void HappenedComboCuts()
    {
        _comboDrawingUI.ShowComboPanel(_cutsCount);

        _cutsCount = 0;
    }
}
