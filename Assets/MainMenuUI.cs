using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private void Start()
    {
        _bestScoreText.text = $"{PlayerPrefs.GetInt(SaveKeys.BestScoreKey)}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
