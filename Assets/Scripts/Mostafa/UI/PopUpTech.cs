using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopUpTech : MonoBehaviour
{
    [SerializeField] private TextMeshPro UseText;
    [SerializeField] private Transform Camera;
    [SerializeField] private float MaxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayers;
    [SerializeField] private GameObject popUpUI;

    public void OnEClick(){
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)){
           Cursor.visible = true;
           Cursor.lockState = CursorLockMode.None;
           popUpUI.SetActive(!popUpUI.activeSelf);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)){
            UseText.SetText("Chat \"F\"");
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }else{
            UseText.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.F)){
            OnEClick();
        }
    }
}
