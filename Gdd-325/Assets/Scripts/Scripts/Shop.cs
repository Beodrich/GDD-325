using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Shop : MonoBehaviour
{
    public static bool hasBoughItem = false;
    private GameObject shopUI;
    private HeathManaBar bar;
    private PlayerController player;
    public TextMeshProUGUI healthToolTipText;
    public TextMeshProUGUI manaToolTipText;

    //utility variables 
    public float amountOfHealthThatIsIncreased = 5f;
    public float amountOfManaThatIsIncreased = 5f;
    public float manaIncreaseRate = 0.01f;
    public float skillPoint = 1f;
    
    private void Awake()
    {
        shopUI = GameObject.Find("ShopSystem");
        shopUI.SetActive(false);
        //healthToolTipText = GameObject.Find("healthTip").GetComponent<Text>();
       // manaToolTipText = GameObject.Find("manaTip").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {


        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        bar = GameObject.Find("Canvas").GetComponent<HeathManaBar>();
        
        healthToolTipText.enabled = false;
        manaToolTipText.enabled = false;

    }
    private void Update()
    {
        if (hasBoughItem)
        {
            shopUI.SetActive(false);

        }
        if (manaToolTip.isOverMana)
        {
             manaToolTipText.text = "This will restore the amount of mana you have and increase it by " + amountOfManaThatIsIncreased + " points. You will also increase your basic recharge rate by " + manaIncreaseRate;
            //manaToolTipText.text = "This will increase the amount of mana you have to " + amountOfManaThatIsIncreased + HeathManaBar.getMana() + "You're basic mana recharge rate will incrase by " + manaIncreaseRate;

            manaToolTipText.enabled = true;

        }
        else {
            manaToolTipText.enabled = false;
        }
        if (healthToolTip.isOverHealth)
        {
            healthToolTipText.text = "This will restore your hp back to full and also increase it by " + amountOfHealthThatIsIncreased + " points";
            healthToolTipText.enabled = true;

        }
        else
        {
            healthToolTipText.enabled = false;
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
