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

    private PlayerController player;

    private void Start()
    {
        player = GameObject.Find("MonkE").GetComponent<PlayerController>();
        Fire.SetActive(true);
        Ice.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Fire)
        {
            Firewand.SetActive(true);
            Fire.SetActive(false);
            Ice.SetActive(true);
            Icewand.SetActive(false);
            Windwand.SetActive(false);
            Wind.SetActive(true);
            Earthwand.SetActive(false);
            Earth.SetActive(true);
            player.setFire(false);
            player.setIce(true);
            player.setWind(false);
            player.setEarth(false);
        }
        if (col.gameObject == Ice)
        {
            Ice.SetActive(false);
            Icewand.SetActive(true);
            Fire.SetActive(true);
            Firewand.SetActive(false);
            Windwand.SetActive(false);
            Wind.SetActive(true);
            Earthwand.SetActive(false);
            Earth.SetActive(true);
            player.setFire(false);
            player.setIce(true);
            player.setWind(false);
            player.setEarth(false);
        }
        if(col.gameObject == Wind)
        {
            Ice.SetActive(true);
            Icewand.SetActive(false);
            Fire.SetActive(true);
            Firewand.SetActive(false);
            Windwand.SetActive(true);
            Wind.SetActive(false);
            Earthwand.SetActive(false);
            Earth.SetActive(true);
            player.setFire(false);
            player.setIce(false);
            player.setWind(true);
            player.setEarth(false);
        }
        if(col.gameObject == Earth)
        {
            Ice.SetActive(true);
            Icewand.SetActive(false);
            Fire.SetActive(true);
            Firewand.SetActive(false);
            Windwand.SetActive(false);
            Wind.SetActive(true);
            Earthwand.SetActive(true);
            Earth.SetActive(false);
            player.setFire(false);
            player.setIce(false);
            player.setWind(false);
            player.setEarth(true);
        }
    }
}
