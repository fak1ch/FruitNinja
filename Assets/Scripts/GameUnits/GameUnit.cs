using System.Collections;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    [SerializeField] private GameObject _halfFruitPrefab;
    [SerializeField] private float _maxDistanceValueCutHalf = 0.3f;
    [SerializeField] private float _timeUntilDestroyAfterCut = 3f;

    private EarthGravity _earthGravity;
    private SpriteRenderer _spriteRenderer;

    private GameObject _fruitHalf;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _earthGravity = GetComponent<EarthGravity>();
    }

    protected virtual void CutResult()
    {

    }

    public virtual void CutThisGameUnit(Vector2 mousePosition)
    {
        float distance = Mathf.Abs(transform.position.x - mousePosition.x);
        Sprite[] sprites = new Sprite[2];

        if (distance < _maxDistanceValueCutHalf)
            sprites = GetTwoSeparatedSprites(_spriteRenderer.sprite.texture, 50);
        else
            sprites = GetTwoSeparatedSprites(_spriteRenderer.sprite.texture, 25);

        _fruitHalf = Instantiate(_halfFruitPrefab, transform.position, transform.rotation);

        _fruitHalf.GetComponent<SpriteRenderer>().sprite = sprites[0];
        _spriteRenderer.sprite = sprites[1];

        _fruitHalf.GetComponent<EarthGravity>().SetVelocityVector(_earthGravity.GetVelocityVector());

        CutResult();

        StartCoroutine(DestroyFruitAfterTime());
    }

    public Sprite[] GetTwoSeparatedSprites(Texture2D texture, float cutLineProcent)
    {
        Sprite[] sprites = new Sprite[2];

        int firstHalfWidth = (int)(texture.width * cutLineProcent / 100);

        float pivotXFirstSprite = 0;
        float pivotXSecondSprite = 0;

        if (cutLineProcent == 50)
        {
            pivotXFirstSprite = 1;
            pivotXSecondSprite = 0;
        }
        else if (cutLineProcent == 25)
        {
            pivotXFirstSprite = 1.75f;
            pivotXSecondSprite = 0.3f;
        }

        Rect cutRectangleFirst = new Rect(0f, 0f, firstHalfWidth, texture.height);
        Rect cutRectangleSecond = new Rect(firstHalfWidth, 0f, texture.width - firstHalfWidth, texture.height);
        sprites[0] = Sprite.Create(texture, cutRectangleFirst, new Vector2(pivotXFirstSprite, 0.5f), 100);
        sprites[1] = Sprite.Create(texture, cutRectangleSecond, new Vector2(pivotXSecondSprite, 0.5f), 100);

        return sprites;
    }

    private IEnumerator DestroyFruitAfterTime()
    {
        yield return new WaitForSeconds(_timeUntilDestroyAfterCut);
        Destroy(_fruitHalf);
        Destroy(this.gameObject);
    }
}
