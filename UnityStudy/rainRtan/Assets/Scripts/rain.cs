using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    SpriteRenderer rain_SR;
    [SerializeField] int type, score;
    float x;
    float y;
    float size;
    void Start()
    {
        rain_SR = GetComponent<SpriteRenderer>();

        type = Random.Range(1, 5);

        switch(type)
        {
            case 1:
                size = 1.2f;
                score = 3;
                rain_SR.color = new Color(100 / 255f, 100 / 255f, 255 / 255f, 255 / 255f);
                break;
            case 2:
                size = 1.0f;
                score = 2;
                rain_SR.color = new Color(130 / 255f, 130 / 255f, 255 / 255f, 255 / 255f);
                break;
            case 3:
                size = 0.8f;
                score = 1;
                rain_SR.color = new Color(150 / 255f, 150 / 255f, 255 / 255f, 255 / 255f);
                break;
            default:
                size = 0.8f;
                score = -5;
                rain_SR.color = new Color(255.0f / 255.0f, 100.0f / 255.0f, 100.0f / 255.0f, 255.0f / 255.0f);
                break;
        }
        x = Random.Range(-2.7f, 2.7f);
        y = Random.Range(3.0f, 5.0f);
        transform.position = new Vector3(x, y, 0);
        transform.localScale = new Vector3(size, size, 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
        if (coll.gameObject.tag == "rtan")
        {
            GameManager.i_GameManager.addScore(score);
            Destroy(gameObject);
        }
    }
}
