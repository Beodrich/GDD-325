using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public LayerMask whatIsSolid;
    private bool isHit;
    private bool isNotHit;

    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroySpell", lifeTime);
        isHit = false;
        isNotHit = true;

    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("enemy"))
            {  
                if (isNotHit)
                {
                    animator.SetBool("Explosion", true);
                    hit.collider.GetComponent<Golem>().TakeDamage();
                    isHit = true;
                    isNotHit = false;
                }
            }
            else if (hit.collider.CompareTag("Boss"))
            {
                if (hit.collider.GetComponent<Boss>().isCanDamage())
                {
                    if(isNotHit)
                    {
                        hit.collider.GetComponent<Boss>().TakeDamage();
                        isHit = true;
                        isNotHit = false;
                    }
                }
            }
            else if (hit.collider.CompareTag("wall"))
            {
                DestroySpell();
            }
            //DestroySpell(hit.collider.GetComponent<Golem>().getPosition());
            //DestroySpell();
        }
        if (!isHit)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
        
    void DestroySpell()
    {
        Destroy(gameObject, 0.55f);
        //animator.SetBool("Explosion", true);
    }
}
