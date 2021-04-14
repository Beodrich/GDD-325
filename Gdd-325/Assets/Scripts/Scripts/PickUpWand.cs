using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWand : MonoBehaviour
{
    // Wands in Hand
    public GameObject Firewand;
    public GameObject Icewand;
    public GameObject Windwand;
    public GameObject Earthwand;

    // Pick Up Wands
    public GameObject Fire;
    public GameObject Ice;
    public GameObject Wind;
    public GameObject Earth;

    // UI
    public GameObject FireUI; 
    public GameObject IceUI;
    public GameObject WindUI;
    public GameObject EarthUI;

    private bool hasFinished = false;
    private PlayerController player;

    /// <summary>
    /// Setting the UI wand to false, all ground wands to true so they appear
    /// </summary>
    private void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        Fire.SetActive(true);
        Ice.SetActive(true);
        Earth.SetActive(true);
        Wind.SetActive(true);
        FireUI.SetActive(false);
        IceUI.SetActive(false);
        WindUI.SetActive(false);
        EarthUI.SetActive(false);
        hasFinished = false;
    }

    /// <summary>
    /// Depending on what boolean is triggered, set that boolean to true and set wands active to pick up 
    /// </summary>
    private void Update()
    {
        if (hasFinished && player.isFire())
        {
            Fire.SetActive(false);
            Ice.SetActive(true);
            Wind.SetActive(true);
            Earth.SetActive(true);
            hasFinished = false;
        }
        else if (hasFinished && player.isIce())
        {
            Fire.SetActive(true);
            Ice.SetActive(false);
            Wind.SetActive(true);
            Earth.SetActive(true);
            hasFinished = false;
        }
        else if (hasFinished && player.isWind())
        {
            Fire.SetActive(true);
            Ice.SetActive(true);
            Wind.SetActive(false);
            Earth.SetActive(true);
            hasFinished = false;
        }
        if (hasFinished && player.isEarth())
        {
            Fire.SetActive(true);
            Ice.SetActive(true);
            Wind.SetActive(true);
            Earth.SetActive(false);
            hasFinished = false;
        }
    }

    /// <summary>
    /// Makes the wands in game diasappear for 8 seconds before player can pick up another
    /// </summary>
    IEnumerator afterPickup()
    {
        Fire.SetActive(false);
        Wind.SetActive(false);
        Ice.SetActive(false);
        Earth.SetActive(false);
        yield return new WaitForSeconds(8f);
        hasFinished = true;
    }

    /// <summary>
    /// sets the wand to true depending on what boolean is set, and sets UI plus starts 
    /// Coroutine to make wands in game disappear
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Fire)
        {

            // Sets GameObjects
            Firewand.SetActive(true);
            Icewand.SetActive(false);
            Windwand.SetActive(false);
            Earthwand.SetActive(false);
            // Sets Player Bools
            player.setFire(true);
            player.setIce(false);
            player.setWind(false);
            player.setEarth(false);
            // Sets UI Wands
            FireUI.SetActive(true);
            IceUI.SetActive(false);
            WindUI.SetActive(false);
            EarthUI.SetActive(false);
            // Disappearing Wands
            StartCoroutine(afterPickup());
            
        }
        else if (col.gameObject == Ice)
        {
            // Sets GameObjects
            Icewand.SetActive(true);
            Firewand.SetActive(false);
            Windwand.SetActive(false);
            Earthwand.SetActive(false);
            // Sets Player Bools
            player.setFire(false);
            player.setIce(true);
            player.setWind(false);
            player.setEarth(false);
            // Sets UI Wands
            FireUI.SetActive(false);
            IceUI.SetActive(true);
            WindUI.SetActive(false);
            EarthUI.SetActive(false);
            // Disappearing Wands
            StartCoroutine(afterPickup());

        }
        else if (col.gameObject == Wind)
        {
            // Sets GameObjects
            Icewand.SetActive(false);
            Firewand.SetActive(false);
            Windwand.SetActive(true);
            Earthwand.SetActive(false);
            // Sets Player Bools
            player.setFire(false);
            player.setIce(false);
            player.setWind(true);
            player.setEarth(false);
            // Sets UI Wands
            FireUI.SetActive(false);
            IceUI.SetActive(false);
            WindUI.SetActive(true);
            EarthUI.SetActive(false);
            // Disappearing Wands
            StartCoroutine(afterPickup());
            
        }
        else if (col.gameObject == Earth)
        {
            // Sets GameObjects
            Icewand.SetActive(false);
            Firewand.SetActive(false);
            Windwand.SetActive(false);
            Earthwand.SetActive(true);
            // Sets Player Bools
            player.setFire(false);
            player.setIce(false);
            player.setWind(false);
            player.setEarth(true);
            // Sets UI Wands
            FireUI.SetActive(false);
            IceUI.SetActive(false);
            WindUI.SetActive(false);
            EarthUI.SetActive(true);
            // Disappearing Wands
            StartCoroutine(afterPickup());
            
        }
        
    }
}
