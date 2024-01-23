using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePlayableCharacter : MonoBehaviour
{
    [SerializeField] GameObject ChoiceUI;
    public PLAYABLE_CHAR eCharacter;
    List<GameObject> characters = new List<GameObject>();
    void Start()
    {
        if (ChoiceUI) ChoiceUI.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
            characters.Add(transform.GetChild(i).gameObject);
        eCharacter = PLAYABLE_CHAR.NONE;
    }
    //캐릭터를 선택했을 때 해당 캐릭터를 활성화.
    public void ChangedCharacter(string select)
    {
        GameObject obj = null;
        switch (select)
        {
            case "Penguin":
                eCharacter = PLAYABLE_CHAR.PENGUIN;
                obj = transform.Find("Penguin").gameObject;
                break;
            case "Warrior":
                eCharacter = PLAYABLE_CHAR.WARRIOR;
                obj = transform.Find("Warrior").gameObject;
                break;
            default:
                break;
        }
        if (obj != null)
        {
            //Active가 켜져있는 다른 캐릭터를 모두 끈다.
            while (characters.Find(character => character.activeSelf) != null)
            {
                characters.Find(character => character.activeSelf).SetActive(false);
            }
            obj.SetActive(true);
        }
        else Debug.Log("Character is null");
    }
    //캐릭터 선택 창 띄우기.
    public void SetChoiceUI(bool active)
    {
        ChoiceUI.SetActive(active);
    }
}
