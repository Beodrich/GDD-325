using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroySpell", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance);
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("enemy"))
            {
                Debug.Log("Taking Damage");
            }
            DestroySpell();
        }


        transform.Translate(Vector2.up* speed * Time.deltaTime);
    }

    void DestroySpell()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
