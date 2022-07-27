using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HearthUI : MonoBehaviour
{
    private HealthHandler _healthHandler;
    private int _addHealthCount;

    public void MoveToPosition(HealthHandler healthHandler, int healthCount)
    {
        _healthHandler = healthHandler;
        _addHealthCount = healthCount;

        DOTween.Init(false);
        transform.DOMove(healthHandler.GetPositionToNextHeart(), 2).OnComplete(HealthComeToPosition);
    }

    private void HealthComeToPosition()
    {
        _healthHandler.AddHealth(_addHealthCount);
        Destroy(gameObject);
    }
}
