using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject1PortalTriggerScript : MonoBehaviour
{
    public GameObject teleportTargetSubject1;


    void OnTriggerEnter(Collider other)
    {



        if (other.tag == "Player")
        {




            other.transform.position = teleportTargetSubject1.transform.position;



        }
    }
}
