using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public PlayerStatus status;

    public TextMeshProUGUI AttackPointTxt;
    public TextMeshProUGUI DefencePointTxt;
    public TextMeshProUGUI HealthPointTxt;
    public TextMeshProUGUI CriticalPointTxt;

    public void OnPanel(bool setBool)
    {
        if (setBool)
        {
            gameObject.SetActive(setBool);

            if (AttackPointTxt && DefencePointTxt && HealthPointTxt && CriticalPointTxt)
            {
                AttackPointTxt.text = status.attackPoint.ToString();
                DefencePointTxt.text = status.defencePoint.ToString();
                HealthPointTxt.text = status.currentHP + " / " + status.maxHP.ToString();
                CriticalPointTxt.text = status.criticalPoint.ToString();
            }
            else
                Debug.Log("StatusUI : �������ͽ� �����Ϳ� Null���� �����մϴ�.");
        }
        else
            gameObject.SetActive(false);
    }
}
