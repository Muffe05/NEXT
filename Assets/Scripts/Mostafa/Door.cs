using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] private bool IsRotatingDoor = true;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float forwardDirection = 0;
    [SerializeField] private Transform Hinge;

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private void Awake(){
        StartRotation = Hinge.rotation.eulerAngles;
        Forward = transform.right;
    }

    public void Open(Vector3 UserPosition){
        if(!IsOpen){
            if(AnimationCoroutine != null){
                StopCoroutine(AnimationCoroutine);
            }
            if(IsRotatingDoor){
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationIsOpen(dot));
            }
        }
    }

    private IEnumerator DoRotationIsOpen(float ForwardAmount){
        Quaternion startRotation = Hinge.rotation;
        Quaternion endRotation;

        if(ForwardAmount >= forwardDirection){
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.x - RotationAmount, 0));
        } else {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.x + RotationAmount, 0));
        }

        IsOpen = true;
        float time = 0;
        while (time < 1){
            Hinge.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close(){
        if(IsOpen){
            if(AnimationCoroutine != null){
                StopCoroutine(AnimationCoroutine);
            }
            if(IsRotatingDoor){
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationClose(){
        Quaternion startRotation = Hinge.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;
        float time = 0;
        while(time < 1){
            Hinge.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}
