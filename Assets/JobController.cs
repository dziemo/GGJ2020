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
        scoreText.text = ((int)(score * 100)).ToString();
        scoreImage.fillAmount = score;
        JobsManager.instance.TaskEnded();
        GetComponent<Interactable>().enabled = false;

        foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
        {
            foreach (Material m in renderer.materials)
            {
                if (m.name.Contains("OutlineFill"))
                {
                    m.SetColor("_OutlineColor", Color.black);
                }
            }
        }
        gameObject.tag = "Untagged";
    }
}
