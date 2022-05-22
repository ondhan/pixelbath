using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static int currentBestScore = 0;
    public string playerName;
    public static string currentBestPlayerName;
    public static DataManager Instance;
    private GameManager gameManagerScript;
    private MenuUIHandler menuUIHandlerScript;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerName = MenuUIHandler.playerName;

        if (GameManager.IsGameFinished)
        {
            SaveBestScore();
        }
    }

    public void SaveBestScore()
    {
        if (gameManagerScript.scoreCount > currentBestScore)
        {
            currentBestScore = gameManagerScript.scoreCount;
            currentBestPlayerName = playerName;
        }
    }
}