using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour
{
    [SerializeField] List<string> tutorialsList;
    private int indexOfTutorial;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI levelText;
    void Start()
    {
        indexOfTutorial = 1;
        tutorialText.SetText(tutorialsList[0]);
    }

    void Update()
    {
         
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(indexOfTutorial > tutorialsList.Count)
        {
            return;
        }
        if(collision.gameObject.CompareTag("trigger"))
        {
            this.gameObject.SetActive(true);
            Destroy(collision.gameObject);
            Debug.Log(tutorialsList[indexOfTutorial]);
            tutorialText.SetText(tutorialsList[indexOfTutorial]);
            indexOfTutorial ++;
            Invoke("HideTheText",5f);
        }
    }
    private void HideTheText()
    {
        tutorialText.SetText("");
    }
}
