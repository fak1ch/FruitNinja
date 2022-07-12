using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float _gravityScale = 9.81f;
    [SerializeField] private float _airDrug = 2f;
    [SerializeField] private Vector2 _velocityVector;

    private void Update()
    {
        transform.Translate(_velocityVector * Time.deltaTime * _gravityScale);

        _velocityVector.y = EarthGravity(_velocityVector.y);
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
