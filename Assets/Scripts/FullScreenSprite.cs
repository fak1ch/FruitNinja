using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        float cameraHeight = _mainCamera.orthographicSize * 2;

        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        Vector2 scale = transform.localScale;

        if (cameraSize.x >= cameraSize.y)
        {
            scale *= cameraSize.x / spriteSize.x;
        }

        transform.localScale = scale;
    }
}
