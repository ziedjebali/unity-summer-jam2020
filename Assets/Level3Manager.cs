using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public float timeBetweenDropping;
    public float timer;
    public bool started;
    public GameObject[] blocks;
    public int blockIndex;
    // Start is called before the first frame update
    void Start()
    {
        timer = timeBetweenDropping;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (started)
        {
            if(timer <= 0)
            {
                blocks[blockIndex].GetComponent<Rigidbody>().isKinematic = false;
                blocks[blockIndex].GetComponent<Rigidbody>().useGravity = true;
                timer = timeBetweenDropping;
                blockIndex++;
            }
        }
    }
}
