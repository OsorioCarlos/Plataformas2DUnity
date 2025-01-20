using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDeathState : State<Fly>
{
    private Animator animator;

    public override void OnEnterState(Fly controller)
    {
        base.OnEnterState(controller);
        SetAnimator();
        animator.SetTrigger("isDead");
    }

    public override void OnUpdateState()
    {

    }

    public override void OnFixedUpdateState()
    {

    }

    public override void OnExitState()
    {

    }

    private void SetAnimator()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
}
