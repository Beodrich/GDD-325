using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWeapon : MonoBehaviour
{
    public float offset;
    //[SerializeField] AudioSource spellShoot;
    public Transform shotPoint;
    public static bool isShoot = false;
    private float timeBTWShots;
    public float startTimeBTWShots = 0.25f;
    public static float Mana = 20f;
    private PlayerController player;
    public Rigidbody2D Wind;

    private void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
    }

    void Update()
    {
        //Debug.Log(isShoot);
        if (!PauseMenu.GameIsPaused)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }


        if (player.isWind())
        {
            WindSpell();
        }
    }

    public void WindSpell()
    {
        if (timeBTWShots <= 0)
        {
            if (Input.GetMouseButtonDown(0) && Mana > 0.5)
            {
                print("Its Windy");
                Instantiate(Wind, shotPoint.position, transform.rotation);
                Wind.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                Mana -= 1;
                HeathManaBar.reduceMana(1);
                timeBTWShots = startTimeBTWShots;
                isShoot = true;
                StartCoroutine(WaitForAttackAnimation());
                // spellShoot.Play();

            }
        }
        else
        {
            timeBTWShots -= Time.deltaTime;
        }
        Mana = HeathManaBar.currentMana;
    }

    IEnumerator WaitForAttackAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        isShoot = false;

    }
    public void updateMana(float amount)
    {
        Mana -= amount;
    }
}
