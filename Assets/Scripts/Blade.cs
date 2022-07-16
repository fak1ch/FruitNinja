using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private float _minBladeSpeedForCut = 0.01f;
    [SerializeField] private float _bladeRadius = 0.2f;
    [SerializeField] private bool _bladeCanCutFruit;

    [Space(10)]
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private FruitsContainer _fruitsContainer;

    private Vector3 _lastFrameMousePosition;
    private Vector3 _currentFrameMousePosition;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        _currentFrameMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _currentFrameMousePosition.z = 0;
        transform.position = _currentFrameMousePosition;
        Vector2 differentPosition = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            _trailRenderer.enabled = true;

            differentPosition = _currentFrameMousePosition - _lastFrameMousePosition;

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
            CutFruits(_currentFrameMousePosition);
        }
    }

    private void CutFruits(Vector2 mousePosition)
    {
        float x = _currentFrameMousePosition.x;
        float y = _currentFrameMousePosition.y;

        var fruits = _fruitsContainer.GetFruitsWhichLocatedInCircle(x, y, _bladeRadius);

        if (fruits.Count == 0)
            return;

        for(int i = 0; i < fruits.Count; i++)
        {
            _fruitsContainer.RemoveFruit(fruits[i]);
            fruits[i].CutThisFruit(mousePosition);
        }
    }
}
