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

    public void RegisterScore (float score)
    {
        Debug.Log(score.ToString());
        isFullfiled = true;
        scoreText.text = (score * 100).ToString();
        scoreImage.fillAmount = score;
        JobsManager.instance.TaskEnded();
        GetComponent<Interactable>().enabled = false;
        GetComponent<Outline>().OutlineWidth = 0.0f;
        gameObject.tag = "Untagged";
    }
}
