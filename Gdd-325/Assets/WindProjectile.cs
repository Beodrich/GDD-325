using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    private bool isHit;
    private bool isNotHit=true;
    private bool applyWind = false;
    private Animator animator;
    private WindWeapon weapon;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroySpell", lifeTime);
        isHit = false;
        isNotHit = true;
        weapon = GameObject.Find("AirWand").GetComponent<WindWeapon>();
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
        if (!isHit)
        {
            //transform.Translate(Vector2.up * speed * Time.deltaTime);

        }
        if (applyWind) {
            StartCoroutine(WindTime());
        
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {

            Debug.Log("Taking Damage");
            other.gameObject.GetComponent<Golem>().TakeDamage();
            isHit = true;
            isNotHit = false;
                
        }
        if (other.gameObject.CompareTag("wall"))
        {
            DestroySpell();
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // IDEA FROM TETZLAFF
        // push pack for golem using on trigger and once the wind hits the golem,
        // use normalized vector dif to push golem back
    }

    void DestroySpell()
    {
        Destroy(gameObject, 0.55f);
        //animator.SetBool("Explosion", true);
    }
    IEnumerator WindTime() {
       weapon.getWind().AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.5f);
        applyWind = false;
    
    }
}
