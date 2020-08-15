using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public GameObject partner, bridge;

    public float activeTimer = 10f;
    public float timer;

    public bool activated;

    bool startTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = activeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            if (partner.GetComponent<ButtonController>().activated)
            {
                Success();
                startTimer = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Clone")
        {
            Debug.Log("Found");
            activated = true;
            startTimer = true;
        }
    }

    public void Success()
    {
        Debug.Log("SUCCESS");
        bridge.SetActive(true);
    }
}
