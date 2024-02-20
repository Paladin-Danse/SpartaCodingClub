using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool alreadyAppliedForce;
    private bool alreadyAppliedDealing;
    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;
        
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float NormalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack");
        if(NormalizedTime < 1f)
        {
            if (NormalizedTime >= stateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();

            if (!alreadyAppliedDealing && NormalizedTime >= stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                stateMachine.Enemy.weapon.SetAttack(stateMachine.Enemy.Data.Damage, stateMachine.Enemy.Data.Force);
                stateMachine.Enemy.weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }

            if (alreadyAppliedDealing && NormalizedTime >= stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                stateMachine.Enemy.weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if(IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }
        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Enemy.ForceReceiver.Reset();

        stateMachine.Enemy.ForceReceiver.AddForce(stateMachine.Enemy.transform.forward * stateMachine.Enemy.Data.Force);
    }
}
