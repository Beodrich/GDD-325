using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public GameObject fireball;
    //public Transform wand;
    //public float fireSpeed;
    //public float fireRate;
    public float fireballSpeed = 8f;
    public Transform Player;
    private Vector2 lastDirection;
    public float fireDamage = 5f;
    //public float iceDamage;
    //public float earthDamage;
    //public float airDamage;
    //public float baseDamage;
    public bool fire = true;
    //public bool ice;
    //public bool air;
    //public bool earth;
    //public bool base;

    public Golem golem;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.rotation = Player.transform.rotation;
        if (Input.GetKeyDown("space") && Time.time > fireRate)
        {
            Debug.Log("Hello");
            fireRate = Time.time + fireSpeed;
            var fireballInst = Instantiate(fireball, transform.position, transform.rotation);
            fireballInst.velocity = new Vector2(fireballSpeed, 0);
            //fireballInst.velocity = transform.forward * fireballSpeed;
            //target = target1.transform.position;

        }
        if (cloneProj.transform.position == target)
        {
            Destroy(cloneProj.gameObject);
        }*/
    }

    public void CastSpell(Vector3 movement)
    {

        transform.rotation = Player.transform.rotation;
        Debug.Log(lastDirection);
        var fireballInst = Instantiate(fireball, transform.position, Quaternion.identity);
        //var fireballInst = Instantiate(fireball, wand.position, Quaternion.identity);
        if (movement.x == 0 && movement.y == 0)
        {
            // shoot left
            if (movement.x == -1)
            {
                lastDirection.x = -1;
                lastDirection.y = 0;
            }
            // shoot right
            else if (movement.x == 1)
            {
                lastDirection.x = 1;
                lastDirection.y = 0;
            }
            //shoot up
            else if (movement.y == 1)
            {
                lastDirection.x = 0;
                lastDirection.y = 1;
            }
            //shoot down
            else if (movement.y == -1)
            {
                lastDirection.x = 0;
                lastDirection.y = -1;
            }
            //fireballInst.velocity = lastDirection * fireballSpeed;
        }
        else
        {
            //fireballInst.velocity = movement * fireballSpeed;
        }

        //fireballInst.velocity = new Vector2(fireballSpeed, 0) ;

        //fireballInst.velocity = transform.forward * fireballSpeed;

        // shoot left
        if (movement.x == -1)
        {
            lastDirection.x = -1;
            lastDirection.y = 0;
        }
        // shoot right
        else if (movement.x == 1)
        {
            lastDirection.x = 1;
            lastDirection.y = 0;
        }
        //shoot up
        else if (movement.y == 1)
        {
            lastDirection.x = 0;
            lastDirection.y = 1;
        }
        //shoot down
        else if (movement.y == -1)
        {
            lastDirection.x = 0;
            lastDirection.y = -1;
        }

        /*if (movement.y == 1 && movement.x == -1)
        {
            
        }
        if (movement.y == 1 && movement.x == 1)
        {
           
        }
        if (movement.y == -1 && movement.x == -1)
        {
            
        }
        if (movement.y == -1 && movement.x == 1)
        {
            
        }
        else
        {
            
        }*/
    }

    void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.CompareTag("enemy"))
        {
            //Enemy.gameObject.SendMessage("OnDamage", fireDamage);
            golem.TakeDamage();
        }
    }
}
