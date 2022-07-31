using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HearthUI : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 2f;
    private HealthHandler _healthHandler;
    private int _addHealthCount;

    public void MoveToPosition(HealthHandler healthHandler, int healthCount)
    {
        _healthHandler = healthHandler;
        _addHealthCount = healthCount;

        DOTween.Init(false);


        var position = healthHandler.GetPositionToNextHeart();

        if (position == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.DOMove((Vector2)position, _moveDuration).OnComplete(HealthComeToPosition);
        }
    }

    private void HealthComeToPosition()
    {
        _healthHandler.AddHealth(_addHealthCount);
        Destroy(gameObject);
    }
}
