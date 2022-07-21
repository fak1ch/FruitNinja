using System.Collections;
using UnityEngine;

public class Fruit : UnitCanCut
{
    [SerializeField] private int _scorePrice = 10;

    public int GetScorePrice()
    {
        return _scorePrice;
    }
}
