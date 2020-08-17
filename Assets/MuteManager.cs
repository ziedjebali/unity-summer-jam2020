using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteManager : MonoBehaviour
{

    public bool toggle;
    public Sprite toggleOn, toggleTwo;
    public Image Sound;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSound()
    {
        toggle = !toggle;

        if (toggle)
        {
            Sound.sprite = toggleOn;
            AudioListener.volume = 1f;
        }


        else
        {
            Sound.sprite = toggleTwo;
            AudioListener.volume = 0f;
        }
            
    }
}
