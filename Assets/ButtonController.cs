using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public GameObject Button1, Button2, Button3, bridge, bridge2;

    public Animator LevelAnimator;
    // Update is called once per frame
    void Update()
    {
        if(Button1.GetComponent<ButtonScript>().active && Button2.GetComponent<ButtonScript>().active)
        {
            Debug.Log("ButtonsPressed");
            bridge.SetActive(true);
            LevelAnimator.Play("DropBridge1", 4);
         
        }

        if (Button3.GetComponent<ButtonScript>().active)
        {
            bridge2.SetActive(true);
            LevelAnimator.Play("DropBridge2", 4);
        }
    }

    public void Success()
    {
        Debug.Log("SUCCESS");
        bridge.SetActive(true);
    }
}
