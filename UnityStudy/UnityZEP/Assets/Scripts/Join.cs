using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Join : MonoBehaviour
{
    string name;
    Player player;
    GameObject UICanvas;
    [SerializeField] ChoicePlayableCharacter choicePlayableCharacter;
    Button button;
    public void PlayerNameInput(string name)
    {
        this.name = name;
    }

    public void NameLengthCheck(string name)
    {
        button.interactable = name.Length >= 2 && name.Length <= 10;
    }

    public void DisableNameInputUI()
    {
        player.PlayerNameInput(name);
        player.PlayerCharacterInput(choicePlayableCharacter.eCharacter);
        UICanvas.SetActive(false);
        GameManager.instance.InitGame();
    }
    private void Start()
    {
        player = GameManager.instance.GetPlayer;
        UICanvas = transform.parent.gameObject;
        button = GetComponent<Button>();
    }
}
