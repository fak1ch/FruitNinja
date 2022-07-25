using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Range(0,4)]
    [SerializeField] private int _vSyncCount = 0;

    [Range(30, 1000)]
    [SerializeField] private int _targetFrameRate = 120;

    private void Awake()
    {
        QualitySettings.vSyncCount = _vSyncCount;
        Application.targetFrameRate = _targetFrameRate;
    }
}
