using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWand : MonoBehaviour
{
    public GameObject Firewand;
    public GameObject Fire;
    public GameObject Icewand;
    public GameObject Ice;
    public GameObject Windwand;
    public GameObject Wind;
    public GameObject Earthwand;
    public GameObject Earth;

    // UI
    public GameObject FireUI; 
    public GameObject IceUI;
    public GameObject WindUI;
    public GameObject EarthUI;


    private PlayerController player;

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
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Fire)
        {
            // Sets GameObjects
            Firewand.SetActive(true);
            Fire.SetActive(false);
            Ice.SetActive(true);
            Icewand.SetActive(false);
            Windwand.SetActive(false);
            Wind.SetActive(true);
            Earthwand.SetActive(false);
            Earth.SetActive(true);
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
        }
        else if (col.gameObject == Ice)
        {
            // Sets GameObjects
            Ice.SetActive(false);
            Icewand.SetActive(true);
            Fire.SetActive(true);
            Firewand.SetActive(false);
            Windwand.SetActive(false);
            Wind.SetActive(true);
            Earthwand.SetActive(false);
            Earth.SetActive(true);
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
        }
        else if (col.gameObject == Wind)
        {
            // Sets GameObjects
            Ice.SetActive(true);
            Icewand.SetActive(false);
            Fire.SetActive(true);
            Firewand.SetActive(false);
            Windwand.SetActive(true);
            Wind.SetActive(false);
            Earthwand.SetActive(false);
            Earth.SetActive(true);
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
        }
        else if (col.gameObject == Earth)
        {
            // Sets GameObjects
            Ice.SetActive(true);
            Icewand.SetActive(false);
            Fire.SetActive(true);
            Firewand.SetActive(false);
            Windwand.SetActive(false);
            Wind.SetActive(true);
            Earthwand.SetActive(true);
            Earth.SetActive(false);
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
        }
    }
}
