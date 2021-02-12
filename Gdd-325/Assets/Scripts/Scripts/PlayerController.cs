using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private const string Monke_B = "MonkE_Back";
    private const string Monke_BL = "Monke_BL";
    private const string Monke_BR = "Monke_BR";
    private const string Monke_F = "Monke_F";
    private const string Monke_FL = "Monke_FL";
    private const string Monke_FR = "Monke_FR";
    private const string Monke_L = "Monke_L";
    private const string Monke_R = "Monke_Right";

   

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animatorLogic = GetComponent<AnimatorLogic>();
    }

    // Update is called once per frame
    void Update()
    {

        ChangeAnimation();

    }

    private void FixedUpdate()
    {
      
       // ChangeAnimation();
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
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(h, v);
       
        if (h != 0 && v != 0)
        {
            Debug.Log("H or v mov is not equal to zero");
            if (movement.y == 1 && movement.x == -1)
            {
                animatorLogic.ChangeAnimationState(Monke_BL);
            }
            if (movement.y == 1 && movement.x == 1)
            {
                animatorLogic.ChangeAnimationState(Monke_BR);
            }
            if (movement.y == -1 && movement.x == -1)
            {
                animatorLogic.ChangeAnimationState(Monke_FL);
            }
            if (movement.y == -1 && movement.x == 1)
            {
                animatorLogic.ChangeAnimationState(Monke_FR);
            }
        }
        else {
            if (movement.x == -1)
            {
                animatorLogic.ChangeAnimationState(Monke_L);

            }
            if (movement.x == 1)
            {
                animatorLogic.ChangeAnimationState(Monke_R);

            }
            if (movement.y == 1)
            {
                animatorLogic.ChangeAnimationState(Monke_B);

            }
            if (movement.y == -1)
            {
                animatorLogic.ChangeAnimationState(Monke_F);

            }

        }
        //function to figure out what animation to play 
        //animatorLogic.ChangeAnimationState(/*throw animation state here*/"hi");
        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }
}