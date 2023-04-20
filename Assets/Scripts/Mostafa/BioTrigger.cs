using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioTrigger : MonoBehaviour
{
   public AudioSource audioSource;

    void OnTriggerEnter(Collider other)

    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();

        }
        else
        {
            audioSource.Stop();
        }
    }
}
