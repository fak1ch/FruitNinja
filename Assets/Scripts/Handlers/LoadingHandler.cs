using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{
    [SerializeField] private float _timeBetweenLoadScene = 1f;
    [SerializeField] private Animator _loadingScreenAnimator;

    private int _animIDLoadScene;

    private void Start()
    {
        _animIDLoadScene = Animator.StringToHash("LoadScene");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(PlayAnimAndLoadScene(sceneName));
    }

    private IEnumerator PlayAnimAndLoadScene(string sceneName)
    {
        _loadingScreenAnimator.SetTrigger(_animIDLoadScene);
        yield return new WaitForSeconds(_timeBetweenLoadScene);
        SceneManager.LoadScene(sceneName);
    }
}
