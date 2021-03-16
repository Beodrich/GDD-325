using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeathManaBar : MonoBehaviour
{
    private PlayerController player;
    private WeaponWand wand;
    public Image lifeBar;
    public static Image manaBar;
    public Text lifeText;
    public Text manaText;

    private static float mana;//change were we get this variable 
    private static  float maxHeath;

    private static float currentLife;
    public static float currentMana;
    private float calculateLife;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        //wand = GameObject.Find("FireWand").GetComponent<WeaponWand>();
        manaBar = GameObject.Find("manaImage").GetComponent<Image>();
        currentLife = player.health;
        maxHeath = currentLife;
        mana = WeaponWand.Mana;
        currentMana = mana;

        Debug.Log("player hp is " + player.health + "current life is " + currentLife + "max health is " + maxHeath);
    }

    // Update is called once per frame
    void Update()
    {
        calculateLife = currentLife / maxHeath;
        lifeBar.fillAmount = Mathf.MoveTowards(lifeBar.fillAmount, calculateLife,Time.deltaTime);
        lifeText.text = "" + (int)currentLife;

        if (currentMana < mana) {
            manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, 1f, Time.deltaTime * 0.01f);
            currentMana = Mathf.MoveTowards(currentMana / mana, 1f, Time.deltaTime * 0.01f)*mana;
            
        }
        if (currentMana < 0) {
            currentMana = 0;//reset it to 0
        }
        manaText.text = "" + (int)currentMana;
       // Debug.Log("The amount of actual mana left is " + WeaponWand.Mana + " the current amount of mana(mana bar) is " + currentMana);

    }
    public static void Damage(float damage)
    {

        currentLife -= damage;

    }
    public static void restoreLife(float amount) {
        currentLife += amount;
        if (currentLife > maxHeath) {
            currentLife = maxHeath;
        
        }
    }
    public static void reduceMana(float m) {
        currentMana -= m;
        manaBar.fillAmount -= m / mana;
    }
    public float getMaxHealth() {
        return maxHeath;
    
    }
    public void setMaxHealth(float amount) {
        maxHeath += amount;
    }
}
