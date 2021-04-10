using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private PlayerController player;
    private WeaponWand wand;
    private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack=0.3f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange=1.4f;
    [SerializeField] private LayerMask whatIsEnemies;
    [SerializeField] private int damage;
     private HeathManaBar bar;

    void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        wand = GetComponent<WeaponWand>();
        bar = GameObject.Find("Canvas").GetComponent<HeathManaBar>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //attackPos.position = -player.playerDirection + player.gameObject.transform.position;
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            if (Input.GetKey(KeyCode.Space)) {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    var currentGolem = enemiesToDamage[i];
                    if (currentGolem.name == "Boss")
                    {
                        currentGolem.GetComponent<Boss>().MeleeDamge();
                        bar.setCurrentMana(2f);
                        

                    }
                    else
                    {
                        enemiesToDamage[i].GetComponent<Golem>().TakeMeleeDamage(damage);
                        bar.setCurrentMana(2f);



                    }
                }
            
            
            }
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        
        
        }



    
    
    
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
 
}
