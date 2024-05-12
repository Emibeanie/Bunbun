using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject startPanel;
    public GameObject creditsPanel;
    public TextMeshProUGUI timerText;
    private float currentTime;
    void Start()
    {
        currentTime = 0f;
        UpdateTimerText();
    }

    void Update()
    {
        if (!startPanel.activeSelf)
        {
            currentTime += Time.deltaTime;
            UpdateTimerText();
        }
       
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);
            string timeString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            timerText.text = timeString;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        startPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ToStartMenu()
    {
        startPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Movement");
    }
}
