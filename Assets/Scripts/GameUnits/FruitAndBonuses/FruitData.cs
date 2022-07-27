using System;
using UnityEngine;

[Serializable]
public class FruitData
{
    public Sprite FruitSprite;
    public Color BladeEffect;
    public Color ParticleSystem;
    public Color Effects;
    public float StartScale = 1.2f;
    public float EndScale = 0.8f;
    public float ScaleSpeed = 0.2f;
    public int ScorePrice = 1;
}
