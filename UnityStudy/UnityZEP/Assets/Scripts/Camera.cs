using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] float clampMinX;
    [SerializeField] float clampMaxX;
    [SerializeField] float clampMinY;
    [SerializeField] float clampMaxY;
    Vector3 LockPos = new Vector3(0, 0, -10);
    bool onCam = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onCam)
        {
            if(transform.localPosition != LockPos) transform.localPosition = LockPos;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampMinX, clampMaxX), Mathf.Clamp(transform.position.y, clampMinY, clampMaxY), -10);
        }
    }
}
