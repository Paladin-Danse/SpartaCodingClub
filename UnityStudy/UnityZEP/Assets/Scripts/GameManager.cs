using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYABLE_CHAR
{
    PENGUIN
}
public class GameManager : MonoBehaviour
{
    static GameManager m_Instance;
    public GameManager instance
    {
        get
        {
            if (m_Instance == null) m_Instance = this;
            return m_Instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
