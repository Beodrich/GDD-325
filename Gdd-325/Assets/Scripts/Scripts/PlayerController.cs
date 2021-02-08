using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//game components for the game object to have
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimatorLogic))]


public class PlayerController : MonoBehaviour
{
    private float h = 0;
    private float v = 0;
    public int movementSpeed;
    Rigidbody2D rb2D;
    AnimatorLogic animatorLogic;
    //animation states
    private const string MonkE_B = "MonkE_B";
    private const string MonkE_BL = "MonkE_B";
    private const string MonkE_BR = "MonkE_BR";
    private const string MonkE_F = "MonkE_F";
    private const string MonkE_FL = "MonkE_FL";
    private const string MonkE_FR = "MonkE_FR";
    private const string MonkE_L = "MonkE_L";
    private const string MonkE_R = "MonkE_R";

    private Animation[] playerStates;




    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animatorLogic = GetComponent<AnimatorLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        MovePlayer();
        ChangeAnimation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if (h != 0 || v != 0)
        {
            RotatePlayer();
            ChangeAnimation();
        }
    }

    

    private void MovePlayer()
    {
        Vector3 directionVector = new Vector3(h, v, 0);
        rb2D.velocity = directionVector.normalized * movementSpeed;
    }

    private void RotatePlayer()
    {
        //Atan2 - Return value is the angle between the x-axis and a 2D vector starting at zero and terminating at (x,y).
        float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void ChangeAnimation() {
        //function to figure out what animation to play 
        animatorLogic.ChangeAnimationState(/*throw animation state here*/"hi");

    }
}