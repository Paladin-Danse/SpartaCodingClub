using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.player.AnimationData.AttackParameterHash);
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.player.AnimationData.AttackParameterHash);
    }

}
