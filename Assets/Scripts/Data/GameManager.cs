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
    public GameObject enemyPrefab;
    private float spawnRangeX = 13;
    private float spawnPosY = 7;
    private float startDelay = 2;
    private float spawnInterval = 4f;
    private Enemy enemyScript;
    public bool IsGameOver;
    public GameObject GameOverText;
    public static bool IsGameFinished;
    public Text currentScoreCounter;
    public int scoreCount = 0;
    public Text bestScoreCounter;
    private DataManager dataManagerScript;
    
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
        IsGameFinished = false;
        scoreCount = 0;
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
        dataManagerScript = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    void Update()
    {
        CountScore();
        ShowBestScore();

        if (IsGameOver == true)
        {
            GameOver();
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

    void CountScore()
    {
        currentScoreCounter.text = "Score: " + scoreCount + " Name: " + dataManagerScript.playerName;
    }

    public void ShowBestScore()
    {
        bestScoreCounter.text = 
        "Best score: " + DataManager.currentBestScore + " Name: " + DataManager.currentBestPlayerName;
    }

    void GameOver()
    {
        GameOverText.SetActive(true);
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(1);
                IsGameFinished = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(0);
                IsGameFinished = true;
            }
    }
}
