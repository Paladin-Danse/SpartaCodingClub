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

    void Start()
    {
        Input = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
        PlayerVCTransform = transform.Find("PlayerViewCam");
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
        Debug.Log(LookInput);

        float curXRot = 0;
        curXRot += LookInput.y * RotateModifier;
        curXRot = Mathf.Clamp(curXRot, minXLook, maxXLook);
        PlayerVCTransform.localEulerAngles = new Vector3(-curXRot, 0, 0);//어째선지는 몰라도 처음엔 정상값이 들어갔다가 다시 들어갈 때 0이 들어와 값이 덮어진다.

        rigidbody.transform.eulerAngles += new Vector3(0, LookInput.x * RotateModifier, 0);
    }
    //void CameraLook()
    //{
    //    camCurXRot += mouseDelta.y * lookSensitivity;
    //    camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
    //    cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

    //    transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    //}

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
