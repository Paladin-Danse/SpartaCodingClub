using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class shopManager : MonoBehaviour
{
    public static shopManager instance;

    public struct upgradeState
    {
        public bool onDoubleShot;
        public int fullnessPowerLvl;
        public int foodSpeedLvl;
        public upgradeState(bool mDoubleShot, int mFullnessPowerLvl, int mFoodSpeedLvl)
        {
            this.onDoubleShot = mDoubleShot;
            this.fullnessPowerLvl = mFullnessPowerLvl;
            this.foodSpeedLvl = mFoodSpeedLvl;
        }
    }
    public upgradeState sUpgradeState;
    [SerializeField] GameObject shopUI;
    RectTransform shopUIRect;
    bool shopUIEnable = false;
    [SerializeField] Vector3 OnEnablePos;
    [SerializeField] Vector3 OnDisablePos;
    //float shopEnableTime = 3.0f;
    [SerializeField] float UIMoveSpeed = 1.0f;
    //bool isDisableShopUI_playing = false;
    //[SerializeField] float OnTime, plusTime = 0f;
    [SerializeField] List<upgradeData> upgradeList;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        shopUIRect = shopUI.GetComponent<RectTransform>();

        initGame();
    }
    private void initGame()
    {
        sUpgradeState = new upgradeState(false, 0, 0);
    }
    private void Update()
    {
        float InputWheel = Input.GetAxis("Mouse ScrollWheel");
        if (InputWheel > 0)
        {
            shopUIEnable = true;
        }
        if(InputWheel < 0)
        {
            shopUIEnable = false;
        }
    }
    private void FixedUpdate()
    {
        if(shopUIEnable)
        {
            shopUIRect.position = Vector3.MoveTowards(shopUIRect.position, OnEnablePos, UIMoveSpeed);
            Time.timeScale = 0.5f;
        }
        else
        {
            shopUIRect.position = Vector3.MoveTowards(shopUIRect.position, OnDisablePos, UIMoveSpeed);
            Time.timeScale = 1.0f;
        }
    }
    /*코루틴을 활용한 shop열고 닫기(안 씀)
    IEnumerator DisableShopUI()
    {
        isDisableShopUI_playing = true;
        yield return new WaitForSeconds(shopEnableTime + plusTime);
        shopUIEnable = false;
        plusTime = 0f;
        isDisableShopUI_playing = false;
    }
    */
    public bool buyUpgrade(string upgradeName, int cost)
    {
        if (GameManager.instance.money < cost) return false;
        switch(upgradeName)
        {
            case "doubleShot":
                sUpgradeState.onDoubleShot = true;
                break;
            case "power":
                sUpgradeState.fullnessPowerLvl++;
                GameManager.instance.foodUpgrade();
                break;
            case "speed":
                sUpgradeState.foodSpeedLvl++;
                GameManager.instance.foodUpgrade();
                break;
            default:
                return false;
        }
        GameManager.instance.lostMoney(cost);
        return true;
    }
}
