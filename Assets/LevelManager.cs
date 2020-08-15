using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Camera cam1, cam2, cam3;
    public Rect cam1Rect, cam2Rect, cam3Rect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            
        }
    }

    void SplitScreen()
    {
        cam1.rect = new Rect(0f, .5f, 1f, .5f);
    }

}
