using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.player.AnimationData.ComboAttackParameterHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.player.Data.AttackData.GetAttackInfo(comboIndex);
        stateMachine.player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.player.AnimationData.ComboAttackParameterHash);

        if (!alreadyApplyCombo)
            stateMachine.ComboIndex = 0;
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.player.ForceReceiver.Reset();

        stateMachine.player.ForceReceiver.AddForce(stateMachine.player.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ForceTransitionTime)   
                TryApplyForce();

            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }
        else
        {
            if (alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.player.weapon.SetAttack(attackInfoData.Damage, attackInfoData.Force);
                stateMachine.player.weapon.gameObject.SetActive(true);
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.player.weapon.gameObject.SetActive(false);
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}