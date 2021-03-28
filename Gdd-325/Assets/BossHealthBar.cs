using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{

    private Vector3 localScale;
    private Boss golem;
    [SerializeField] private TextMeshProUGUI text;
    private void Start()
    {
        localScale = transform.localScale;
        golem = GameObject.Find("Boss").GetComponent<Boss>();
    }
    private void Update()
    {
        localScale.x = golem.getHP();
        localScale.x = localScale.x / 4;
        transform.localScale = localScale;
        text.text = "Boss HP " + golem.getHP() + "/ " + golem.getMaxHP();
    }
}
