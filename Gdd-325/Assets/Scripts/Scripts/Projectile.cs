using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("enemy"))
            {
                Debug.Log("Enemy Take Damage");
            }
            DestroyProjectile();
        }

        transform.Translate(transform.up * speed * Time.deltaTime);
    }
    private void DestroyProjectile()
    {
        //Instantiate(destroySpell,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

        /*    old projectile class
         *    public float speed;
            public float lifeTime;
            public float distance;
            //public List<Golem> golemList;
            public Golem golem;
            public LayerMask whatIsSolid;

            //public GameObject destroySpell;

            private void Start()
            {
                //Invoke("DestroyProjectile", lifeTime);
            }
            private void Update()
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("enemy"))
                    {
                        Debug.Log("Enemy Take Damage");
                        this.golem.TakeDamage();
                    }
                    DestroyProjectile();
                }
                transform.Translate(transform.up * speed * Time.deltaTime);
            }

            private void DestroyProjectile()
            {
                //Instantiate(destroySpell,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }*/
    }
