using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDrawingUI : MonoBehaviour
{
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private int _score;
    private int _bestScore;

    public void AddScore(int score)
    {
        if (score > 0)
            _score += score;

        RefreshScores();
    }

    public void SubtractScore(int score)
    {
        if (score > 0)
        {
            if (_score - score > 0)
            {
                _score -= score;
            }
        }

        RefreshScores();
    }

    private void RefreshScores()
    {
        _currentScoreText.text = $"{_score}";
        _bestScoreText.text = $"{_score}";
    }
}
