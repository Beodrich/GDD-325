using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour
{
    public float lifeTime;
    public LayerMask whatIsSolid;
    private bool isHit;
    private bool isNotHit = true;
    private Animator animator;
    Golem[] enemies;


    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroySpell", lifeTime);
        isHit = false;
        isNotHit = true;
        enemies = FindObjectsOfType<Golem>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("enemy"))
        if (other.collider.gameObject.CompareTag("enemy"))
        {
            if (isNotHit)
            {
                Debug.Log("Taking Damage");
                other.gameObject.GetComponent<Golem>().TakeDamage();
                isNotHit = false;

            }
        }
        if (other.gameObject.CompareTag("wall"))
        {
            DestroySpell();
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            foreach (Golem enemy in enemies)
            {
                //Your damage code
                col.gameObject.GetComponent<Golem>().TakeDamage();
            }
        }
    }
    */


    void DestroySpell()
    {
        Destroy(gameObject, 0.55f);
        //animator.SetBool("Explosion", true);
    }
  
}
