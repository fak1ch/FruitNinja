using System.Collections;
using UnityEngine;

public class Fruit : GameUnit
{
    [SerializeField] private int _scorePrice = 10;

    public int GetScorePrice()
    {
        return _scorePrice;
    }
}
