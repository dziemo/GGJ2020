using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image scoreImage;

    public bool isFullfiled = false;
    public int score = 0;
    public void RegisterScore (float sc)
    {
        Debug.Log(sc.ToString());
        isFullfiled = true;
        scoreText.text = ((int)(sc * 100)).ToString();
        scoreImage.fillAmount = sc;
        score = (int)(sc * 100);
        JobsManager.instance.TaskEnded();
        GetComponent<Interactable>().enabled = false;
        gameObject.tag = "Untagged";
    }
}
