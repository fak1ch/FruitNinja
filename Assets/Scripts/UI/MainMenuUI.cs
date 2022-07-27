using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private LoadingHandler _loadingHandler;

    private bool _isOpen = true;

    private void Start()
    {
        _bestScoreText.text = $"{PlayerPrefs.GetInt(SaveKeys.BestScoreKey)}";
    }

    public void StartGame(RectTransform buttonTransform)
    {
        if (_isOpen == true)
        {
            Utils.PlayButtonAnimation(buttonTransform);
            _loadingHandler.LoadScene("SampleScene");
        }
    }

    public void ExitGame(RectTransform buttonTransform)
    {
        if (_isOpen == true)
        {
            Utils.PlayButtonAnimation(buttonTransform);
            Application.Quit();
        }
    }
}
