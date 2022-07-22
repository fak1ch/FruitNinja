using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Image _image;
    [SerializeField] private Camera _mainCamera;

    private void Awake()
    {
        if (_spriteRenderer != null)
            SetSpriteFullScreen(_spriteRenderer.sprite);

        if (_image != null)
            SetSpriteFullScreen(_image.sprite);
    }

    public void SetSpriteFullScreen(Sprite sprite)
    {
        float cameraHeight = _mainCamera.orthographicSize * 2;

        Vector2 cameraSize = new Vector2(_mainCamera.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = sprite.bounds.size;
        Vector2 scale = transform.localScale;

        if (cameraSize.x >= cameraSize.y)
        {
            scale *= cameraSize.x / spriteSize.x;
        }

        transform.localScale = scale;
    }
}
