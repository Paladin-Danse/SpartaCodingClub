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
        //������ �۾��ϴ� �� ���� �״�� ������. ���⼭ �߰����� ���� �ʿ�.
        //�ϴ� ������ �� Ÿ���� �ִٰ� �����ϰ� Ÿ���� �ֱ�� ��ĭ�� �����̵��� ������ ¥����.
        //�������� ������ �������ִ� ����. normalized�� �밢�� Ÿ���̵��� ���صǾ� ����
        Vector3 moveDirection = ((transform.right * PlayerInputX) + (transform.up * PlayerInputY));
        //magnitude : ������ ũ�Ⱚ = sqrt(x^2 + y^2 + z^2). ����Ű�� �Է����� ������(0,0,0)�̱� ������ �������� �ʴ´�.
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
