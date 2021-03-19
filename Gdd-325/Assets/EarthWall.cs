using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    private bool isHit;
    private bool isNotHit = true;
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
        /*  RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance);
          if (hit.collider != null)
          {
              if (hit.collider.CompareTag("enemy") || hit.collider.gameObject.name=="Golem(Clone)")
              {
                  Debug.Log("hit");
                  Debug.Log("isNotHit " + isNotHit);
                  if (isNotHit)
                  {
                      Debug.Log("Taking Damage");
                      hit.collider.GetComponent<Golem>().TakeDamage();
                      isHit = true;
                      isNotHit = false;
                  }
              }

              if (hit.collider.CompareTag("wall"))
              {
                  DestroySpell();
              }
              //DestroySpell(hit.collider.GetComponent<Golem>().getPosition());
              //DestroySpell();
          }*/
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("hit");
            Debug.Log("isNotHit " + isNotHit);
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
