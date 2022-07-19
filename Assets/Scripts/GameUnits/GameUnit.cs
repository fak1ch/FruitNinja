using System.Collections;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    [SerializeField] private HalfUnit[] _halfPrefabsPool;
    [SerializeField] private int _minCutLineProcent = 15;
    [SerializeField] private int _maxCutLineProcent = 85;

    [Space(10)]
    [SerializeField] private PhysicalMovement _physicalMovement;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Utils _utils;
    public PhysicalMovement GetPhysicalMovement => _physicalMovement;

    private void Start()
    {
        _utils = new Utils();
    }

    protected virtual void CutResult()
    {

    }

    public virtual void CutThisGameUnit(Vector2 mousePosition)
    {
        float distance = Mathf.Abs(transform.position.x - mousePosition.x);
        Sprite[] sprites = new Sprite[2];

        int cutLineProcent = Random.Range(_minCutLineProcent, _maxCutLineProcent);

        sprites = _utils.GetTwoSeparatedSprites(_spriteRenderer.sprite.texture, cutLineProcent);

        Vector2 velocity = _physicalMovement.GetVelocityVector();

        _halfPrefabsPool[0].SpawnHalfUnit(sprites[0], velocity, true);
        _halfPrefabsPool[1].SpawnHalfUnit(sprites[1], velocity, false);

        CutResult();

        Destroy(gameObject);
    }
}
