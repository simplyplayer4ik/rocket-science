using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 1;
    [SerializeField] float currentLevelDelay = 2.5f;
    [SerializeField] int currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;  

    }

    void Update()
    {
        
    }
    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        if (currentScene +1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentScene +1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public IEnumerator LevelReload()
    {
        yield return new WaitForSeconds(currentLevelDelay);
        SceneManager.LoadScene(currentScene);
    }
}
