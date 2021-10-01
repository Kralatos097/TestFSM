using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIScript : MonoBehaviour
{
    public GameObject UIDeath;
    public GameObject UIVictoire;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void Victoire()
    {
        Time.timeScale = 0;
        UIVictoire.SetActive(true);
    }

    public void Death()
    {
        Time.timeScale = 0;
        UIDeath.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
