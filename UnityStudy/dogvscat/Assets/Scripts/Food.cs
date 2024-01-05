using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float _fullness;
    [SerializeField] float _upgrade;
    public float fullness { get { return _fullness + _upgrade; } }
    void OnEnable()
    {
        _fullness = GameManager.instance.fullness;
        _upgrade = GameManager.instance.upgradeFullness;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0f, 0.5f, 0f);
    }

    private void Update()
    {
        if(transform.position.y > 26.0f)
            gameObject.SetActive(false);
    }
    
    //food에서 cat에게 포만감을 주는 함수
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("cat"))
        {
            coll.GetComponent<Cat>().catEating(fullness);
            gameObject.SetActive(false);
        }
    }
    
    public void FullnessPowerBalancing(float upgrade)
    {
        _upgrade = upgrade;
    }
    public void SetfoodStatus()
    {
        _fullness = GameManager.instance.fullness;
        _upgrade = GameManager.instance.upgradeFullness;
    }
}
