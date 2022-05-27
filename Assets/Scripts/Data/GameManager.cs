using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [Header("Enemies")]
    public GameObject[] enemyPrefabs;

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
    public GameObject GameOverText;

    [Header("Score")]
    public Text currentScoreCounter;
    public int scoreCount = 0;
    public Text bestScoreCounter;

    [Header("Scripts")]
    private Enemy enemyScript;
    private DataManager dataManagerScript;

    [Header("Difficulty")]
    public int difficultyLevel = 1;
    public TextMeshProUGUI currentLevel;
    public string playerName;

    [Header("Tutorial")]
    public GameObject tutorial;
    public KeyCode turnOffTutorial = KeyCode.Space;
    
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
        DataManager.IsGameOver = false;
        DataManager.IsGameFinished = false;

        scoreCount = 0;
        playerName = MenuUIHandler.playerName;

        rightSpawn = true;
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
        SetDifficultyLevel();
        ShowDifficultyLevel();
        TurnOffTutorial();

        if (DataManager.IsGameOver == true)
        {
            GameOver();
        }

        if (DataManager.IsGameFinished == false && DataManager.IsGameOver == false)
        {
            PlayerPrefs.GetInt("Best Score");
            PlayerPrefs.GetString("Best Player");
        } 
        if (DataManager.IsGameFinished == true || DataManager.IsGameOver == true)
        {
            if (scoreCount > PlayerPrefs.GetInt("Best Score"))
            {
                PlayerPrefs.SetInt("Best Score", scoreCount);
                PlayerPrefs.SetString("Best Player", playerName);
            }
        }
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // spawn random enemy at random position
        if (rightSpawn) // right border
        {
            Vector2 spawnpos1 = new Vector2(spawnRangeX, Random.Range(-spawnRangeY, spawnRangeY));

            Instantiate(enemyPrefabs[enemyIndex], new Vector3(spawnpos1.x, Random.Range(-spawnpos1.y, spawnpos1.y)),
            enemyPrefabs[enemyIndex].transform.rotation);

            if (difficultyLevel == 2)
            {
                Instantiate(enemyPrefabs[enemyIndex], new Vector3(spawnpos1.x, Random.Range(-spawnpos1.y, spawnpos1.y)),
                enemyPrefabs[enemyIndex].transform.rotation);
            }
        }
        if (leftSpawn) // left border
        {
            Vector2 spawnpos2 = new Vector2(-spawnRangeX, Random.Range(-spawnRangeY, spawnRangeY));

            Instantiate(enemyPrefabs[enemyIndex], new Vector3(spawnpos2.x, Random.Range(-spawnpos2.y, spawnpos2.y)),
            enemyPrefabs[enemyIndex].transform.rotation);

            if (difficultyLevel == 3)
            {
                Instantiate(enemyPrefabs[enemyIndex], new Vector3(spawnpos2.x, Random.Range(-spawnpos2.y, spawnpos2.y)),
                enemyPrefabs[enemyIndex].transform.rotation);
            }
        }
        if (upperSpawn) // upper border
        {
            Vector2 spawnpos3 = new Vector2(Random.Range(-spawnRangeX,spawnRangeX), spawnRangeY);

            Instantiate(enemyPrefabs[enemyIndex], new Vector3(Random.Range(-spawnpos3.x, spawnpos3.x), spawnpos3.y),
            enemyPrefabs[enemyIndex].transform.rotation);

            if (difficultyLevel == 4)
            {
                Instantiate(enemyPrefabs[enemyIndex], new Vector3(Random.Range(-spawnpos3.x, spawnpos3.x), spawnpos3.y),
                enemyPrefabs[enemyIndex].transform.rotation);
            }
        }
        if (lowerSpawn) // lower border
        {
            Vector2 spawnpos4 = new Vector2(Random.Range(-spawnRangeX,spawnRangeX),-spawnRangeY);

            Instantiate(enemyPrefabs[enemyIndex], new Vector3(Random.Range(-spawnpos4.x, spawnpos4.x), spawnpos4.y),
            enemyPrefabs[enemyIndex].transform.rotation);

            if (difficultyLevel == 5)
            {
                Instantiate(enemyPrefabs[enemyIndex], new Vector3(Random.Range(-spawnpos4.x, spawnpos4.x), spawnpos4.y),
                enemyPrefabs[enemyIndex].transform.rotation);
            }
        }        
    }

    void CountScore()
    {
        currentScoreCounter.text = "Score: " + scoreCount + " Name: " + playerName;
    }

    public void ShowBestScore()
    {
        bestScoreCounter.text = 
        "Best score: " + PlayerPrefs.GetInt("Best Score") + " Name: " + PlayerPrefs.GetString("Best Player");
    }

    public void ShowDifficultyLevel()
    {
        currentLevel.text = 
        "Level: " + difficultyLevel;
    }

    void SetDifficultyLevel()
    {
        if (scoreCount > 15)
        {
            difficultyLevel = 2;
            leftSpawn = true;
        }
        if (scoreCount > 35)
        {
            difficultyLevel = 3;
            upperSpawn = true;
        }
        if (scoreCount > 60)
        {
            difficultyLevel = 4;
            lowerSpawn = true;
        }
        if (scoreCount > 90)
        {
            difficultyLevel = 5;
            lowerSpawn = true;
        }
    }

    void TurnOffTutorial()
    {
        if (Input.GetKeyDown(turnOffTutorial))
        {
            tutorial.SetActive(false);
        }
    }

    void GameOver()
    {
        GameOverText.SetActive(true);
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(1);
                DataManager.IsGameFinished = true;
                DataManager.IsGameFinished = false;
                DataManager.IsGameOver = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(0);
                DataManager.IsGameFinished = true;
                DataManager.IsGameFinished = false;
                DataManager.IsGameOver = false;
            }
    }
}
