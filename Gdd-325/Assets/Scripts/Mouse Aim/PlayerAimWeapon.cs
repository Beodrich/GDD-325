using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs: EventArgs
    {
        public Vector3 spellEndPointPos;
        public Vector3 shootPos;
    }
    private Transform aimTransform;
    private Transform aimWandEndPointTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimWandEndPointTransform = aimTransform.Find("SpellEndPointPosition");
    }
    private void Update()
    {
        HandleAiming();
        HandleShooting();

    }

    private void HandleAiming()
    {
        Vector3 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                               Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        /*Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }
        aimTransform.localScale = localScale;*/
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                               Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                spellEndPointPos = aimWandEndPointTransform.position,
                shootPos = mousePos,
            }); ; 
        }
    }

}
