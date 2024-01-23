using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager instance
    {
        get
        {
            if (m_Instance == null) m_Instance = FindObjectOfType<GameManager>();
            return m_Instance;
        }
    }
    //��ũ��Ʈ���� �ҷ������� Null�� ��ȯ���� [serializeField]�� ��ȯ. �ٲ��� �� ��.
    [SerializeField] Player player;
    PlayerMovement playerMove;
    [SerializeField] Canvas InputUI;
    //������ ���� ������ ������ �ԷµǱ� ���� �ķ� ������ ����.
    bool onInit = false;
    public Player GetPlayer { get { return player; } }

    // Start is called before the first frame update
    void Start()
    {
        InputUI.gameObject.SetActive(true);
        if (player)
        {
            playerMove = player.gameObject.GetComponent<PlayerMovement>();
            playerMove.isMovable = onInit;//�÷��̾ ������ �ԷµǴ� �߿� �������� �ʵ��� �ڸ� ����.
        }
        else
            Debug.Log("���� : ���ӸŴ����� �÷��̾ ����.");
    }
    public void InitGame()
    {
        onInit = true;
        playerMove.isMovable = onInit;
    }
}
