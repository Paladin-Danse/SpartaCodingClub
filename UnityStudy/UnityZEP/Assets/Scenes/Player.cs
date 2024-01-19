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
        //������ �۾��ϴ� �� ���� �״�� ������. ���⼭ �߰����� ���� �ʿ�.
        //�ϴ� ������ �� Ÿ���� �ִٰ� �����ϰ� Ÿ���� �ֱ�� ��ĭ�� �����̵��� ������ ¥����.
        //�������� ������ �������ִ� ����. �¿��� �����Ӱ� ������ �������� ���ϰ� �װ��� ����ȭ���� ���Ͱ��� �����ϰ� �����.
        Vector3 moveDirection = ((transform.right * PlayerInputX) + (transform.up * PlayerInputY)).normalized;
        //���⺤���� ���� �þ�°� �����ϸ� ���⺤�Ϳ� Speed���� ���ϰ� ��ǻ���� ���� ���̶����� ����� �ӵ� ���̸� ���ֱ� ���� deltaTime�� �߰��� ���Ѵ�.
        //magnitude : ������ ũ�Ⱚ = sqrt(x^2 + y^2 + z^2). ����Ű�� �Է����� ������(0,0,0)�̱� ������ �������� �ʴ´�.
        if (moveDirection.magnitude > 0)
            playerRigid.MovePosition(transform.position + (moveDirection * PlayerMoveSpeed * Time.deltaTime));
    }
}
