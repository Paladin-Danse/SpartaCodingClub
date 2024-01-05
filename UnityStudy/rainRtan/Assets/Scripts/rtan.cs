using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rtan : MonoBehaviour
{
    float direction = 0.05f;
    SpriteRenderer rtan_SR;
    void Start()
    {
        rtan_SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction *= -1;
            rtan_SR.flipX = !rtan_SR.flipX;
        }
    }
    void FixedUpdate()
    {
        if(transform.position.x > 2.8f)
        {
            direction = -0.05f;
            rtan_SR.flipX = !rtan_SR.flipX;
        }
        if (transform.position.x < -2.8f)
        {
            direction = 0.05f;
            rtan_SR.flipX = !rtan_SR.flipX;
        }
        transform.position += new Vector3(direction, 0, 0);
        Debug.Log(transform.position.x);
    }

    void Move()
    {
        
        transform.position -= new Vector3(0.05f, 0, 0);
    }
}
