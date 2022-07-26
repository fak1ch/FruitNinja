using System.Collections;
using UnityEngine;
using TMPro;

public class FruitScoreEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private float _timeUntilDestroy;

    public void SetScoreText(int score)
    {
        _scoreText.text = $"{score}";
    }

    private void Start()
    {
        StartCoroutine(DestroyGameObject(_timeUntilDestroy));
    }

    private IEnumerator DestroyGameObject(float time)
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        Destroy(gameObject);
    }
}
