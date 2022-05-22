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

    public Text nameInput;

    // Update is called once per frame
    void Start()
    {
        
    }

    public void ReadStringInput(string s)
    {
        playerName = s;
        Debug.Log(playerName);
    }

    public void SetPlayerName()
    {
        playerName = nameInput.GetComponentInChildren<TMP_InputField>().text;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
