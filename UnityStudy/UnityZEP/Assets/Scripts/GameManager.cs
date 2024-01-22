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
    Player player;
    PlayerMovement playerMove;
    [SerializeField] Canvas InputUI;
    bool onInit = false;
    [SerializeField] Sprite spritePenguin;
    [SerializeField] Sprite spriteWarrior;
    public Player GetPlayer { get { return player; } }

    // Start is called before the first frame update
    void Start()
    {
        InputUI.gameObject.SetActive(true);
        if(GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            playerMove = player.gameObject.GetComponent<PlayerMovement>();
            //Camera.main.GetComponent<CameraChase>().ChasePos(player.transform);

            playerMove.isMovable = onInit;
        }
    }
    public void InitGame()
    {
        onInit = true;
        playerMove.isMovable = onInit;
    }
    public void CharacterSelect(PLAYABLE_CHAR select)
    {
        switch (select)
        {
            case PLAYABLE_CHAR.PENGUIN:

                break;
            case PLAYABLE_CHAR.WARRIOR:
                break;
            default:
                break;
        }
    }
}
