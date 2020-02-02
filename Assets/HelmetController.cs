using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelmetController : MonoBehaviour
{
    public static HelmetController helmetController;
    public JobController ctrl;
    Animator anim;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
        helmetController = this;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    public void HelmetOn (JobController jc)
    {
        anim.SetTrigger("PutOn");
        ctrl = jc;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if (arg0.name == "WeldScene")
        {
            GetComponent<RectTransform>().localScale = Vector3.one;
            anim.SetTrigger("TakeOff");
            cam.gameObject.SetActive(true);
            MinigamesController.instance.TurnOffMinigames();
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "WeldScene")
            cam.gameObject.SetActive(false);
        //START MINIGAME
        SceneManager.SetActiveScene(arg0);
    }

    public void LoadWeldScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
