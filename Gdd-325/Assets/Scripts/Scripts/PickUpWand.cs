using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWand : MonoBehaviour
{
    public GameObject Firewand;
    public GameObject Fire;
    public GameObject Icewand;
    public GameObject Ice;
    //public GameObject Windwand;
    //public GameObject Earthwand;

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
            player.setFire(true);
            player.setIce(false);
        }
        if (col.gameObject == Ice)
        {
            Ice.SetActive(false);
            Icewand.SetActive(true);
            Fire.SetActive(true);
            Firewand.SetActive(false);
            player.setFire(false);
            player.setIce(true);
        }
        // same for wind and earth wand
    }
}
