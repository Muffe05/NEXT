using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class afspilVideo : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            videoPlayer.Play();
            
        }
    }
}
