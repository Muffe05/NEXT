using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _timeUnitlIdle;
    [SerializeField] private int _numberOfIdle;
    [SerializeField] private bool _isIdle;
    [SerializeField] private float _idleTime;
    private int _idleAnimator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_isIdle == false){
            _idleTime += Time.deltaTime;

            if(_idleTime > _timeUnitlIdle && stateInfo.normalizedTime % 1 < 0.02f){
                _isIdle = true;
                _idleAnimator = Random.Range(1, _numberOfIdle + 1);
                _idleAnimator = _idleAnimator * 2 - 1;
                
                animator.SetFloat("IdleAnimator", _idleAnimator - 1);
            }
        }
        else if(stateInfo.normalizedTime % 1 > 0.98){
            ResetIdle();
        }

        animator.SetFloat("IdleAnimator", _idleAnimator, 0.2f, Time.deltaTime);
    }

    private void ResetIdle(){
        if(_isIdle){
            _idleAnimator--;
        }
        _isIdle = false;
        _idleTime = 0;
    }
}
