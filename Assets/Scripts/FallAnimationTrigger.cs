using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAnimationTrigger : MonoBehaviour
{
    public Animator LevelAnimator;
    public string AnimationName;
    bool Played = false;
    public int layer;
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
        if(other.tag == "Player") {
            if (!Played)
            {
                LevelAnimator.Play(AnimationName, layer);
                Played = true;
            }
        }

    }
}
