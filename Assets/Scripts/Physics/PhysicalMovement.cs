using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMovement : MonoBehaviour
{
    [SerializeField] private PhysicalMovementData _data;

    [Space(10)]
    [SerializeField] Vector2 _velocity;

    private float _rotateMultiplier = 1f;
    private float _rotateSpeed;

    private void Awake()
    {
        _data.SetValuesFromSctiptableObject();
    }

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
            _rotateMultiplier = -1f;

        _rotateSpeed = Random.Range(_data.MinRotateSpeed, _data.MaxRotateSpeed);

        _data.SetValuesFromSctiptableObject();
    }

    private void Update()
    {
        Vector2 tempPosition = transform.position;
        Vector2 newPosition = tempPosition + _velocity * Time.deltaTime * _data.GravityScale;
        transform.position = Vector2.MoveTowards(transform.position, newPosition, _data.GravityScale);

        _velocity.y = ApplyGravity(_velocity.y);

        RotateAroundLocal();
    }

    private void RotateAroundLocal()
    {
        transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime * _rotateMultiplier);
    }

    public void SetPointWhereToFly(Vector3 pointPosition)
    {
        Vector2 _moveDirection = pointPosition - transform.position;

        float impulseX = Random.Range(_data.MinImpulseX, _data.MaxImpulseX);
        float impulseY = Random.Range(_data.MinImpulseY, _data.MaxImpulseY);

        _velocity = new Vector2(_moveDirection.x * impulseX, _moveDirection.y * impulseY);
    }

    public void SetVelocityVector(Vector2 velocityVector)
    {
        _velocity = velocityVector;
    }

    private float ApplyGravity(float number)
    {
        if (_data.AirDrug != 0)
        {
            number -= _data.GravityScale * Time.deltaTime / _data.AirDrug;
        }
        else
        {
            number -= _data.GravityScale * Time.deltaTime;
        }

        return number;
    }

    public Vector2 GetVelocityVector()
    {
        return _velocity;
    }
}
