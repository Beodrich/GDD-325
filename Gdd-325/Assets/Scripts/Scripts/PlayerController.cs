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
    public static float health=10f;
    public Text text;
    private bool canTakeDamage = true;
    private float timeUntilCanTakeDamge = 0f;
    public float maxTime = 5f;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animatorLogic = GetComponent<AnimatorLogic>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //mySpell = GetComponent<Spells>();
        //text.text= "Current HP is at :" + heath.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(h, v);
        transform.Translate(movement * movementSpeed * Time.deltaTime);
        var mousePos = Input.mousePosition;
       // var charPos = Camera.WorldToScreenPoint((Vector3)this.gameObject.transform.position);

        ChangeAnimation(mousePos);

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

    private void ChangeAnimation(Vector2 mousePos) {
        var player= camera.WorldToScreenPoint(gameObject.transform.position);
        var playerDirection = mousePos - (Vector2)player;
        playerDirection = playerDirection.normalized;
        Debug.Log(playerDirection);
        Debug.Log("In the if statment");
        //Vector2 movement = new Vector2(h, v);
        //up direction
        /*if (playerDirection.x == 0 && playerDirection.y == 1.0 || playerDirection.x == -0.1f && playerDirection.y == 1.0f) {
            animatorLogic.ChangeAnimationState(Monke_B);
            Debug.Log("Monke going up");
        
        
        
        }
        //down
        if (playerDirection.x == 0 && playerDirection.y == -1.0 )
        {
            animatorLogic.ChangeAnimationState(Monke_F);
            Debug.Log("Monke going Down");



        }*/


        if (playerDirection.x <= -0.9f)
        {
            animatorLogic.ChangeAnimationState(Monke_L);

        }
        else if (playerDirection.x >= 0.9f)
        {
            animatorLogic.ChangeAnimationState(Monke_R);

        }
        else if (playerDirection.y >= 0.9f)
        {
            animatorLogic.ChangeAnimationState(Monke_B);

        }
       else if (playerDirection.y <=-0.9f)
        {
            animatorLogic.ChangeAnimationState(Monke_F);

        }


        else if (playerDirection.y >= 0.5f && playerDirection.x <= -0.5f)
        {
            animatorLogic.ChangeAnimationState(Monke_BL);
            Debug.Log("In the Back left thing");
        }
        else if (playerDirection.y >= 0.5f && playerDirection.x >=0.5f)
        {
            animatorLogic.ChangeAnimationState(Monke_BR);
            Debug.Log("In the Back right");
        }
        else if (playerDirection.y <= -0.5f && playerDirection.x <= -0.5f)
        {
            animatorLogic.ChangeAnimationState(Monke_FL);
            Debug.Log("In the front left");
        }
        else if (playerDirection.y <= -0.5f && playerDirection.x >= 0.5f)
        {
            animatorLogic.ChangeAnimationState(Monke_FR);
            Debug.Log("In the front right thing");
        }
        else { 
            //stop 
        }


        

        //function to figure out what animation to play 

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
            health -= 1;
            canTakeDamage = false;
        }
        text.text = "Current HP is at :"+ health.ToString();
        if (health <= 0) {
            Debug.Log("You are dead");
        }
    }
}