using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class openDoor : MonoBehaviour
{
    [SerializeField] private TextMeshPro UseText;
    [SerializeField] private Transform Camera;
    [SerializeField] private float MaxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayers;

    public void OnEClick(){
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)){
            if(hit.collider.TryGetComponent<Door>(out Door door)){
                if (door.IsOpen){
                    door.Close();
                } else {
                    door.Open(transform.position);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers) && hit.collider.TryGetComponent<Door>(out Door door)){
            if(door.IsOpen){
                UseText.SetText("Close \"E\"");
            } else {
                UseText.SetText("Open \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }else{
            UseText.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.E)){
            OnEClick();
        }
    }
}
