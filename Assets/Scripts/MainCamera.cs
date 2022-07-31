using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    public void ShakeCamera(float duration = 1, float strength = 1, int vibrato = 10, float randomless = 90)
    {
        transform.DOShakePosition(duration, strength, vibrato, randomless);
    }
}
