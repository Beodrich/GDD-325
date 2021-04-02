using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthToolTip : MonoBehaviour
{
    public static bool isOverHealth = false;

    // Start is called before the first frame update
    public void setIsOverHealth(bool isOverHealthButton) {

        isOverHealth = isOverHealthButton;
        //Debug.Log(isOverHealth);
    }
}
