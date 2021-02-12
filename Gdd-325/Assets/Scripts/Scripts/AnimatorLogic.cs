using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]

public class AnimatorLogic : MonoBehaviour
{
     private string currentState;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //function to change the animation state
    //@param takes a string of the animation name
    public void ChangeAnimationState(string newState) {
        //if it is already playing the animation just return 
        if (currentState == newState)
        {
            return;
        }
        
            //play the new state
            animator.Play(newState);
            //set the currentstate to the new state
            currentState = newState;
            //Debug.Log(currentState);
        
    }
}
