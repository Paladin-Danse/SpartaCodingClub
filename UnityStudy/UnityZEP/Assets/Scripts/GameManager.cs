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
    //스크립트에서 불러오려다 Null값 반환으로 [serializeField]로 전환. 바꾸지 말 것.
    [SerializeField] Player player;
    PlayerMovement playerMove;
    [SerializeField] Canvas InputUI;
    //게임의 시작 기준을 정보가 입력되기 전과 후로 나누는 변수.
    bool onInit = false;
    public Player GetPlayer { get { return player; } }

    // Start is called before the first frame update
    void Start()
    {
        InputUI.gameObject.SetActive(true);
        if (player)
        {
            playerMove = player.gameObject.GetComponent<PlayerMovement>();
            playerMove.isMovable = onInit;//플레이어가 정보가 입력되는 중에 움직이지 않도록 자리 고정.
        }
        else
            Debug.Log("오류 : 게임매니저에 플레이어가 없음.");
    }
    public void InitGame()
    {
        onInit = true;
        playerMove.isMovable = onInit;
    }
}
