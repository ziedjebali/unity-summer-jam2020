using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    
    public float timer, activeTime;
    public bool active = false;
    public Material changedMat;

    Material defaultMat;

    // Start is called before the first frame update
    void Start()
    {
        defaultMat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                active = false;
                timer = activeTime;

                gameObject.GetComponent<Renderer>().material = defaultMat;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Clone")
        {
            active = true;
            timer = activeTime;

            gameObject.GetComponent<Renderer>().material = changedMat;
        }
    }
}
