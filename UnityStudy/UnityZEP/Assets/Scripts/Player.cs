using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PLAYABLE_CHAR
{
    PENGUIN,
    WARRIOR
}

public class Player : MonoBehaviour
{
    private string name;
    Canvas PlayerUI;
    Text nameTxt;
    PlayerMovement playerMove;
    //���߿� ĳ���� ������ �߰��� ���� ���� ����ĳ���� ����.
    GameObject currentCharacter;
    PLAYABLE_CHAR eCharacter;

    //�÷��̾� �̸� �Է�.
    public void PlayerNameInput(string name)
    {
        this.name = name;
        nameTxt.text = name;

        PlayerUI.gameObject.SetActive(true);

        Canvas.ForceUpdateCanvases();

        RectTransform textRect = nameTxt.GetComponent<RectTransform>();
        float textSizeX = textRect.rect.width;
        float textSizeY = textRect.rect.height;

        PlayerUI.transform.Find("BackGround").GetComponent<RectTransform>().sizeDelta = new Vector2(textSizeX + 40, textSizeY + 40);
    }
    //�÷��̾� ĳ���� �Է�.
    public void PlayerCharacterInput(PLAYABLE_CHAR select)
    {
        eCharacter = select;
        string characterPath = null;
        switch (eCharacter)
        {
            case PLAYABLE_CHAR.PENGUIN:
                characterPath = "Playable_Penguin";
                break;
            case PLAYABLE_CHAR.WARRIOR:
                characterPath = "Playable_Warrior";
                break;
            default:
                break;
        }
        currentCharacter = transform.Find(characterPath).gameObject;
        currentCharacter.SetActive(true);
        playerMove.getPlayerInitInfo(characterPath);
    }
    private void Start()
    {
        playerMove = GetComponent<PlayerMovement>();

        if (transform.Find("PlayerName"))
        {
            PlayerUI = transform.Find("PlayerName").GetComponent<Canvas>();
            if (PlayerUI.renderMode == RenderMode.WorldSpace && PlayerUI.worldCamera == null) PlayerUI.worldCamera = Camera.main;
            nameTxt = PlayerUI.transform.Find("PlayerNameTxt").GetComponent<Text>();
            PlayerUI.gameObject.SetActive(false);
        }
    }
}
