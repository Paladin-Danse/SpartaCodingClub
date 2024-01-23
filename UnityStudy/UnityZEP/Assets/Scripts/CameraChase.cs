using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] float clampMinX;
    [SerializeField] float clampMaxX;
    [SerializeField] float clampMinY;
    [SerializeField] float clampMaxY;
    Vector3 LockPos = new Vector3(0, 0, -10);
    bool onCam = true;
    
    // Update is called once per frame
    void Update()
    {
        if(onCam && LockPos != null)
        {
            if (transform.localPosition != LockPos) transform.localPosition = LockPos;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampMinX, clampMaxX), Mathf.Clamp(transform.position.y, clampMinY, clampMaxY), -10);
        }
    }
}
