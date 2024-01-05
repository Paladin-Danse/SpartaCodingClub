using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel : MonoBehaviour
{
    public void retry()
    {
        GameManager.i_GameManager.retry();
    }
}
