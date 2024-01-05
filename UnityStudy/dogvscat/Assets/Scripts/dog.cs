using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog : MonoBehaviour
{
    void Start()
    {
        
    }

     void Update()
    {
        if (GameManager.instance.OnGameOver == false)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(Mathf.Clamp(mousePos.x, -9.5f, 9.5f), transform.position.y, 0);
        }
    }
}
