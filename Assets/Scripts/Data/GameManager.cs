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

    [Header("Enemies")]
    public GameObject enemyPrefab;

    [Header("Spawn zones")]
    private float spawnRangeX = 35f;
    private float spawnRangeY = 25f;
    private float startDelay = 2;
    private float spawnInterval = 4f;
    public bool rightSpawn;
    public bool leftSpawn;
    public bool upperSpawn;
    public bool lowerSpawn;

    [Header("Game over")]
    public bool IsGameOver;
    public GameObject GameOverText;
    public static bool IsGameFinished;

    [Header("Score")]
    public Text currentScoreCounter;
    public int scoreCount = 0;
    public Text bestScoreCounter;

    [Header("Scripts")]
    private Enemy enemyScript;
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

        rightSpawn = false;
        leftSpawn = false;
        upperSpawn = false;
        lowerSpawn = false;

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
        if (rightSpawn) // right border
        {
            Vector2 spawnpos1 = new Vector2(spawnRangeX, Random.Range(-spawnRangeY, spawnRangeY));

            Instantiate(enemyPrefab, new Vector3(spawnpos1.x, Random.Range(-spawnpos1.y, spawnpos1.y)),
            enemyPrefab.transform.rotation);
        }
        if (leftSpawn) // left border
        {
            Vector2 spawnpos2 = new Vector2(-spawnRangeX, Random.Range(-spawnRangeY, spawnRangeY));

            Instantiate(enemyPrefab, new Vector3(spawnpos2.x, Random.Range(-spawnpos2.y, spawnpos2.y)),
            enemyPrefab.transform.rotation);
        }
        if (upperSpawn) // upper border
        {
            Vector2 spawnpos3 = new Vector2(Random.Range(-spawnRangeX,spawnRangeX), spawnRangeY);

            Instantiate(enemyPrefab, new Vector3(Random.Range(-spawnpos3.x, spawnpos3.x), spawnpos3.y),
            enemyPrefab.transform.rotation);
        }
        if (lowerSpawn) // lower border
        {
            Vector2 spawnpos4 = new Vector2(Random.Range(-spawnRangeX,spawnRangeX),-spawnRangeY);

            Instantiate(enemyPrefab, new Vector3(Random.Range(-spawnpos4.x, spawnpos4.x), spawnpos4.y),
            enemyPrefab.transform.rotation);
        }        
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
