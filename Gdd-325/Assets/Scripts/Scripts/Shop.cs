using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static bool hasBoughItem = false;
    private GameObject shopUI;
    private HeathManaBar bar;
    private PlayerController player;


    //utility variables 
    public float amountOfHealthThatIsIncreased = 5f;
    public float amountOfManaThatIsIncreased = 5f;
    public float manaIncreaseRate = 0.01f;
    public float skillPoint = 1f;
    private void Awake()
    {
        shopUI = GameObject.Find("ShopSystem");
        shopUI.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {


        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        bar = GameObject.Find("Canvas").GetComponent<HeathManaBar>();


    }
    private void Update()
    {
        if (hasBoughItem) {
            shopUI.SetActive(false);
        
        }
    }




    //restore health and increase it a factor 
    public void buyHealth() {
        if (skillPoint == 1)
        {
            bar.setMaxHealth(amountOfHealthThatIsIncreased);
            player.health = bar.getMaxHealth();
            HeathManaBar.restoreLife(bar.getMaxHealth());
            //Debug.Log("player hp is " + player.health + "current life is " + HeathManaBar.currentLife + "max health is " + HeathManaBar.maxHeath);
            skillPoint = 0;
            hasBoughItem = true;
        }

    }
    //restore mana, increase by a factor, and increase the mana by a certain factor
    public void buyMana() {
        if (skillPoint == 1)
        {
            bar.setMaxMana(amountOfManaThatIsIncreased);
            bar.setCurrentMana(amountOfManaThatIsIncreased);
            WeaponWand.Mana = HeathManaBar.getMana();
            bar.addManaRechargeRate(manaIncreaseRate);
            skillPoint = 0;
            hasBoughItem = true;
        }


    }
    public void activateShopUI() {
        shopUI.SetActive(true);
    
    }
}
