using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeCam : MonoBehaviour
{
    public Vector3 cameraNorth, cameraSouth;
    public bool camIsSouth = true;
    public float duration;
    public GameObject player, cam;
    public AudioSource goNorthChime, goSouthChime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (camIsSouth)
            {
                goNorthChime.Play();
                player.GetComponent<PlayerMovement>().MovementEnabled = false;
                player.GetComponent<Rigidbody>().Sleep();
                cam.transform.DOMove(cameraNorth, duration).OnComplete(() => player.GetComponent<PlayerMovement>().ToggleMovement());
                camIsSouth = false;
                player.GetComponent<Rigidbody>().WakeUp();
            }
            else
            {
                goSouthChime.Play();
                player.GetComponent<PlayerMovement>().MovementEnabled = false;
                player.GetComponent<Rigidbody>().Sleep();
                cam.transform.DOMove(cameraSouth, duration).OnComplete(() => player.GetComponent<PlayerMovement>().ToggleMovement());
                camIsSouth = true;
                player.GetComponent<Rigidbody>().WakeUp();
            }

        }
    }
}
