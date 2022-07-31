using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Space(10)]
    [SerializeField] private ScoreDrawingUI _scoreDrawingUI;
    [SerializeField] private ComplexSpawnHandler _unitSpawner;
    [SerializeField] private HealthHandler _healthHandler;
    [SerializeField] private LoadingHandler _loadingHandler;
    [SerializeField] private GameObject _blade;

    private bool _isButtonsBlocked = false;

    public void PauseTheGame()
    {
        _unitSpawner.PauseSpawnUnits = true;
        _blade.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 1);

        int currentScore = _scoreDrawingUI.CurrentScore;
        int bestScore = _scoreDrawingUI.BestScore;

        _currentScore.text = $"{currentScore}";
        _bestScore.text = $"{bestScore}";

        _isButtonsBlocked = false;
    }

    public void RestartGame()
    {
        if (_isButtonsBlocked == false)
        {
            _isButtonsBlocked = true;
            _canvasGroup.DOFade(0, 1).OnComplete(RestartGameCallback);
        }
    }

    private void RestartGameCallback()
    {
        _unitSpawner.PauseSpawnUnits = false;
        _healthHandler.SetMaxHealth();
        _healthHandler.IsGameOver = false;
        _scoreDrawingUI.RestartCurrentScore();
        _unitSpawner.SetCurrentWaveDataAsDefault();
        _blade.SetActive(true);

        gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        if (_isButtonsBlocked == false)
        {
            _isButtonsBlocked = true;
            _loadingHandler.LoadScene("MainMenu");
        }
    }
}
