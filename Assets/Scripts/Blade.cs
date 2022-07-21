using System;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public event Action<int, int> OnFruitsCutten;

    [SerializeField] private float _minBladeSpeedForCut = 0.01f;
    [SerializeField] private float _bladeRadius = 0.2f;
    [SerializeField] private float _speedChangeBladeColor = 1f;
    [SerializeField] private bool _bladeCanCut;

    [Space(10)]
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private UnitsContainer _fruitsContainer;

    private Vector3 _lastFrameMousePosition;
    private Vector3 _currentFrameMousePosition;
    private Color _startBladeEndColor;

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
            _trailRenderer.enabled = true;

            Vector2 differentPosition = _currentFrameMousePosition - _lastFrameMousePosition;

            if (differentPosition.sqrMagnitude > _minBladeSpeedForCut)
            {
                _bladeCanCut = true;
            }
            else
            {
                _bladeCanCut = false;
            }
        }
        else
        {
            _trailRenderer.enabled = false;

            _bladeCanCut = false;
        }

        _lastFrameMousePosition = _currentFrameMousePosition;

        if (_bladeCanCut == true)
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

        Color newBladeColorEffect = _trailRenderer.endColor;

        for(int i = 0; i < gameUnits.Count; i++)
        {
            _fruitsContainer.RemoveUnit(gameUnits[i]);
            gameUnits[i].CutThisGameUnit(mousePosition);

            if (gameUnits[i].gameObject.TryGetComponent(out Fruit fruit) == true)
            {
                fruitCuttenCount++;
                totalScore += fruit.GetScorePrice();
            }

            newBladeColorEffect = gameUnits[i].GetBladeColorCut;
        }

        _trailRenderer.endColor = newBladeColorEffect;

        if (fruitCuttenCount > 0)
            OnFruitsCutten?.Invoke(fruitCuttenCount, totalScore);
    }
}
