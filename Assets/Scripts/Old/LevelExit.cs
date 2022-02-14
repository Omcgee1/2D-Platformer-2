using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    float levelLoadDelay = 1f;
    float levelExitSlowMo = 0.25f;
    float normalTime = 1f;
    int currentSceneIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = levelExitSlowMo;
        yield return new WaitForSeconds(levelLoadDelay);
        Time.timeScale = normalTime;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
