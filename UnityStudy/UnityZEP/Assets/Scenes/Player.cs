using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    float PlayerInputX;
    float PlayerInputY;
    [SerializeField]
    float PlayerMoveSpeed = 1.5f;

    Rigidbody2D playerRigid;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        PlayerInputX = Input.GetAxis("Horizontal");
        PlayerInputY = Input.GetAxis("Vertical");
        //이전에 작업하던 걸 거의 그대로 가져옴. 여기서 추가적인 수정 필요.
        //일단 움직일 때 타일이 있다고 가정하고 타일을 주기당 한칸씩 움직이도록 로직을 짜야함.
        //움직임의 방향을 결정해주는 변수. 좌우의 움직임과 상하의 움직임을 더하고 그것을 정규화시켜 벡터값을 일정하게 만든다.
        Vector3 moveDirection = ((transform.right * PlayerInputX) + (transform.up * PlayerInputY)).normalized;
        //방향벡터의 값이 늘어나는걸 감지하면 방향벡터에 Speed값을 곱하고 컴퓨터의 성능 차이때문에 생기는 속도 차이를 없애기 위해 deltaTime을 추가로 곱한다.
        //magnitude : 벡터의 크기값 = sqrt(x^2 + y^2 + z^2). 방향키를 입력하지 않으면(0,0,0)이기 때문에 움직이지 않는다.
        if (moveDirection.magnitude > 0)
            playerRigid.MovePosition(transform.position + (moveDirection * PlayerMoveSpeed * Time.deltaTime));
    }
}
