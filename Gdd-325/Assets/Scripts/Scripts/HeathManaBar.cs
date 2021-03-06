using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeathManaBar : MonoBehaviour
{
    private PlayerController player;
    public Image lifeBar;
    public Image manaBar;
    public Text lifeText;
    public Text manaText;

    private float mana;//change were we get this variable 
    private float maxHeath = 10;

    private static float currentLife;
    private float currentMana;
    private float calculateLife;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        currentLife = player.health;
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
        manaText.text = currentMana.ToString();
    }
    public static void Damage(float damage)
    {

        currentLife -= damage;

    }
}
