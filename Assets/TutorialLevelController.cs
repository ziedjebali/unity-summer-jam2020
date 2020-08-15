using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialLevelController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
