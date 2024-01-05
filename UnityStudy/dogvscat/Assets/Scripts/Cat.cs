using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Type
{
    normalCat = 0,
    fatCat,
    pirateCat
}

public class Cat : MonoBehaviour
{
    [SerializeField] private float full;
    [SerializeField] private float energy;
    GameObject hungry_Obj;
    GameObject full_Obj;
    private bool isFull;
    [SerializeField] private float moneyDropChance;
    [SerializeField] private Type type;
    Collider2D catColl;
    public Type getType
    {
        get
        {
            return type;
        }
    }
    float moveSpeed;

    void Start()
    {
        hungry_Obj = gameObject.transform.Find("hungry").gameObject;
        full_Obj = gameObject.transform.Find("full").gameObject;
        catColl = GetComponent<Collider2D>();

        switch (type)
        {
            case Type.normalCat:
                moveSpeed = 0.05f;
                full = 5f;
                break;
            case Type.fatCat:
                moveSpeed = 0.03f;
                full = 10f;
                break;
            case Type.pirateCat:
                moveSpeed = 0.1f;
                full = 5f;
                break;
        }
    }

    private void OnEnable()
    {
        float x = Random.Range(-8.5f, 8.5f);
        float y = 30f;
        transform.position = new Vector3(x, y, 0);
        isFull = false;
        if(catColl != null) catColl.enabled = true;
    }

    void FixedUpdate()
    {
        if (energy < full)
            transform.position += new Vector3(0, -moveSpeed, 0);
        else
        {
            if (transform.position.x > 0)
                transform.position += new Vector3(0.05f, 0, 0);
            else
                transform.position += new Vector3(-0.05f, 0, 0);
        }
        if (transform.position.y < -16f)
        {
            GameManager.instance.GameOver();
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "food")
        {
            if (energy < full)
            {
                energy += coll.GetComponent<Food>().fullness;
                coll.gameObject.SetActive(false);
                hungry_Obj.transform.Find("Canvas/front").localScale
                    = new Vector3(energy / full, 1, 1);
            }

            if (energy >= full && isFull == false)
                StartCoroutine(fullCat());
        }
    }
    */
    //외부에서 cat에게 포만감을 주기위한 함수
    public void catEating(float fullness)
    {
        energy += fullness;
        hungry_Obj.transform.Find("Canvas/front").localScale
                    = new Vector3(energy / full, 1, 1);

        if (energy >= full && isFull == false)
            StartCoroutine(fullCat());
    }

    protected IEnumerator fullCat()
    {
        GameManager.instance.addCat();
        hungry_Obj.SetActive(false);
        full_Obj.SetActive(true);
        isFull = true;
        catColl.enabled = false;
        if (Random.Range(0f, 10f) < moneyDropChance)
            GameManager.instance.makeMoneyItem(transform.position);

        yield return new WaitForSeconds(3.0f);

        hungry_Obj.SetActive(true);
        full_Obj.SetActive(false);

        energy = 0;
        hungry_Obj.transform.Find("Canvas/front").localScale
                    = new Vector3(energy / full, 1, 1);

        gameObject.SetActive(false);
    }
}
