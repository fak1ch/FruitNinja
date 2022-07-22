using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDrawingUI : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private float _textLerpSpeed = 1f;

    private string _bestScoreKey;

    private int _score;
    private int _bestScore;
    private int _needScore;
    private float _lerpSpeed;

    private bool _isOpenScore = false;
    private bool _isOpenBestScore = false;

    public int GetCurrentScore => _score;
    public int GetBestScore => _bestScore;

    private void Start()
    {
        _bestScoreKey = SaveKeys.BestScoreKey;
        _bestScore = PlayerPrefs.GetInt(_bestScoreKey, 0);
        _bestScoreText.text = $"{_bestScore}";
    }

    private void Update()
    {
        _score = SmoothInteger(_score, _needScore, ref _isOpenScore);

        if (_isOpenBestScore == true)
        {
            _bestScore = _score;
        }

        RefreshScores();
    }

    public void AddScore(int score)
    {
        if (score > 0)
        {
            _isOpenScore = true;
            _needScore += score;

            if (_score >= _bestScore)
            {
                _isOpenBestScore = true;
                PlayerPrefs.SetInt(_bestScoreKey, _needScore);
            }
        }
    }

    public void SubtractScore(int score)
    {
        _isOpenScore = true;
        _isOpenBestScore = false;

        if (_score - score > 0)
        {
            _score -= score;
        }
    }

    public void RestartCurrentScore()
    {
        _score = 0;
        _needScore = 0;
        RefreshScores();
    }

    private void RefreshScores()
    {
        _currentScoreText.text = $"{_score}";
        _bestScoreText.text = $"{_bestScore}";
    }

    private int SmoothInteger(int value, int needValue, ref bool openingFlag)
    {
        if (openingFlag == true)
        {
            _lerpSpeed += Time.deltaTime * _textLerpSpeed;

            value = (int)Mathf.Lerp(value, needValue, _lerpSpeed);

            if (value == needValue)
            {
                openingFlag = false;
                _lerpSpeed = 0;
            }
        }

        return value;
    }
}
