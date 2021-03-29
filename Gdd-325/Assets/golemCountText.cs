using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class golemCountText : MonoBehaviour
{
    [SerializeField] private Text numOfGolemText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numOfGolemText.text = GameObject.FindGameObjectsWithTag("enemy").Length + " Golems";

    }
}
