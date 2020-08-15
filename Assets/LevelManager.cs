using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Camera cam1, cam2, cam3;
    public Rect cam1Rect, cam2Rect, cam3Rect;

    public GameObject Level1, Level2, Level3;
    public AudioSource SwitchSound;



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


    public void ShowNextSection()
    {
        SwitchSound.Play();
        Level2.SetActive(true);
    }

}
