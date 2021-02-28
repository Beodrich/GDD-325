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

    // spell stuff
    //private Spells mySpell;

    //public Rigidbody2D fireball;
    //public float fireballSpeed = 8f;
    //int timer = 175;
    
    //heath
    public float heath=10f;
    public Text text;
    private bool canTakeDamage = true;
    private float timeUntilCanTakeDamge = 0f;
    public float maxTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animatorLogic = GetComponent<AnimatorLogic>();
        //mySpell = GetComponent<Spells>();
        //text.text= "Current HP is at :" + heath.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(h, v);


        ChangeAnimation();

        //transform.Translate(movement * movementSpeed * Time.deltaTime);
       /* timer += 1;
        if (Input.GetKeyDown("space") && timer >= 175)
        {
            mySpell.CastSpell((Vector3)movement);
            timer = 0;
        }*/
        //invisibility frame timer
        Debug.Log("Can Take Damage: " + canTakeDamage);
        if (canTakeDamage == false)
        {
            if (timeUntilCanTakeDamge >= maxTime)
            {
                canTakeDamage = true;
                timeUntilCanTakeDamge = 0f;
            }
            else
            {
                timeUntilCanTakeDamge += Time.deltaTime;
            }
        }

    }

    private void ChangeAnimation() {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(h, v);
       
        if (h != 0 && v != 0)
        {
            
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="enemy") {
            TakeDamage();
        }
    }
    void TakeDamage() {
        if (canTakeDamage)
        {
            heath -= 1;
            canTakeDamage = false;
        }
        text.text = "Current HP is at :"+ heath.ToString();
        if (heath <= 0) {
            Debug.Log("You are dead");
        }
    }
}