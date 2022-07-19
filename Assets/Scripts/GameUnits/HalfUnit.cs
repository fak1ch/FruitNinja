using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfUnit : MonoBehaviour
{
    [SerializeField] private float _minPlusSpeed = 1.5f;
    [SerializeField] private float _manPlusSpeed = 2.5f;
    [SerializeField] private float _timeUntilDestroy = 3f;

    [Space(10)]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PhysicalMovement _earthGravity;

    public void SpawnHalfUnit(Sprite sprite, Vector2 newVelocity, bool isLeftHalf)
    {
        gameObject.SetActive(true);
        transform.parent = null;
        _earthGravity.SetVelocityVector(newVelocity);

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
        Vector2 velocity = _earthGravity.GetVelocityVector();

        float plusSpeed = Random.Range(_minPlusSpeed, _manPlusSpeed);

        plusSpeed *= isLeftHalf ? -1 : 1;
        velocity.x += plusSpeed;

        _earthGravity.SetVelocityVector(velocity);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_timeUntilDestroy);
        Destroy(gameObject);
    }
}
