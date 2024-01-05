using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class moneyItem : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] int earnedMoney = 1;
    float MoveSpeed = 1.5f;
    bool OnMove = false;
    private void Start()
    {
        dog = GameObject.Find("dog");
    }

    private void OnEnable()
    {
        OnMove = false;
        StartCoroutine(UItoMove());
    }

    private void FixedUpdate()
    {
        if (OnMove)
        {
            transform.position = Vector3.Lerp(transform.position, dog.transform.position, Time.deltaTime * MoveSpeed);
        }
    }

    IEnumerator UItoMove()
    {
        yield return new WaitForSeconds(1.0f);
        OnMove = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag.Equals("Player"))
        {
            GameManager.instance.getMoney(earnedMoney);
            gameObject.SetActive(false);
        }
    }
}
