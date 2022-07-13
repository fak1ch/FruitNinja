using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _minBladeSpeedForCut = 0.01f;
    [SerializeField] private bool _bladeCanCutFruit;

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

        if (Input.GetMouseButton(0))
        {
            _trailRenderer.enabled = true;

            Vector3 differentPosition = _currentFrameMousePosition - _lastFrameMousePosition;

            Debug.Log(differentPosition.sqrMagnitude);

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fruit fruit))
        {
            fruit.CutThisFruit();
        }
    }
}
