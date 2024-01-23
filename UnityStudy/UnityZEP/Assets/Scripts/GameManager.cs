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
    [SerializeField] Canvas InGameUI;
    //������ ���� ������ ������ �ԷµǱ� ���� �ķ� ������ ����.
    bool onInitGame = false;
    public Player GetPlayer { get { return player; } }

    // Start is called before the first frame update
    void Start()
    {
        if (player)
        {
            playerMove = player.gameObject.GetComponent<PlayerMovement>();
            playerMove.isMovable = onInitGame;//�÷��̾ ������ �ԷµǴ� �߿� �������� �ʵ��� �ڸ� ����.
        }
        else
            Debug.Log("���� : ���ӸŴ����� �÷��̾ ����.");
        InputDataChange();
        onInitGame = false;
    }
    
    //������ ���� â
    public void InputDataChange()
    {
        InputUI.gameObject.SetActive(true);
        InGameUI.gameObject.SetActive(false);
        setPause(true);
    }

    public void DataChangeComplate()
    {
        if (!onInitGame) onInitGame = true;
        setPause(false);
        InputUI.gameObject.SetActive(false);
        InGameUI.gameObject.SetActive(true);
    }

    public void setPause(bool setbool)
    {
        playerMove.isMovable = !setbool;
    }
}
