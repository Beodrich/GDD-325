using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchie : MonoBehaviour
{
    private float speed;
    private AIPath agent;
    [SerializeField] private float startTimeBtwJump= 0.3f;
    public float time;
   
    // Start is called before the first frame update
    void Start()
    {
       agent  = GetComponent<AIPath>();
        speed = agent.maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(JumpTime());

    }
    IEnumerator JumpTime() {
        agent.maxSpeed = 0;
        yield  return new WaitForSeconds(time);
        agent.maxSpeed = speed;
    
    }
}
