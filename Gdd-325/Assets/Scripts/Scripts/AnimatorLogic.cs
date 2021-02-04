using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorLogic : MonoBehaviour
{
    [SerializeField] private string currentState;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimationState(string newState) {
        //if it is already playing the animation just return 
        if (currentState == newState)
        {
            return;
        }
        else {
            //play the new state
            animator.Play(newState);
            //set the currentstate to the new state
            currentState = newState;
        }
    }
}
