using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    [SerializeField] TextMeshProUGUI scoreText;
    public int score;
    public EnemySpawn enemySpawn;

    public void GameOver()
    {
        gameOverScreen.Setup(score);
    }
    

    private void Awake()
    {
        score = 0;
        UpdateScore();
        if(score ==15)
        {
            enemySpawn.maxEnemies = 20;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }


    void UpdateScore()
    {
        scoreText.text = $"Enemies Killed: {score}";
    }
}
