using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public Rigidbody2D fireball;
    public float fireSpeed;
    public float fireRate;
    public float fireballSpeed = 8f;
    public Transform Player;

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
        Vector2 lastDirection = new Vector2();

        transform.rotation = Player.transform.rotation;
        //Debug.Log("Hello");
        var fireballInst = Instantiate(fireball, transform.position, Quaternion.identity);
        if(movement.x == 0 && movement.y == 0)
        {
            fireballInst.velocity = (lastDirection) * fireballSpeed;
        }
        else
        {
            fireballInst.velocity = movement * fireballSpeed;
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
}
