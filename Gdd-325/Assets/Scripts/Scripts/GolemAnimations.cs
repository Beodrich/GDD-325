using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum GolemStates
{
    up, down, right, left, none


};
[RequireComponent(typeof(AnimatorLogic))]

public class GolemAnimations : MonoBehaviour
{
    private string golem_Right;
    private string golem_Left;
    private string golem_Up;
    private string golem_Down;
    //attacks
    private string attack_down;
    private string attack_up;
    private string attack_left;
    private string attack_right;
    [Range(0.0f, 20f)]
    public float attackRange;
    AnimatorLogic anim;
    GolemStates movDirection = GolemStates.none;
    private bool isAttacking = false;
    [Range(0.0F, 1F)]
    [SerializeField] private float animationChangeRange = 0.9f;
    PlayerController player;
    private AIPath golemPath;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        golemPath = GetComponent<AIPath>();


        if (gameObject.name == "crawler(Clone)")
        {
            golem_Right = "Crawler_Right";
            golem_Down = "Crawler_Down";
            golem_Left = "Crawler_Left";
            golem_Up = "Crawler_Up";

            attack_down = "Crawler_Attack_Down";
            attack_up = "Crawler_Attack_Up";
            attack_left = "Crawler_Attack_L";
            attack_right = "Crawler_Attack_R";

        }
        else if (gameObject.name == "shooting enemy (Clone)")
        {
            golem_Right = "Golem_Shoot_Right";
            golem_Left = "Golem_Shoot_Left";
            golem_Down = "Golem_Shoot_Down";
            golem_Up = "Golem_Shoot_Up";

        }
        else if (gameObject.name == "Crouchie(Clone)")
        {
            golem_Right = "Golem_Crouch_Right";
            golem_Left = "Golem_Crouch_Left";
            golem_Down = "Golem_Crouch_Down";
            golem_Up = "Golem_Crouch_Up";
            attack_down = "Crouchie_Attack_Down";
            attack_up = "Crouchie_Attack_Up";
            attack_left = "Crouchie_Attack_Left";
            attack_right = "Crouchie_Attack_Right";

        }
        else if (gameObject.name == "Golem(Clone)")
        {
            golem_Right = "Golem_Right";
            golem_Left = "Golem_Left";
            golem_Down = "Golem_Down";
            golem_Up = "Golem_UP";
            attack_down = "Golem_Attack_Down";
            attack_up = "Golem_Attack_Up";
            attack_left = "Golem_Attack_Left";
            attack_right = "Golem_Attack_Right";

        }
        anim = GetComponent<AnimatorLogic>();
    }
    private void Update()
    {
        Vector2 golemVector = golemPath.desiredVelocity.normalized;

        if (!isAttacking)
        {
            // This is for shooting enemy so he faces where the player is
            if (gameObject.name == "shooting enemy (Clone)" && GetComponent<ShootingEnemy>().isShoot)
            {

                Debug.Log("IF STATMENT");
                Vector2 direction = player.transform.position - this.transform.position;
                direction = direction.normalized;

                if (direction.x <= -animationChangeRange)
                {
                    anim.ChangeAnimationState(golem_Left);
                    movDirection = GolemStates.left;

                }
                else if (direction.x > animationChangeRange)
                {
                    anim.ChangeAnimationState(golem_Right);
                    movDirection = GolemStates.right;


                }
                if (direction.y > animationChangeRange)
                {

                    anim.ChangeAnimationState(golem_Up);
                    movDirection = GolemStates.up;


                }
                else if (direction.y <= -animationChangeRange)
                {
                    anim.ChangeAnimationState(golem_Down);
                    movDirection = GolemStates.down;


                }

            }
            else
            {
                if (golemVector.x <= -animationChangeRange)
                {
                    anim.ChangeAnimationState(golem_Left);
                    movDirection = GolemStates.left;


                }
                else if (golemVector.x > animationChangeRange)
                {
                    anim.ChangeAnimationState(golem_Right);
                    movDirection = GolemStates.right;


                }
                if (golemVector.y > animationChangeRange)
                {

                    anim.ChangeAnimationState(golem_Up);
                    movDirection = GolemStates.up;


                }
                else if (golemVector.y <= -animationChangeRange)
                {
                    anim.ChangeAnimationState(golem_Down);
                    movDirection = GolemStates.down;


                }
            }
        }
        //Debug.Log(golemPath.desiredVelocity);
        Debug.Log(Vector2.Distance(this.gameObject.transform.position, player.gameObject.transform.position));
        //attack animations
        if (Vector2.Distance(this.gameObject.transform.position, player.gameObject.transform.position) <= attackRange && gameObject.name != "shooting enemy (Clone))")
        {
            switch (movDirection)
            {
                case GolemStates.up:
                    anim.ChangeAnimationState(attack_up);
                    isAttacking = true;
                    break;
                case GolemStates.down:
                    anim.ChangeAnimationState(attack_down);
                    isAttacking = true;

                    break;
                case GolemStates.left:
                    anim.ChangeAnimationState(attack_left);
                    isAttacking = true;

                    break;
                case GolemStates.right:
                    anim.ChangeAnimationState(attack_right);
                    isAttacking = true;

                    break;
            }


        }
        else
        {
            isAttacking = false;

        }
    }
}


  

