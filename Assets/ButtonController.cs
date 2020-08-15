using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public GameObject Button1, Button2, bridge;

    
    // Update is called once per frame
    void Update()
    {
        if(Button1.GetComponent<ButtonScript>().active && Button2.GetComponent<ButtonScript>().active)
        {
            Debug.Log("Test");
            bridge.SetActive(true);
        }
    }

    public void Success()
    {
        Debug.Log("SUCCESS");
        bridge.SetActive(true);
    }
}
