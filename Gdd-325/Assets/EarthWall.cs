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
    private List<Golem> enemies;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroySpell", lifeTime);
        isHit = false;
        isNotHit = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            for (int i = 0; i < 100; i++)
            {
                // add the enemies that hit the collider to a list them have that
                // list take damage 
            }
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


    void DestroySpell()
    {
        Destroy(gameObject, 0.55f);
        //animator.SetBool("Explosion", true);
    }
  
}
