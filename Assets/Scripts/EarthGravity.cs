using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthGravity : MonoBehaviour
{
    [SerializeField] private float _gravityScale = 9.81f;
    [SerializeField] private float _airDrug = 10f;
    [SerializeField] private float _impulseMultiplierX = 15;
    [SerializeField] private float _impulseMultiplierY = 8;
    [SerializeField] private float _minRotateSpeed = 50f;
    [SerializeField] private float _maxRotateSpeed = 400f;

    [Space(10)]
    [SerializeField] private Vector2 _velocityVector;

    private float _rotateMultiplier = 1f;
    private float _rotateSpeed;

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
            _rotateMultiplier = -1f;

        _rotateSpeed = Random.Range(_minRotateSpeed, _maxRotateSpeed);
    }

    private void Update()
    {
        Vector2 tempPosition = transform.position;
        Vector2 newPosition = tempPosition + _velocityVector * Time.deltaTime * _gravityScale;
        transform.position = Vector2.MoveTowards(transform.position, newPosition, _gravityScale);

        _velocityVector.y = ApplyEarthGravity(_velocityVector.y);

        RotateAroundLocal();
    }

    private void RotateAroundLocal()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime * _rotateMultiplier);
    }

    public void SetPointWhereToFly(Vector3 pointPosition)
    {
        Vector2 _moveDirection = pointPosition - transform.position;

        float impulseY = Random.Range(_impulseMultiplierY, _impulseMultiplierY * 2);

        _velocityVector = new Vector2(_moveDirection.x / _impulseMultiplierX, _moveDirection.y / impulseY);
    }

    public void SetVelocityVector(Vector2 velocityVector)
    {
        _velocityVector = velocityVector;
    }

    private float ApplyEarthGravity(float number)
    {
        if (_airDrug != 0)
        {
            number -= _gravityScale * Time.deltaTime / _airDrug;
        }
        else
        {
            number -= _gravityScale * Time.deltaTime;
        }

        return number;
    }

    public Vector2 GetVelocityVector()
    {
        return _velocityVector;
    }
}
