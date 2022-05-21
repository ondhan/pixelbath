using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public Text currentScoreCounter;
    public Text bestScoreCounter;
    public int scoreCount = 0;
    public int bestScore = 0;
    public GameObject enemyPrefab;
    private float spawnRangeX = 13;
    private float spawnPosY = 7;
    private float startDelay = 2;
    private float spawnInterval = 4f;

    public bool IsGameOver;
    public GameObject GameOverText;

    private Enemy enemyScript;
    private MenuUIHandler menuUIHandlerScript;
    private string playerName;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    void Start()
    {
        IsGameOver = false;
        scoreCount = 0;
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        CountScore();

        if (IsGameOver == true)
        {
            GameOverText.SetActive(true);
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(0);
            }
        }
    }

    void CountScore()
    {
        currentScoreCounter.text = "Score: " + scoreCount + playerName;
    }

    void CountBestScore()
    {
        bestScoreCounter.text = "Best score: " + bestScore + " Name:";

        if (scoreCount > bestScore)
        {
            bestScore = scoreCount;
        }
    }


    void SpawnEnemy()
    {
        // spawn random enemy at random position
        Vector2 spawnpos = new Vector2(Random.Range(-spawnRangeX,spawnRangeX),
        spawnPosY);
        Instantiate(enemyPrefab, new Vector3(Random.Range(-spawnRangeX,spawnRangeX), spawnPosY),
        enemyPrefab.transform.rotation);
    }
}
