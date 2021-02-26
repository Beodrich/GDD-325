using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWand : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    private float timeBTWShots;
    public float startTimeBTWShots = 0.25f;

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if(timeBTWShots <=0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBTWShots = startTimeBTWShots;
            }
        }
        else
        {
            timeBTWShots -= Time.deltaTime;
        }

       
    }
}
