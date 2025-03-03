using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]private int numberOfLife = 3;
    private static GameController instance;
    private void Awake() {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        if(FindObjectsOfType<GameController>().Length == 1)
        {
        instance = this;
        DontDestroyOnLoad(instance);
        }
    }
    public void PlayerDieController()
    {
        if(numberOfLife > 1)
        {
            TakeLife();
        }
        else
        {
            RestartGame();
        }
    }
    private void TakeLife()
    {
        this.numberOfLife--;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(this.numberOfLife);
        SceneManager.LoadScene(sceneIndex);
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<GamePersist>().GamePersistReset();
    }
}
