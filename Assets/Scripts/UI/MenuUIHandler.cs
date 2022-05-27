using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public static string playerName;
    public TMP_InputField nameInput;

    public void ReadStringInput(string s)
    {
        playerName = s;
    }

    public void StartGame()
    {
        if (playerName == null || playerName == "" || playerName == " ")
        {

        }
        else
        {
            SceneManager.LoadScene(1);
            DataManager.IsGameFinished = false;
            DataManager.IsGameOver = false;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
