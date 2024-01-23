using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealtimeUI : MonoBehaviour
{
    DateTime realTime;
    Text realTimeTxt;
    // Start is called before the first frame update
    void Start()
    {
        realTime = DateTime.Now;
        realTimeTxt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        realTimeTxt.text = $"{realTime.Hour.ToString()} : {realTime.Minute.ToString()}";
    }
}