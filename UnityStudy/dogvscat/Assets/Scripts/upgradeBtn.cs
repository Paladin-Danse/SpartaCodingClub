using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeBtn : MonoBehaviour
{
    [SerializeField] upgradeData upgrade;
    [SerializeField] int upgradeCount = 0;
    public void OnUpgrade()
    {
        bool result = shopManager.instance.buyUpgrade(upgrade.Data.name, upgrade.Data.cost);
        if (result)
        {
            if (upgradeCount == 0) gameObject.SetActive(false);
            else upgradeCount--;
        }
    }
}
