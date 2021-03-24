using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Follow : MonoBehaviour
{
    Animator animator;
    private Transform target;
    public float speed = 3;
    public float stopDistance = 3;
    public static bool isInStopDistnace = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        GetComponent<Boss_Follow>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            isInStopDistnace = false;
        }
        else
        {
            isInStopDistnace = true;
        }
    }
}
