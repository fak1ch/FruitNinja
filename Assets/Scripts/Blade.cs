using System;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public event Action<int, int> OnFruitsCutten;

    [SerializeField] private float _minBladeSpeedForCut = 0.01f;
    [SerializeField] private float _bladeRadius = 0.2f;
    [SerializeField] private bool _bladeCanCutFruit;

    [Space(10)]
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private UnitsContainer _fruitsContainer;

    private Vector3 _lastFrameMousePosition;
    private Vector3 _currentFrameMousePosition;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _currentFrameMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _currentFrameMousePosition.z = 0;
        transform.position = _currentFrameMousePosition;

        if (Input.GetMouseButton(0))
        {
            _trailRenderer.enabled = true;

            Vector2 differentPosition = _currentFrameMousePosition - _lastFrameMousePosition;

            if (differentPosition.sqrMagnitude > _minBladeSpeedForCut)
            {
                _bladeCanCutFruit = true;
            }
            else
            {
                _bladeCanCutFruit = false;
            }
        }
        else
        {
            _trailRenderer.enabled = false;

            _bladeCanCutFruit = false;
        }

        _lastFrameMousePosition = _currentFrameMousePosition;

        if (_bladeCanCutFruit == true)
        {
            TryCutFruits(_currentFrameMousePosition);
        }
    }

    private void TryCutFruits(Vector2 mousePosition)
    {
        float x = _currentFrameMousePosition.x;
        float y = _currentFrameMousePosition.y;

        var gameUnits = _fruitsContainer.GetUnitsLocatedInCircle(x, y, _bladeRadius);

        if (gameUnits.Count == 0)
            return;

        int fruitCuttenCount = 0;
        int totalScore = 0;

        for(int i = 0; i < gameUnits.Count; i++)
        {
            _fruitsContainer.RemoveUnit(gameUnits[i]);
            gameUnits[i].CutThisGameUnit(mousePosition);

            if (gameUnits[i].gameObject.TryGetComponent(out Fruit fruit) == true)
            {
                fruitCuttenCount++;
                totalScore += fruit.GetScorePrice();
            }
        }

        if (fruitCuttenCount > 0)
            OnFruitsCutten?.Invoke(fruitCuttenCount, totalScore);
    }
}
