using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWand : MonoBehaviour
{
    public float offset;
    [SerializeField] AudioSource spellShoot;
    public GameObject projectile;
    public Transform shotPoint;
    public static bool isShoot = false;
    private float timeBTWShots;
    public float startTimeBTWShots = 0.25f;
    public static float Mana = 20f;
    private PlayerController player;
    // public Rigidbody2D Wind;
    public Vector3 difference;
    private void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
    }

    void Update()
    {
        //Debug.Log(isShoot);
        if (!PauseMenu.GameIsPaused)
        {
             difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }
            
        shootSpells();
        
    }

    public void shootSpells()
    {
        if (timeBTWShots <= 0)
        {
            if (Input.GetMouseButtonDown(0) && Mana > 0.5 && !SpawnLogic.inBetweenRounds)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                Mana -= 1;
                HeathManaBar.reduceMana(1);
                timeBTWShots = startTimeBTWShots;
                isShoot = true;
                StartCoroutine(WaitForAttackAnimation());
                spellShoot.Play();

            }
        }
        else
        {
            timeBTWShots -= Time.deltaTime;
        }
        Mana = HeathManaBar.currentMana;
    }
    IEnumerator WaitForAttackAnimation() {
        yield return new WaitForSeconds(1.5f);
        isShoot = false;
    
    }
    public void updateMana(float amount) {
        Mana -= amount;
    }
}
