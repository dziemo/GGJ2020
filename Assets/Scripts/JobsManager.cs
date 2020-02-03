using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JobsManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI averageScore, highScore;
    public static JobsManager instance;

    public List<JobController> tasks = new List<JobController>();

    bool gameEnded = false;

    private void Awake()
    {
        instance = this;
        endGamePanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
        else if (gameEnded && Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    public void TaskEnded ()
    {
        int avgScore = 0;
        foreach(var task in tasks)
        {
            if (!task.isFullfiled)
                return;
            avgScore += task.score;
            Debug.Log("AVG SCORE: " + avgScore);
        }

        avgScore = avgScore / tasks.Count;
        if (PlayerPrefs.GetInt("Highscore", 0) < avgScore)
        {
            PlayerPrefs.SetInt("Highscore", avgScore);
        }

        averageScore.text += avgScore;
        highScore.text += PlayerPrefs.GetInt("Highscore");

        MinigamesController.instance.BlockPlayer();
        endGamePanel.SetActive(true);
        gameEnded = true;
    }
}
