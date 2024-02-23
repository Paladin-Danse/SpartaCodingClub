using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class MainGameStartBtn : MonoBehaviour
{
    [field: SerializeField] public string sceneName { get; private set; }
    public void MainSceneLoad()
    {
        SceneManager.LoadScene(sceneName);
    }
}
