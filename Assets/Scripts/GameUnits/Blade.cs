    using System;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public event Action<int, int> OnFruitsCutten;

    [SerializeField] private float _minBladeSpeedForCut = 0.01f;
    [SerializeField] private float _bladeRadius = 0.2f;
    [SerializeField] private float _speedChangeBladeColor = 1f;

    [Space(10)]
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private UnitsContainer _unitsContainer;

    private Vector3 _lastFrameMousePosition;
    private Vector3 _currentFrameMousePosition;
    private Color _startBladeEndColor;
    private bool _isFirstClick = true;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _startBladeEndColor = _trailRenderer.endColor;
    }

    private void Update()
    {
        TrailRendererHandler();
        ReturnBladeColorToNormal();
    }

    private void ReturnBladeColorToNormal()
    {
        Color newColor = _trailRenderer.endColor;

        float currentRed = _trailRenderer.endColor.r;
        float currentGreen = _trailRenderer.endColor.g;
        float currentBlue = _trailRenderer.endColor.b;

        newColor.r = Mathf.Lerp(currentRed, _startBladeEndColor.r, Time.deltaTime * _speedChangeBladeColor);
        newColor.g = Mathf.Lerp(currentGreen, _startBladeEndColor.g, Time.deltaTime * _speedChangeBladeColor);
        newColor.b = Mathf.Lerp(currentBlue, _startBladeEndColor.b, Time.deltaTime * _speedChangeBladeColor);

        _trailRenderer.endColor = newColor;
    }

    private void TrailRendererHandler()
    {
        _currentFrameMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _currentFrameMousePosition.z = 0;
        transform.position = _currentFrameMousePosition;

        if (Input.GetMouseButton(0))
        {
            if (_isFirstClick == true)
            {
                _isFirstClick = false;
                _lastFrameMousePosition = _currentFrameMousePosition;
            }

            _trailRenderer.enabled = true;

            Vector2 differentPosition = _currentFrameMousePosition - _lastFrameMousePosition;

            if (differentPosition.sqrMagnitude > _minBladeSpeedForCut)
            {
                TryCutFruits(_currentFrameMousePosition);
            }
        }
        else
        {
            _trailRenderer.enabled = false;
            _isFirstClick = true;
        }

        _lastFrameMousePosition = _currentFrameMousePosition;
    }

    private void TryCutFruits(Vector2 mousePosition)
    {
        float x = _currentFrameMousePosition.x;
        float y = _currentFrameMousePosition.y;

        var gameUnits = _unitsContainer.GetUnitsLocatedInCircle(x, y, _bladeRadius);

        if (gameUnits.Count == 0)
            return;

        int fruitCuttenCount = 0;
        int totalScore = 0;

        Color newBladeColorEffect = _trailRenderer.endColor;

        for(int i = 0; i < gameUnits.Count; i++)
        {
            _unitsContainer.RemoveUnit(gameUnits[i]);
            gameUnits[i].CutThisGameUnit(mousePosition);

            if (gameUnits[i].gameObject.TryGetComponent(out Fruit fruit) == true)
            {
                fruitCuttenCount++;
                totalScore += fruit.GetScorePrice();
            }

            newBladeColorEffect = gameUnits[i].BladeColorCut;
        }

        _trailRenderer.endColor = newBladeColorEffect;

        if (fruitCuttenCount > 0)
            OnFruitsCutten?.Invoke(fruitCuttenCount, totalScore);
    }
}
