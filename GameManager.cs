using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText; 
    public TextMeshProUGUI restartText;
    public Image gameOverPanel;
    public Image winPanel;
    public bool gameWon = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score >= 20)
        {
            WinGame();
        }
    }
    public void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinGame()
    {
        winPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = false;
    }
}
