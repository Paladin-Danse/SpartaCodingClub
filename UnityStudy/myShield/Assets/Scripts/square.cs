using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class square : MonoBehaviour
{
    void Start()
    {
        float x = Random.Range(-3.0f, 3.0f);
        float y = Random.Range(3.0f, 5.0f);
        transform.position = new Vector3(x, y, 0);

        float size = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector3(size, size, 0);
    }

    void Update()
    {
        if (transform.position.y < -5.0f)
            destroySquare();
    }

    public void destroySquare()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "balloon")
        {
            GameManager.Instance.GameOver();
        }
    }
}
