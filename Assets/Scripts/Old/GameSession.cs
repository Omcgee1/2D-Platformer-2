using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float score = 0f;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            playerLives--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            livesText.text = playerLives.ToString();
        }

        else
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    public void AddToScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
