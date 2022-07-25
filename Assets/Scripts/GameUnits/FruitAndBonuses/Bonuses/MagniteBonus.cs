using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagniteBonus : UnitCanCut
{
    [SerializeField] private float _magniteRadius = 5f;
    [SerializeField] private float _magnitePower = 0.2f;
    [SerializeField] private float _magniteDuration = 3f;

    private bool _magniteIsWork = false;

    protected override void CutSprite(Vector2 mousePosition)
    {

    }

    protected override void CutResult()
    {
        _magniteIsWork = true;
        StartCoroutine(MagniteWork());
    }

    private void Update()
    {
        if (_magniteIsWork == true)
        {
            _physicalMovement.SetVelocityVector(new Vector2(0, 0));
            _physicalRotation.RotateSpeed = 0;

            float x = transform.position.x;
            float y = transform.position.y;
            var units = _mainObjects.UnitsContainer.GetUnitsLocatedInCircle(x, y, _magniteRadius);

            for (int i = 0; i < units.Count; i++)
            {
                float distance = Vector2.Distance(transform.position, units[i].transform.position);
                Vector2 moveVector = transform.position - units[i].transform.position;
                moveVector.Normalize();
                moveVector *= _magnitePower * distance;
                units[i].PhysicalMovement.SetVelocityVector(moveVector);
            }
        }
    }

    private IEnumerator MagniteWork()
    {
        yield return new WaitForSeconds(_magniteDuration);
        _magniteIsWork = false;
    }
}
