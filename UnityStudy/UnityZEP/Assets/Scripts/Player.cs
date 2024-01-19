using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    float PlayerInputX;
    float PlayerInputY;
    [SerializeField]
    float PlayerMoveSpeed = 1.5f;

    Rigidbody2D playerRigid;
    IEnumerator moveCorout;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        moveCorout = null;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        PlayerInputX = Input.GetAxisRaw("Horizontal");
        PlayerInputY = Input.GetAxisRaw("Vertical");
        //이전에 작업하던 걸 거의 그대로 가져옴. 여기서 추가적인 수정 필요.
        //일단 움직일 때 타일이 있다고 가정하고 타일을 주기당 한칸씩 움직이도록 로직을 짜야함.
        //움직임의 방향을 결정해주는 변수. normalized는 대각선 타일이동에 방해되어 제거
        Vector3 moveDirection = ((transform.right * PlayerInputX) + (transform.up * PlayerInputY));
        //magnitude : 벡터의 크기값 = sqrt(x^2 + y^2 + z^2). 방향키를 입력하지 않으면(0,0,0)이기 때문에 움직이지 않는다.
        if (moveDirection.magnitude > 0 && moveCorout == null)
        {
            moveCorout = MoveCoroutine(moveDirection);
            StartCoroutine(moveCorout);
        }
    }

    private IEnumerator MoveCoroutine(Vector3 Direction)
    {
        float t = 0;
        Vector3 beforePos = transform.position;
        Vector3 afterPos = transform.position + Direction;
        while (transform.position != afterPos)
        {
            t += (Time.deltaTime * PlayerMoveSpeed);
            transform.position = Vector3.Lerp(beforePos, afterPos, t);
            yield return null;
        }
        moveCorout = null;
    }
}
/*
 while (t <= 1.0f)
        {
            Liquid_Renderer.material.Lerp(oldLiquid, newLiquid, t);
            t += Time.deltaTime;

            yield return null;
        }
        Get_Liquid_Court = null;
 */
