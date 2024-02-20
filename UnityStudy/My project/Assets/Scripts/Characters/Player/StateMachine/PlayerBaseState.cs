using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.player.Data.GroundData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void Update()
    {
        Move();
    }
    public virtual void PhysicsUpdate()
    {
        
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;

        stateMachine.player.Input.PlayerActions.Jump.started += OnJumpStarted;

        stateMachine.player.Input.PlayerActions.Attack.performed += OnAttackPerformed;
        stateMachine.player.Input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }
    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;

        stateMachine.player.Input.PlayerActions.Jump.started -= OnJumpStarted;

        stateMachine.player.Input.PlayerActions.Attack.performed -= OnAttackPerformed;
        stateMachine.player.Input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }
    protected virtual void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = true;
    }
    protected virtual void OnAttackCanceled(InputAction.CallbackContext obj)
    {
        stateMachine.IsAttacking = false;
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }
    

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }
    private void Rotate(Vector3 movementDirection)
    {
        if(movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.player.transform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        stateMachine.player.Controller.Move(
            ((movementDirection * movementSpeed) +
            stateMachine.player.ForceReceiver.Movement) *
            Time.deltaTime
            );
    }
    protected void ForceMove()
    {
        stateMachine.player.Controller.Move(stateMachine.player.ForceReceiver.Movement * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }
    protected void StartAnimation(int animationHash)
    {
        stateMachine.player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.player.Animator.SetBool(animationHash, false);
    }
    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if(animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}