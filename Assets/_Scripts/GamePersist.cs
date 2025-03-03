using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersist : MonoBehaviour
{
    private static GamePersist instance;
    private void Awake() {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        if(FindObjectsOfType<GamePersist>().Length == 1)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    public void GamePersistReset()
    {
        Destroy(gameObject);
    }
}
