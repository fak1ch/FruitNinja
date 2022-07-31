using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalRotation : MonoBehaviour
{
    [SerializeField] private float _minRotateSpeed = 50;
    [SerializeField] private float _maxRotateSpeed = 200;

    private float _rotateMultiplier = 1f;
    private float _rotateSpeed;

    public float TimeScale;

    public float Multiplier
    {
        get
        {
            return _rotateMultiplier;
        }
        set
        {
            _rotateMultiplier = value;
        }
    }
    public float RotateSpeed
    {
        get
        {
            return _rotateSpeed;
        }
        set
        {
            _rotateSpeed = value;
        }
    }

    private void Awake()
    {
        if (Random.Range(0, 2) == 0)
            _rotateMultiplier = -1f;

        _rotateSpeed = Random.Range(_minRotateSpeed, _maxRotateSpeed);
    }

    private void Update()
    {
        RotateAroundLocal();
    }

    private void RotateAroundLocal()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime * _rotateMultiplier * TimeScale);
    }
}
