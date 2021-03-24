using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float attackDistance = 1.5f;
    private Transform target;
    public static bool fireAttack = false;
    public static bool iceAttack = false;
    public static bool isSmashed = false;
    private int heath = 12;
    private Animator animator;
    Animator ani;
    private bool isAttack = false;
    public Transform player;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void SetWalk(Animator animator)
    {
        ani = animator;
        StartCoroutine(IdleToWalk());
    }

    public IEnumerator IdleToWalk()
    {
        yield return new WaitForSeconds(2f);
        ani.SetTrigger("BowlingAttack");
        GetComponent<Boss_Follow>().enabled = true;
    }
}
