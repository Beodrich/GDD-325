using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private float h = 0;
    private float v = 0;
    public int movementSpeed;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        MovePlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if (h != 0 || v != 0)
            RotatePlayer();
    }

    

    private void MovePlayer()
    {
        Vector3 directionVector = new Vector3(h, v, 0);
        rb2D.velocity = directionVector.normalized * movementSpeed;
    }

    private void RotatePlayer()
    {
        //Atan2 - Return value is the angle between the x-axis and a 2D vector starting at zero and terminating at (x,y).
        float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}