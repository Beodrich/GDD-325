using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum BossStates { 
    spawning,
    rolling,
    waiting,
    none


}
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
    public Transform[] golemsToSpawn;
    public float rate = 1f;
    private BossStates state = BossStates.none;
    private SpawnLogic spawn;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawn = GameObject.Find("SpawnControl").GetComponent<SpawnLogic>();
    }
    private void Update()
    {
       
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
