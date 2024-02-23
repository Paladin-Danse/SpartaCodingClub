using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Player : MonoBehaviour
{
    public PlayerInput Input { get; private set; }
    public Rigidbody rigidbody { get; private set; }
    [field: SerializeField] public float SpeedModifier { get; private set; }
    [field: SerializeField] public float RotateModifier { get; private set; }
    [field: SerializeField] public float minXLook { get; private set;}
    [field: SerializeField] public float maxXLook { get; private set;}
    public Transform PlayerVCTransform { get; private set; }
    
    float curXRot = 0;
    void Start()
    {
        Input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
        PlayerVCTransform = transform.Find("PlayerViewCam");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move(GetMovementDirection());
        if (Input.PlayerActions.Look.ReadValue<Vector2>().magnitude > 0)
        {
            CameraLook();
        }
    }

    public void Move(Vector3 moveDirection)
    {
        rigidbody.position += moveDirection * SpeedModifier * Time.deltaTime;
    }

    private void CameraLook()
    {
        Vector2 LookInput = Input.PlayerActions.Look.ReadValue<Vector2>();
        //Debug.Log(LookInput);

        curXRot += LookInput.y * RotateModifier;
        curXRot = Mathf.Clamp(curXRot, minXLook, maxXLook);
        PlayerVCTransform.localEulerAngles = new Vector3(-curXRot, 0, 0);

        rigidbody.transform.eulerAngles += new Vector3(0, LookInput.x * RotateModifier, 0);
    }

    private Vector3 GetMovementDirection()
    {
        Vector2 MoveInput = Input.PlayerActions.Movement.ReadValue<Vector2>();

        Vector3 forward = PlayerVCTransform.forward;
        Vector3 right = PlayerVCTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * MoveInput.y + right * MoveInput.x;
    }
}
