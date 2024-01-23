using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Join : MonoBehaviour
{
    string name;
    Player player;
    [SerializeField] ChoicePlayableCharacter choicePlayableCharacter;
    Button button;
    bool isLength = false;
    bool isChar = false;
    public void PlayerNameInput(string name)
    {
        this.name = name;
    }

    //버튼 활성화 체크
    public void NameLengthCheck(string name)
    {
        isLength = name.Length >= 2 && name.Length <= 10;
        button.interactable = isLength && isChar;
    }
    public void onCharacterCheck()
    {
        isChar = choicePlayableCharacter.eCharacter != PLAYABLE_CHAR.NONE;
        button.interactable = isLength && isChar;
    }

    public void DisableNameInputUI()
    {
        player.PlayerNameInput(name);
        player.PlayerCharacterInput(choicePlayableCharacter.eCharacter);
        GameManager.instance.DataChangeComplate();
    }
    private void Start()
    {
        player = GameManager.instance.GetPlayer;
        button = GetComponent<Button>();
    }
}
