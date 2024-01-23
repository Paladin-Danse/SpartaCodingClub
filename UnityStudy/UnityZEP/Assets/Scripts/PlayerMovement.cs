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

    //������Ʈ
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
    //�̵�
    private void Move()
    {
        PlayerInputX = Input.GetAxisRaw("Horizontal");
        PlayerInputY = Input.GetAxisRaw("Vertical");
        //Ÿ���� �ֱ�� ��ĭ�� �����̵��� ������ ¥����.
        //�������� ������ �������ִ� ����. Ÿ�� �̵����� ��ҿ� ���� normalized(����ȭ)�� �밢�� Ÿ���̵��� ���صǾ� ����
        Vector3 moveDirection = ((transform.right * PlayerInputX) + (transform.up * PlayerInputY));
        //magnitude : ������ ũ�Ⱚ = sqrt(x^2 + y^2 + z^2). ����Ű�� �Է����� ������(0,0,0)�̱� ������ �������� �ʴ´�.
        if (moveDirection.magnitude > 0 && moveCorout == null)
        {
            anim.SetBool("isWalk", true);
            moveCorout = MoveCoroutine(moveDirection);
            StartCoroutine(moveCorout);
        }
        else if(moveDirection.magnitude == 0) anim.SetBool("isWalk", false);
    }
    //��,�� ȸ�� ó��
    private void LookAtMouse()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }
    //���� Ÿ�Ͽ��� ���� Ÿ�ϱ��� �̵� �ڷ�ƾ
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
    //���õ� ĳ������ ��θ� �޾ƿ�. �޾ƿ� ��θ� ���� ĳ������ �ִϸ����Ϳ� �������� ������.
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