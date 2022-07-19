using UnityEngine;

public class Utils
{
    public Utils() { }

    public Sprite[] GetTwoSeparatedSprites(Texture2D texture, float cutLineProcent)
    {
        Sprite[] sprites = new Sprite[2];

        int firstHalfWidth = (int)(texture.width * cutLineProcent / 100);

        float partCount = (100 / cutLineProcent) - 1;
        partCount *= 0.5f;
        float pivotXFirstSprite = 0.5f + partCount;

        partCount = (100 / (100 - cutLineProcent)) - 1;
        partCount *= 0.5f;
        float pivotXSecondSprite = 0.5f - partCount;

        Rect cutRectangleFirst = new Rect(0f, 0f, firstHalfWidth, texture.height);
        Rect cutRectangleSecond = new Rect(firstHalfWidth, 0f, texture.width - firstHalfWidth, texture.height);
        sprites[0] = Sprite.Create(texture, cutRectangleFirst, new Vector2(pivotXFirstSprite, 0.5f), 100);
        sprites[1] = Sprite.Create(texture, cutRectangleSecond, new Vector2(pivotXSecondSprite, 0.5f), 100);

        return sprites;
    }

    public bool CheckRandomless(int procent)
    {
        if (procent == 0)
            return false;

        return Random.Range(0, 100 / procent) == 0;
    }
}
