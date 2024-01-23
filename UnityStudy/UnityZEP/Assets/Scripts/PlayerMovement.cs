using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    float PlayerInputX;
    float PlayerInputY;
    [SerializeField]
    float PlayerMoveSpeed = 1.5f;
    public bool isMovable = true;
    IEnumerator moveCorout = null;

    //컴포넌트
    SpriteRenderer spriteRenderer;
    Animator anim;

    string Character_Path = "";
    
    // Update is called once per frame
    void Update()
    {
        if(isMovable && Character_Path != "")
        {
            Move();
            if(spriteRenderer) LookAtMouse();
        }
    }
    //이동
    private void Move()
    {
        PlayerInputX = Input.GetAxisRaw("Horizontal");
        PlayerInputY = Input.GetAxisRaw("Vertical");
        //타일을 주기당 한칸씩 움직이도록 로직을 짜야함.
        //움직임의 방향을 결정해주는 변수. 타일 이동에선 평소에 쓰던 normalized(정규화)가 대각선 타일이동에 방해되어 제거
        Vector3 moveDirection = ((transform.right * PlayerInputX) + (transform.up * PlayerInputY));
        //magnitude : 벡터의 크기값 = sqrt(x^2 + y^2 + z^2). 방향키를 입력하지 않으면(0,0,0)이기 때문에 움직이지 않는다.
        if (moveDirection.magnitude > 0 && moveCorout == null)
        {
            anim.SetBool("isWalk", true);
            moveCorout = MoveCoroutine(moveDirection);
            StartCoroutine(moveCorout);
        }
        else if(moveDirection.magnitude == 0) anim.SetBool("isWalk", false);
    }
    //좌,우 회전 처리
    private void LookAtMouse()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }
    //현재 타일에서 다음 타일까지 이동 코루틴
    private IEnumerator MoveCoroutine(Vector3 Direction)
    {
        float t = 0;
        Vector3 beforePos = transform.position;
        Vector3 afterPos = transform.position + Direction;

        int layerMask = LayerMask.GetMask("Wall");

        if (Physics2D.Raycast(beforePos, Direction, 1.5f, layerMask))
        {
            moveCorout = null;
        }
        else
        {
            while (transform.position != afterPos)
            {
                t += (Time.deltaTime * PlayerMoveSpeed);
                transform.position = Vector3.Lerp(beforePos, afterPos, t);

                yield return null;
            }
            moveCorout = null;
        }
    }
    //선택된 캐릭터의 경로를 받아옴. 받아온 경로를 토대로 캐릭터의 애니메이터와 렌더러를 가져옴.
    public void getPlayerInitInfo(string path)
    {
        Character_Path = path;

        anim = transform.Find(Character_Path).GetComponent<Animator>();

        if (transform.Find(Character_Path))
            spriteRenderer = transform.Find(Character_Path + "/Sprite").GetComponent<SpriteRenderer>();
        else
            Debug.Log("Error");
    }
}