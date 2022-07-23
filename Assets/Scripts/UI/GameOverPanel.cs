using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _bestScore;

    [Space(10)]
    [SerializeField] private ScoreDrawingUI _scoreDrawingUI;
    [SerializeField] private ComplexSpawnHandler _unitSpawner;
    [SerializeField] private HealthHandler _healthHandler;
    [SerializeField] private LoadingHandler _loadingHandler; 

    public void PauseTheGame()
    {
        _unitSpawner.PauseSpawnUnits = true;
    }

    public void ShowGameOverPanel()
    {
        gameObject.SetActive(true);

        int currentScore = _scoreDrawingUI.GetCurrentScore;
        int bestScore = _scoreDrawingUI.GetBestScore;

        _currentScore.text = $"{currentScore}";
        _bestScore.text = $"{bestScore}";
    }

    public void RestartGame()
    {
        _unitSpawner.PauseSpawnUnits = false;
        _healthHandler.SetMaxHealth();
        _scoreDrawingUI.RestartCurrentScore();

        gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        _loadingHandler.LoadScene("MainMenu");
    }
}
