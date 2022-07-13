using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float _gravityScale = 9.81f;
    [SerializeField] private float _airDrug = 2f;
    [SerializeField] private float _impulseMultiplierX = 10;
    [SerializeField] private float _impulseMultiplierY = 6;

    [Space(10)]
    [SerializeField] private Vector2 _velocityVector;
    [SerializeField] private float _timeUntilDestroy = 5f;

    private void Start()
    {
        Destroy(gameObject, _timeUntilDestroy);
    }

    private void Update()
    {
        transform.Translate(_velocityVector * Time.deltaTime * _gravityScale);

        _velocityVector.y = EarthGravity(_velocityVector.y);
    }

    public void CutThisFruit()
    {
        gameObject.SetActive(false);
    }

    public void SetPointWhereToFly(Vector3 pointPosition)
    {
        Vector2 _moveDirection = pointPosition - transform.position;

        float impulseY = Random.Range(_impulseMultiplierY, _impulseMultiplierY * 2);

        _velocityVector = new Vector2(_moveDirection.x / _impulseMultiplierX, _moveDirection.y / impulseY);
    }

    private float EarthGravity(float number)
    {
        if (number != 0)
        {
            if (_airDrug != 0)
            {
                number -= _gravityScale * Time.deltaTime / _airDrug;
            }
            else
            {
                number -= _gravityScale * Time.deltaTime;
            }
        }

        return number;
    }
}
