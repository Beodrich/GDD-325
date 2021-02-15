using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Sel;
    [SerializeField] private Vector2 Min;
    [SerializeField] private Vector2 Max;

    void LateUpdate()
    {
        transform.position = new Vector3
        (
            Mathf.Clamp(Sel.transform.position.x, Min.x, Max.x),
            Mathf.Clamp(Sel.transform.position.y, Min.y, Max.y),
            transform.position.z
        );
    }
}
