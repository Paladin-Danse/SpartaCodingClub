using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePlayableCharacter : MonoBehaviour
{
    GameObject Character;
    [SerializeField] GameObject ChoiceUI;
    public PLAYABLE_CHAR eCharacter;
    void Start()
    {
        if (ChoiceUI) ChoiceUI.SetActive(false);
    }

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
            Character = obj;
            Character.SetActive(true);
        }
        else Debug.Log("Character is null");
    }

    public void SetChoiceUI(bool active)
    {
        ChoiceUI.SetActive(active);
    }
}
