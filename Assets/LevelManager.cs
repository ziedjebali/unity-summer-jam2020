using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Camera cam1, cam2, cam3;
    public Rect cam1Rect, cam2Rect, cam3Rect;

    public GameObject Level1, Level2, Level3;
    public AudioSource SwitchSound;


    public GameObject Level1Player, Level2Player, Level3Player;

    public int levelCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        Level1Player.GetComponent<PlayerMovement>().MovementEnabled = true;
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
        if(levelCount == 1)
        {
            Level1Player.GetComponent<PlayerMovement>().MovementEnabled = false;
            Level1Player.GetComponent<Rigidbody>().isKinematic = true;
            SwitchSound.Play();
            Level2.SetActive(true);
            Level2Player.GetComponent<PlayerMovement>().MovementEnabled = true;
            levelCount++;
        }else if(levelCount == 2)
        {
            Level2Player.GetComponent<PlayerMovement>().MovementEnabled = false;
            SwitchSound.Play();
            Level3.SetActive(true);
            Level3Player.GetComponent<PlayerMovement>().MovementEnabled = true;
            levelCount++;
        }
        
    }

}
