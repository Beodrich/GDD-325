using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//game components for the game object to have
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimatorLogic))]


public class CopyController : MonoBehaviour
{
    private float h = 0;
    private float v = 0;
    public int movementSpeed;
    Rigidbody2D rb2D;
    AnimatorLogic animatorLogic;

    // monkE stuff
    public static int health = 100;
    
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
    public Spells mySpell;

    public Rigidbody2D fireball;
    public float fireballSpeed = 8f;
    int timer = 175;

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
        else
        {
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
        timer += 1;
        if (Input.GetKeyDown("space") && timer >= 175)
        {
            mySpell.CastSpell((Vector3)movement);
            timer = 0;
        }
        

       /* void castSpell()
        {
            if (Input.GetKeyDown("space") && timer >= 175)
            {
                Instantiate(mySpell);
                timer = 0;
            }
        }*/

        /*timer += 1;
        if (Input.GetKeyDown("space") && timer >= 175)
        {
            //var fireballInst = Instantiate(fireball, transform.position, Quaternion.Euler(hello.Angle(hello.up,hello.right)));
            var fireballInst = Instantiate(fireball, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            fireballInst.velocity = transform.forward * fireballSpeed;
            timer = 0;
        }*/

    }

    public void OnDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
