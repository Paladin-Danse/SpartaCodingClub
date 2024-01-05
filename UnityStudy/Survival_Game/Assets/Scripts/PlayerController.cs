using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //현재 플레이어의 상태
    bool onPlayerMove = true;
    private bool isRun = false;
    private bool isWalk = false;
    private bool isGround = true;
    private bool isCrouch = false;

    //플레이어 및 행동의 밸류값
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    private float applySpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float cameraRotationLimit;
    private float currentCameraRotationX = 0;
    private Vector3 lastPos;

    //필요한 컴포넌트
    [SerializeField] private Camera theCamera;
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;
    private GunController theGunController;
    private Crosshair theCrosshair;
    [SerializeField] private CloseWeaponController theCloseWeaponController;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        theGunController = FindObjectOfType<GunController>();
        theCrosshair = FindObjectOfType<Crosshair>();

        lastPos = transform.position;
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }
    //체크하는 것들만
    private void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
    }
    //동작하는 것들만
    void FixedUpdate()
    {
        if (onPlayerMove)
        {
            Move();
            MoveCheck();
            CameraRotation();
            CharacterRotation();
        }
    }
    //지면 체크
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        theCrosshair.RunningAnimation(!isGround);
    }
    //점프 체크
    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            if (isCrouch) Crouch();
        }
    }
    //점프
    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }
    //달리기 체크
    private void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    //앉기 체크
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGround)
        {
            Crouch();
        }
    }
    //앉기
    private void Crouch()
    {
        isCrouch = !isCrouch;
        theCrosshair.CrouchingAnimation(isCrouch);

        if(isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        StartCoroutine(CrouchCoroutine());
    }
    //앉기 동작
    IEnumerator CrouchCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while(_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 10f * Time.deltaTime);
            theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, _posY, theCamera.transform.localPosition.z);
            if (count >= 100) break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x, applyCrouchPosY, theCamera.transform.localPosition.z);
    }
    //달리기
    private void Running()
    {
        if (isCrouch) Crouch();
        if (theGunController.isActiveAndEnabled) theGunController.CancelFineSight();

        isRun = true;
        if (theCrosshair.isActiveAndEnabled) theCrosshair.RunningAnimation(isRun);
        if (theGunController.isActiveAndEnabled) theGunController.getCurrentGun.RunningAnimation(isRun);
        if (theCloseWeaponController.isActiveAndEnabled) theCloseWeaponController.RunningAnimation(isRun);
        applySpeed = runSpeed;
        
    }
    //걷기
    private void RunningCancel()
    {
        isRun = false;
        if (theCrosshair.isActiveAndEnabled) theCrosshair.RunningAnimation(isRun);
        if (theGunController.isActiveAndEnabled) theGunController.getCurrentGun.RunningAnimation(isRun);
        if (theCloseWeaponController.isActiveAndEnabled) theCloseWeaponController.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }
    //움직이기
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
    private void MoveCheck()
    {
        if (isRun == false && isCrouch == false && isGround)
        {
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
                isWalk = true;
            else
                isWalk = false;

            if (theCrosshair.isActiveAndEnabled) theCrosshair.WalkingAnimation(isWalk);
            if (theGunController.isActiveAndEnabled) theGunController.getCurrentGun.WalkingAnimation(isWalk);
            if (theCloseWeaponController.isActiveAndEnabled) theCloseWeaponController.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }
    }
    //고개 위아래로 움직이기
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }
    //고개 좌우로 움직이기
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
