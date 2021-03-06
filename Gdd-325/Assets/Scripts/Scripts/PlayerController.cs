using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    //attack animations
    private const string Monke_BA = "MonkE_BackAttack";
    private const string Monke_BLA = "MonkE_BackLeftAttack";
    private const string Monke_BRA = "MonkE_BackRightAttack";
    private const string Monke_FA = "MonkE_FrontAttack";
    private const string Monke_FLA = "MonkE_FrontLeftAttack";
    private const string Monke_FRA = "MonkE_FrontRightAttack";
    private const string Monke_LA = "MonkE_LeftAttack";
    private const string Monke_RA = "MonkE_RightAttack";

    //heath
    public float health = 1f;
    //[SerializeField]private Text healthText;
    private bool canTakeDamage = true;
    private float timeUntilCanTakeDamge = 0f;
    public float maxTime = 5f;
    private Camera camera;
    public Vector2 playerDirection;
    // spell

    private bool fire;
    private bool ice;
    private bool earth;
    private bool wind;

    public bool isFire()
    {
        return fire;
    }

    public void setFire(bool FIRE)
    {
        this.fire = FIRE;
    }
    public bool isIce()
    {
        return ice;
    }

    public void setIce(bool ICE)
    {
        this.ice = ICE;
    }
    public bool isWind()
    {
        return wind;
    }

    public void setWind(bool WIND)
    {
        this.wind = WIND;
    }
    public bool isEarth()
    {
        return earth;
    }

    public void setEarth(bool EARTH)
    {
        this.earth = EARTH;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animatorLogic = GetComponent<AnimatorLogic>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = Vector2.zero;
        if (health <= 0)
        {
            SceneManager.LoadScene("YouLose");
        }

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(h, v);
        transform.Translate(movement * movementSpeed * Time.deltaTime);
        var mousePos = Input.mousePosition;
        // var charPos = Camera.WorldToScreenPoint((Vector3)this.gameObject.transform.position);

        if (PauseMenu.GameIsPaused == false)
        {
            ChangeAnimation(mousePos);
        }


        //transform.Translate(movement * movementSpeed * Time.deltaTime);
        /* timer += 1;
         if (Input.GetKeyDown("space") && timer >= 175)
         {
             mySpell.CastSpell((Vector3)movement);
             timer = 0;
         }*/
        //invisibility frame timer
        //Debug.Log("Can Take Damage: " + canTakeDamage);
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

    private void ChangeAnimation(Vector2 mousePos)
    {
        var player = camera.WorldToScreenPoint(gameObject.transform.position);
        playerDirection = mousePos - (Vector2)player;
        playerDirection = playerDirection.normalized;



        if (playerDirection.x <= -0.9f)
        {
            if (!WeaponWand.isShoot)
            {


                animatorLogic.ChangeAnimationState(Monke_L);
            }
            else
            {
                animatorLogic.ChangeAnimationState(Monke_LA);


            }

        }
        else if (playerDirection.x >= 0.9f)
        {
            if (!WeaponWand.isShoot)
            {
                animatorLogic.ChangeAnimationState(Monke_R);
            }
            else
            {
                animatorLogic.ChangeAnimationState(Monke_RA);

            }

        }
        else if (playerDirection.y >= 0.9f)
        {
            if (!WeaponWand.isShoot)
            {
                animatorLogic.ChangeAnimationState(Monke_B);
            }
            else
            {
                animatorLogic.ChangeAnimationState(Monke_BA);
            }

        }
        else if (playerDirection.y <= -0.9f)
        {
            if (!WeaponWand.isShoot)
            {
                animatorLogic.ChangeAnimationState(Monke_F);
            }
            else
            {

                animatorLogic.ChangeAnimationState(Monke_FA);
            }

        }


        else if (playerDirection.y >= 0.5f && playerDirection.x <= -0.5f)
        {
            if (!WeaponWand.isShoot)
            {
                animatorLogic.ChangeAnimationState(Monke_BL);
            }
            else
            {
                animatorLogic.ChangeAnimationState(Monke_BLA);

            }
        }

        else if (playerDirection.y >= 0.5f && playerDirection.x >= 0.5f)
        {
            if (!WeaponWand.isShoot)
            {
                animatorLogic.ChangeAnimationState(Monke_BR);
            }
            else
            {

                animatorLogic.ChangeAnimationState(Monke_BRA);
            }

        }
        else if (playerDirection.y <= -0.5f && playerDirection.x <= -0.5f)
        {
            if (!WeaponWand.isShoot)
            {
                animatorLogic.ChangeAnimationState(Monke_FL);
            }
            else
            {

                animatorLogic.ChangeAnimationState(Monke_FLA);
            }

        }
        else if (playerDirection.y <= -0.5f && playerDirection.x >= 0.5f)
        {
            if (!WeaponWand.isShoot)
            {
                animatorLogic.ChangeAnimationState(Monke_FR);
            }
            else
            {
                animatorLogic.ChangeAnimationState(Monke_FRA);

            }
        }
        else
        {
            //stop 
        }




        //function to figure out what animation to play 

    }


    public void TakeDamage(float amount)
    {
        if (canTakeDamage)
        {


            health -= amount;
            HeathManaBar.Damage(amount);//update the heath bar
            canTakeDamage = false;


        }
        //healthText.text = "Current HP is at :"+ health.ToString();
        if (health <= 0)
        {
            Debug.Log("You are dead");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //play golem attack animation here 
           // TakeDamage(1);
        }



    }
}
   
