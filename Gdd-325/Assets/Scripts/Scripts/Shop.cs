using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static bool hasBoughItem=false;
    private GameObject shopUI;
    private HeathManaBar bar;
    private PlayerController player;
    
    private void Awake()
    {
        shopUI = GameObject.Find("ShopItemTemplete");
        shopUI.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        bar = GameObject.Find("Canvas").GetComponent<HeathManaBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyMana() { 
    
    }
    //restore health and increase it a factor 
    public void buyHealth() {
        bar.setMaxHealth(5);
        player.health = bar.getMaxHealth();
        HeathManaBar.restoreLife(bar.getMaxHealth());
        //Debug.Log("player hp is " + player.health + "current life is " + HeathManaBar.currentLife + "max health is " + HeathManaBar.maxHeath);

    }
}
