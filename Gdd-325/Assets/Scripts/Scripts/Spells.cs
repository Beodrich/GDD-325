using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public enum SpellState { 
     Fire, Air, Earth,Ice,None
    
    };
    //public GameObject fireball;
    public Rigidbody2D fireball;
    //public Transform wand;
    //public float fireSpeed;
    //public float fireRate;
    public float fireballSpeed = 8f;
    public Transform Player;
    private Vector2 lastDirection;
    //public float fireDamage = 5f;
    public Transform shotPoint;
    //public float iceDamage;
    //public float earthDamage;
    //public float airDamage;
    //public float baseDamage;


    public SpellState spell = SpellState.Fire;

    private Golem golem;
   public void SetSpellState(SpellState state) {

        this.spell = state;
    }
    public SpellState getSpellState() {
        return this.spell;
    
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CastSpell(Vector3 movement)
    {

        transform.rotation = Player.transform.rotation;
        var fireballInst = Instantiate(fireball, transform.position, Quaternion.identity);
        //var fireballInst = Instantiate(fireball, shotPoint.position, Quaternion.identity);
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
            fireballInst.velocity = lastDirection * fireballSpeed;
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
