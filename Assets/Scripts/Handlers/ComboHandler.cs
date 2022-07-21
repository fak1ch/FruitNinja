using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    [SerializeField] private float _timeBetweenCuts = 0.3f;
    [SerializeField] private int _minCutsForCombo = 2;

    [Space(10)]
    [SerializeField] private Blade _blade;
    [SerializeField] private ComboDrawingUI _comboDrawingUI;
    [SerializeField] private ScoreDrawingUI _scoreDrawingUI;

    private int _cutsCount = 0;
    private int _totalScore = 0;

    private Coroutine _checkTimeBetweenCuts;

    private void OnEnable()
    {
        _blade.OnFruitsCutten += CheckComboCut;
    }

    private void OnDisable()
    {
        _blade.OnFruitsCutten -= CheckComboCut;
    }

    private void CheckComboCut(int cuttenFruitsCount, int totalScore)
    {
        if (_checkTimeBetweenCuts != null)
            StopCoroutine(_checkTimeBetweenCuts);

        _cutsCount += cuttenFruitsCount;
        _totalScore += totalScore;

        _checkTimeBetweenCuts = StartCoroutine(CheckTimeBetweenCuts());
    }

    private IEnumerator CheckTimeBetweenCuts()
    {
        yield return new WaitForSeconds(_timeBetweenCuts);

        if (_cutsCount >= _minCutsForCombo)
        {
            HappenedComboCuts();
        }
        else
        {
            _scoreDrawingUI.AddScore(_totalScore);
            _cutsCount = 0;
            _totalScore = 0;
        }
    }

    private void HappenedComboCuts()
    {
        _comboDrawingUI.ShowComboPanel(_cutsCount);

        _totalScore *= _cutsCount;

        _scoreDrawingUI.AddScore(_totalScore);
        _cutsCount = 0;
        _totalScore = 0;
    }
}
