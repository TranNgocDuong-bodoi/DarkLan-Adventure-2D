using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    [SerializeField] private float timeLoadScense = 1f;
    private int scenesIndex;
    private int nextScenesIndex;
    private int numberOfScenes;

    IEnumerator LoadNextScenes()
    {
        yield return new WaitForSecondsRealtime(timeLoadScense);
        nextScenesIndex = SetUpScenesIndex();
        SceneManager.LoadScene(nextScenesIndex);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScenes());
        }
    }
    private int SetUpScenesIndex()
    {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;
        scenesIndex = SceneManager.GetActiveScene().buildIndex;
        return ((scenesIndex + 1) == numberOfScenes) ? 0 : scenesIndex + 1;   
    }
}
