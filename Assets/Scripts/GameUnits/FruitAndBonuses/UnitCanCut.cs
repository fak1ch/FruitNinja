using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCanCut : GameUnit
{
    [Space(10)]
    [SerializeField] private HalfUnit[] _halfPrefabsPool;
    [SerializeField] private int _minCutLineProcent = 15;
    [SerializeField] private int _maxCutLineProcent = 85;

    [Space(10)]
    [SerializeField] protected Color _bladeColorCut;
    [SerializeField] private float _timeUntilDestroy;

    [Space(10)]
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Effect[] _effects;

    protected MainObjects _mainObjects;
    private bool _immortalityOn = false;


    public FruitData _currentUnitData;
    public Color BladeColorCut => _bladeColorCut;

    public virtual bool CutThisGameUnit(Vector2 mousePosition)
    {
        if (_immortalityOn == false)
        {
            CutSprite(mousePosition);
            CreateEffects();
            CutResult();

            return true;
        }

        return false;
    }

    private void CreateEffects()
    {
        if (_particleSystem != null)
        {
            var particles = Instantiate(_particleSystem, transform.position, Quaternion.identity);
            var particlesMain = particles.main;
            particlesMain.startColor = _currentUnitData.ParticleSystem;
        }

        if (_effects.Length > 0)
        {
            for(int i = 0; i < _effects.Length; i++)
            {
                var effect = Instantiate(_effects[i], transform.position, Quaternion.identity);
                effect.transform.position = transform.position;
                effect.Color = _currentUnitData.Effects;
            }
        }
    }

    protected virtual void CutSprite(Vector2 mousePosition)
    {
        int cutLineProcent = Random.Range(_minCutLineProcent, _maxCutLineProcent);

        var sprites = Utils.GetTwoSeparatedSprites(_spriteRenderer.sprite.texture, cutLineProcent);

        Vector2 velocity = _physicalMovement.GetVelocityVector();
        Vector2 newShadowPosition = (Vector2)transform.position + _unitShadow.Offset;

        float scale = _unitShadow.transform.lossyScale.x;

        _halfPrefabsPool[0].SpawnHalfUnit(sprites[0], velocity, newShadowPosition, scale, true);
        _halfPrefabsPool[1].SpawnHalfUnit(sprites[1], velocity, newShadowPosition, scale, false);
    }

    protected virtual void CutResult()
    {
        Destroy(gameObject);
    }

    public void SetMainObjects(MainObjects mainObjects)
    {
        _mainObjects = mainObjects;
    }

    public void SetImmortality(float time)
    {
        StartCoroutine(ImmortalityWork(time));
    }

    private IEnumerator ImmortalityWork(float time)
    {
        _immortalityOn = true;
        yield return new WaitForSeconds(time);
        _immortalityOn = false;
    }
}
