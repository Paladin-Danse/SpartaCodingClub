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
    //ĳ���͸� �������� �� �ش� ĳ���͸� Ȱ��ȭ.
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
            //Active�� �����ִ� �ٸ� ĳ���͸� ��� ����.
            while (characters.Find(character => character.activeSelf) != null)
            {
                characters.Find(character => character.activeSelf).SetActive(false);
            }
            obj.SetActive(true);
        }
        else Debug.Log("Character is null");
    }
    //ĳ���� ���� â ����.
    public void SetChoiceUI(bool active)
    {
        ChoiceUI.SetActive(active);
    }
}
