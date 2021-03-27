using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    private Vector3 localScale;
    private Boss golem;
    private void Start()
    {
        localScale = transform.localScale;
        golem = GetComponentInParent<Boss>();
    }
    private void Update()
    {
        localScale.x = golem.getHP();
        transform.localScale = localScale;
    }
}
