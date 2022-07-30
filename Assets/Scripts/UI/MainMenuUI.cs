using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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

    public void StartGame()
    {
        if (_isOpen == true)
        {
            _isOpen = false;
            _loadingHandler.LoadScene("SampleScene");
        }
    }

    public void ExitGame()
    {
        if (_isOpen == true)
        {
            _isOpen = false;
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }
}
