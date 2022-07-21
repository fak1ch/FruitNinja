using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfUnit : GameUnit
{
    [Space(10)]
    [SerializeField] private float _minPlusSpeed = 1.5f;
    [SerializeField] private float _maxPlusSpeed = 2.5f;
    [SerializeField] private float _timeUntilDestroy = 3f;

    public void SpawnHalfUnit(Sprite sprite, Vector2 newVelocity, Vector2 newPosition, float newScale, bool isLeftHalf)
    {
        gameObject.SetActive(true);
        transform.parent = null;
        _physicalMovement.SetVelocityVector(newVelocity);
        _isCanChange = false;
        _unitShadow.SetShadowPosition(newPosition);
        _unitShadow.transform.parent = null;
        _unitShadow.transform.localScale = new Vector2(newScale, newScale);
        _unitShadow.transform.parent = this.gameObject.transform;


        SetSprite(sprite);
        RandomIncreaseVelocityX(isLeftHalf);
        StartCoroutine(DestroyAfterTime());
    }

    private void SetSprite(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }

    private void RandomIncreaseVelocityX(bool isLeftHalf)
    {
        Vector2 velocity = _physicalMovement.GetVelocityVector();

        float plusSpeed = Random.Range(_minPlusSpeed, _maxPlusSpeed);

        plusSpeed *= isLeftHalf ? -1 : 1;
        velocity.x += plusSpeed;

        _physicalMovement.SetVelocityVector(velocity);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        Destroy(gameObject);
    }
}
