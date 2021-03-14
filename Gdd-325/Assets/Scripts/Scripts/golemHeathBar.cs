using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class golemHeathBar : MonoBehaviour
{
    Vector3 localScale;
    private Golem golem;
    private void Start()
    {
        localScale = transform.localScale;
        golem = GetComponentInParent<Golem>();
    }
    private void Update()
    {
        localScale.x = golem.getGolemHp();
        transform.localScale = localScale;
    }





}
